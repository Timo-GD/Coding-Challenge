using System.Collections;
using UnityEngine;

public class ButtonGameManager : MonoBehaviour
{
    [SerializeField] private Button _redButton;
    [SerializeField] private Button _greenButton;
    [SerializeField] private Button _blueButton;
    [SerializeField] private Button _yellowButton;

    private Renderer[] _answerColors;
    private Stack _colorCombination;

    private void Awake()
    {
        _redButton.OnPressStateChange += (isPressed) => UpdateCode(isPressed, Color.red);
        _greenButton.OnPressStateChange += (isPressed) => UpdateCode(isPressed, Color.red);
        _blueButton.OnPressStateChange += (isPressed) => UpdateCode(isPressed, Color.red);
        _yellowButton.OnPressStateChange += (isPressed) => UpdateCode(isPressed, Color.red);
    }

    private bool UpdateCode(bool buttonState, Color buttonColor)
    {
        if (_colorCombination.Count > 4)
            return false;

        Debug.Log(buttonState);
        return false;   
    }

}
