using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    private float _explosionForce = 10.0f;
    private float _explosionRadius = 5.0f;
    private float _upwardsModifier = 1.0f;

    public void ApplyExplosionForce(Cube cube)
    {
        Rigidbody rigidbodyCube = cube.Rigidbody;

        Vector3 randomDirection = cube.transform.position + Random.insideUnitSphere.normalized;

        if (rigidbodyCube != null)
        {
            rigidbodyCube.AddExplosionForce(_explosionForce, randomDirection, _explosionRadius, _upwardsModifier, ForceMode.Impulse);
        }
    }
}
