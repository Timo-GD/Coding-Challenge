using UnityEngine;

namespace Items
{
    public class Bullet : Item
    {
        private int _bulletSpeed = 50;

        public void Fire(Vector3 targetPosition)
        {
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            Vector3 targetDirection = targetPosition - transform.position;
            transform.rotation = Quaternion.Euler(targetDirection);
            _rigidbody.AddForce(targetDirection.normalized * _bulletSpeed, ForceMode.Impulse);
        }
    }
}
