using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    public event Action<Bomb> OnExploded;

    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 700f;
    [SerializeField] private float _fadeDurationMin = 2f;
    [SerializeField] private float _fadeDurationMax = 5f;

    private MaterialHelper _materialHelper;
    private Exploader _exploader;
    private bool _hasExploded = false;

    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        _materialHelper = new MaterialHelper(renderer);

        _exploader = new Exploader(_explosionRadius, _explosionForce);
    }

    public void InitializeBomb()
    {
        _hasExploded = false;

        _materialHelper.ResetToOriginal();

        float fadeDuration = UnityEngine.Random.Range(_fadeDurationMin, _fadeDurationMax);

        StartCoroutine(FadeAndExplode(fadeDuration));
    }

    private IEnumerator FadeAndExplode(float duration)
    {
        _materialHelper.SetTransparent();

        float timer = 0f;

        while(timer < duration)
        {
            timer += Time.deltaTime;
            float time = timer / duration;
            
            _materialHelper.SetAlpha(Mathf.Lerp(1f, 0f, time));

            yield return null;
        }

        Explode();
    }

    private void Explode()
    {
        if (_hasExploded)
        {
            return;
        }

        _hasExploded = true;

        _exploader.Explode(transform.position);

        OnExploded?.Invoke(this);
    }
}