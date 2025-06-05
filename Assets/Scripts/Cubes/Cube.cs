using System;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private bool _hasTouched = false;
    private Color _originalColor;

    public event Action<Cube> TouchedPlatform;

    public Renderer Renderer { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Renderer = GetComponent<Renderer>();
        _originalColor = Renderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_hasTouched && collision.collider.GetComponent<Platform>() != null)
        {
            _hasTouched = true;
            ChangeColor();

            TouchedPlatform?.Invoke(this);
        }
    }

    public void ResetCube()
    {
        _hasTouched = false;
        Renderer.material.color = _originalColor;
    }

    private void ChangeColor()
    {
        Renderer.material.color = UnityEngine.Random.ColorHSV();
    }
}