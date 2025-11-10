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
            MinigameColliderCheck[] scoreTriggers = _minigame.GetComponentsInChildren<MinigameColliderCheck>();

            for (int i = 0; i < scoreTriggers.Length; i++)
                scoreTriggers[i].OnHit += (score) => AddScore(score);
                
            _scoreText = GetComponentInChildren<TextMeshPro>();
            base.Awake();
        }
        private void AddScore(int increaseAmount)
        {
            if (_score + increaseAmount >= 100 && _score < 100)
                MinigameManager.Instance.UpdateMinigameCount();
                
            _score += increaseAmount;

            if (_score >= 1000)
                _score = 999;

            _scoreText.text = "Score: " + _score;
        }
    }
}
