using UnityEngine;

namespace CodingChallenge.Minigames
{
    public class MinigameColliderCheck : MonoBehaviour
    {
        public delegate void Hit(int scoreIncrease);
        public event Hit OnHit;

        [SerializeField] private int _points;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                return;
            OnHit?.Invoke(_points);
        }
    }
}
