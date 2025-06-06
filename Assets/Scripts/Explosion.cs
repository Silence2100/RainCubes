using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float _explosionRadiusMultiplier = 10f;
    public float _explosionForceMultiplier = 1f;

    public void HandleExplosion(Vector3 explosionCenter, float size)
    {
        float explosionRadius = _explosionRadiusMultiplier / size;
        float explosionForce = _explosionForceMultiplier / size;

        Collider[] colliders = Physics.OverlapSphere(explosionCenter, explosionRadius);

        foreach (var collider in colliders)
        {
            Rigidbody rigidbody = collider.attachedRigidbody;

            if (rigidbody != null)
            {
                Vector3 direction = (rigidbody.position - explosionCenter).normalized;

                float distance = Vector3.Distance(explosionCenter, rigidbody.position);
                float adjustedForce = explosionForce * (1 - (distance / explosionRadius));

                if (adjustedForce > 0)
                {
                    rigidbody.AddForce(direction * adjustedForce, ForceMode.Impulse);
                }
            }
        }
    }
}
