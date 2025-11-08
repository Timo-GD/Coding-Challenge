using UnityEngine;

namespace CodingChallenge.Minigames
{
    public class MinigameColliderCheck : MonoBehaviour
    {
        public delegate void Hit(int scoreIncrease);
        public event Hit OnHit;
        
        [SerializeField] private int _points;

        private bool _isHit;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                return;
            OnHit?.Invoke(_points);
        }
    }
}
