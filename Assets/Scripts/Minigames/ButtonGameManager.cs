using UnityEngine;

public class ButtonGameManager : MonoBehaviour
{
    [SerializeField] private Button _redButton;
    [SerializeField] private Button _greenButton;
    [SerializeField] private Button _blueButton;
    [SerializeField] private Button _yellowButton;

    //private Button[] _buttons;

    private void Awake()
    {
        //_buttons = GetComponentsInChildren<Button>();

        _redButton.OnPressStateChange += (isPressed) => UpdateCode(isPressed, Color.red);
    }

    private bool UpdateCode(bool buttonState, Color buttonColor)
    {
        Debug.Log(buttonState);
        return false;   
    }

}
