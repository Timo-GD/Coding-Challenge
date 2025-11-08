using UnityEngine;

namespace CodingChallenge.Minigames
{
    public class MinigameColliderCheck : MonoBehaviour
    {
        [SerializeField] private int _points;

        private bool _isHit;

        public delegate void Hit(int scoreIncrease);
        public event Hit OnHit;

        private void OnCollisionEnter(Collision collision)
        {
            if (_isHit)
                return;

            if (collision.gameObject.CompareTag("Player"))
                return;

            OnHit?.Invoke(_points);
            _isHit = true;
        }

        private void OnCollisionExit(Collision collision)
        {
            _isHit = false;
        }
    }
}
