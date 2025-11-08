using CodingChallenge.Interactable;
using UnityEngine;

namespace CodingChallenge.Minigames
{
    public class ButtonGameManager : MonoBehaviour
    {
        [SerializeField] private Button _redButton;
        [SerializeField] private Button _greenButton;
        [SerializeField] private Button _blueButton;
        [SerializeField] private Button _yellowButton;
        [SerializeField] private Material _prizeColor;

        private Renderer[] _pressedColors;
        private Color[] _answerColors;
        private int _index = 0;
        private int _isCorrect = 0;

        private void Awake()
        {
            _pressedColors = GetComponentsInChildren<Renderer>();
            _answerColors = new Color[]
            { _blueButton.GetComponent<Renderer>().material.color,
            _greenButton.GetComponent<Renderer>().material.color,
            _yellowButton.GetComponent<Renderer>().material.color,
            _redButton.GetComponent<Renderer>().material.color };

            _redButton.OnPress += (buttonColor) => UpdateCode(buttonColor);
            _greenButton.OnPress += (buttonColor) => UpdateCode(buttonColor);
            _blueButton.OnPress += (buttonColor) => UpdateCode(buttonColor);
            _yellowButton.OnPress += (buttonColor) => UpdateCode(buttonColor);
        }

        private void UpdateCode(Color buttonColor)
        {
            if (_index > 3)
            {
                ResetCode();
                return;
            }

            _pressedColors[_index].material.color = buttonColor;
            CheckPassword();
            _index++;
        }

        private void ResetCode()
        {
            _index = 0;
            _isCorrect = 0;
            for (int i = 0; i < _pressedColors.Length; i++)
                _pressedColors[i].material.color = Color.black;
        }

        private void CheckPassword()
        {
            if (_pressedColors[_index].material.color == _answerColors[_index])
                _isCorrect++;

            if (_isCorrect < 4)
                return;

            MinigameManager.Instance.UpdateMinigameCount();
            for (int i = 0; i < _pressedColors.Length; i++)
                _pressedColors[i].material = _prizeColor;
        }

    }
}
