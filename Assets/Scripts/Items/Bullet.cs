using UnityEngine;

namespace CodingChallenge.Items
{
    public class Bullet : Item
    {
        private int _bulletSpeed = 20;

        /// <summary>
        /// Fires the bullet in the direction of the targetposition;
        /// </summary>
        /// <param name="targetPosition">The position that the bullet has to go to;</param>
        public void Fire(Vector3 targetPosition)
        {
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.useGravity = false;

            Vector3 targetDirection = targetPosition - transform.position;
            transform.rotation = Quaternion.Euler(targetDirection);
            _rigidbody.AddForce(targetDirection.normalized * _bulletSpeed, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            _rigidbody.useGravity = true;
        }
    }
}
