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
        Vector3 targetDirection = targetPosition - transform.position;
        Rigidbody.AddForce(targetDirection.normalized * _bulletSpeed, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        _isHit = true;
    }
}
