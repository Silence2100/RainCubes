using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]

public class Cube : MonoBehaviour
{
    public event Action<Cube> OnClicked;

    private Renderer _renderer;
    private float _splitChanceDecay = 2.0f;

    public float CurrentChance { get; private set; } = 1.0f;
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    public void OnMouseDown()
    {
        OnClicked?.Invoke(this);

        Destroy(gameObject);
    }

    public void ChangeColor()
    {
        _renderer.material.color = UnityEngine.Random.ColorHSV();
    }

    public void ReduceChance(float chance)
    {
        CurrentChance = chance / _splitChanceDecay;
    }
}
