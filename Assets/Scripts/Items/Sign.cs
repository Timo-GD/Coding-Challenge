using TMPro;
using UnityEngine;

namespace CodingChallenge.Items
{
    public class Sign : Item
    {
        private TextMeshPro _scoreText;
        private int _score = 0;
        public override void Awake()
        {
            _scoreText = GetComponent<TextMeshPro>();
            base.Awake();
        }
        private void AddScore(int increaseAmount)
        {
            _score += increaseAmount;
            _scoreText.text = "Score: " + _score;
        }
    }
}
