using UnityEngine;

public class Bullet : Item
{
    private int _bulletSpeed = 50;
    private bool _isHit;
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        BoxColliders = GetComponentsInChildren<BoxCollider>();
    }

    public void Fire(Vector3 targetPosition)
    {
        Rigidbody.linearVelocity = Vector3.zero;
        Vector3 targetDirection = targetPosition - transform.position;
        Rigidbody.AddForce(targetDirection.normalized * _bulletSpeed, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        _isHit = true;
    }
}
