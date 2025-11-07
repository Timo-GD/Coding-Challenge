using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonGameManager : MonoBehaviour
{
    [SerializeField] private Button _redButton;
    [SerializeField] private Button _greenButton;
    [SerializeField] private Button _blueButton;
    [SerializeField] private Button _yellowButton;

    private Renderer[] _pressedColors;
    private Color[] _answerColors = { Color.gray, Color.gray, Color.gray, Color.gray };
    private int _index = 0;

    private void Awake()
    {
        _pressedColors = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < _pressedColors.Length; i++)
        {
            _answerColors[i] = _pressedColors[i].material.color;
            _pressedColors[i].material.color = Color.gray;
        }

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
        _index++;
        if(_index > 3)
            CheckPassword();
    }

    private void ResetCode()
    {
        _index = 0;
        for (int i = 0; i < _pressedColors.Length; i++)
            _pressedColors[i].material.color = Color.gray;
    }
    
    private void CheckPassword()
    {
        for (int i = 0; i < _pressedColors.Length; i++)
            Debug.Log("Pressed Color:   " + _pressedColors[i].material.color);
        for (int i = 0; i < _answerColors.Length; i++)
            Debug.Log("Anser Color: " + _answerColors[i]);
        int isCorrect = 0;
        if (_pressedColors[0].material.color == _answerColors[0])
            isCorrect++;
        if (_pressedColors[1].material.color == _answerColors[1])
            isCorrect++;
        if (_pressedColors[2].material.color == _answerColors[2])
            isCorrect++;
        if (_pressedColors[3].material.color == _answerColors[3])
            isCorrect++;
        Debug.Log(isCorrect);
        if (isCorrect == 4)
            Debug.Log("B-B-B-B-B-Bingo");
    }

}
