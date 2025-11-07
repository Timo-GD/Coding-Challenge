using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGameManager : MonoBehaviour
{
    [SerializeField] private Button _redButton;
    [SerializeField] private Button _greenButton;
    [SerializeField] private Button _blueButton;
    [SerializeField] private Button _yellowButton;

    private Renderer[] _answerColors;
    private int _index = 0;

    private void Awake()
    {
        _answerColors = GetComponentsInChildren<Renderer>();

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

        _answerColors[_index].material.color = buttonColor;
        _index++;
    }
    
    private void ResetCode()
    {
        _index = 0;
        for (int i = 0; i < _answerColors.Length; i++)
            _answerColors[i].material.color = Color.gray;
    }

}
