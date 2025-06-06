using UnityEngine;

public class Exploader
{
    private float _radius;
    private float _force;

    public Exploader(float radius, float force)
    {
        _radius = radius;
        _force = force;
    }

    public void Explode(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, _radius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rigidbody = hit.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_force, position, _radius);
            }
        }
    }
}