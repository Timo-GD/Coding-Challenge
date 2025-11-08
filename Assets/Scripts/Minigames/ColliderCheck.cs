using UnityEngine;

namespace CodingChallenge.Minigames
{
    public class ColliderCheck : MonoBehaviour
    {
        [SerializeField] private int _points;
        public delegate void Hit(int scoreIncrease);
        public event Hit OnHit;
        private void OnCollisionEnter(Collision collision)
        {
            OnHit?.Invoke(_points);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                return;

            OnHit?.Invoke(_points);
        }
    }
}
