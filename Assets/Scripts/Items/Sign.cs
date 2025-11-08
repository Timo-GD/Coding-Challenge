using CodingChallenge.Minigames;
using TMPro;
using UnityEngine;

namespace CodingChallenge.Items
{
    public class Sign : Item
    {
        [SerializeField] private GameObject _minigame;
        private TextMeshPro _scoreText;
        private int _score = 0;
        public override void Awake()
        {
            ColliderCheck[] scoreTriggers = _minigame.GetComponentsInChildren<ColliderCheck>();
            for (int i = 0; i < scoreTriggers.Length; i++)
                scoreTriggers[i].OnHit += (score) => AddScore(score);
                
            _scoreText = GetComponentInChildren<TextMeshPro>();
            base.Awake();
        }
        private void AddScore(int increaseAmount)
        {
            if (_score + increaseAmount > 100)
                return;
            _score += increaseAmount;
            _scoreText.text = "Score: " + _score;
        }
    }
}
