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
            if (collision.gameObject.CompareTag("Player"))
                return;
            OnHit?.Invoke(_points);
        }
    }
}
