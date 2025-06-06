using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    private const int RenderQueueDefault = -1;

    private static readonly string KeywordAlphaTest = "_ALPHATEST_ON";
    private static readonly string KeywordAlphaBlend = "_ALPHABLEND_ON";
    private static readonly string KeywordAlphaPremultiply = "_ALPHAPREMULTIPLY_ON";

    public event Action<Bomb> OnExploded;

    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 700f;
    [SerializeField] private float _fadeDurationMin = 2f;
    [SerializeField] private float _fadeDurationMax = 5f;

    private int _shaderModeOpaque = 0;
    private int _shaderModeTransparent = 3;
    private UnityEngine.Rendering.BlendMode _srcBlendMode = UnityEngine.Rendering.BlendMode.SrcAlpha;
    private UnityEngine.Rendering.BlendMode _dstBlendMode = UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;
    private int _renderQueueTransparent = (int)UnityEngine.Rendering.RenderQueue.Transparent;

    private Renderer _renderer;
    private Material _materialInstance;
    private float _fadeDuration;
    private bool _hasExploded = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _materialInstance = Instantiate(_renderer.material);
        _renderer.material = _materialInstance;
    }

    public void InitializeBomb()
    {
        _hasExploded = false;
        SetMaterialOpaque();

        Color color = _materialInstance.color;
        color.a = 1f;
        _materialInstance.color = color;

        _fadeDuration = UnityEngine.Random.Range(_fadeDurationMin, _fadeDurationMax);
        StartCoroutine(FadeAndExplode());
    }

    private IEnumerator FadeAndExplode()
    {
        SetMaterialTransparent();
        float timer = 0f;
        Color color = _materialInstance.color;

        while(timer < _fadeDuration)
        {
            timer += Time.deltaTime;
            float time = timer / _fadeDuration;
            color.a = Mathf.Lerp(1f, 0f, time);
            _materialInstance.color = color;
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

        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rigidbody = hit.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

        OnExploded?.Invoke(this);
    }

    private void SetMaterialTransparent()
    {
        _materialInstance.SetFloat("_Mode", _shaderModeTransparent);
        _materialInstance.SetInt("_SrcBlend", (int)_srcBlendMode);
        _materialInstance.SetInt("_DstBlend", (int)_dstBlendMode);
        _materialInstance.SetInt("_ZWrite", 0);
        _materialInstance.DisableKeyword(KeywordAlphaTest);
        _materialInstance.EnableKeyword(KeywordAlphaBlend);
        _materialInstance.DisableKeyword(KeywordAlphaPremultiply);
        _materialInstance.renderQueue = _renderQueueTransparent;
    }

    private void SetMaterialOpaque()
    {
        _materialInstance.SetFloat("_Mode", _shaderModeOpaque);
        _materialInstance.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        _materialInstance.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        _materialInstance.SetInt("_ZWrite", 1);
        _materialInstance.DisableKeyword(KeywordAlphaTest);
        _materialInstance.DisableKeyword(KeywordAlphaBlend);
        _materialInstance.DisableKeyword(KeywordAlphaPremultiply);
        _materialInstance.renderQueue = RenderQueueDefault;
    }
}