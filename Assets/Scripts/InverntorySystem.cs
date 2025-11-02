using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InverntorySystem : MonoBehaviour
{
    [SerializeField] private InputAction _drop;
    [SerializeField] private InputAction _use;
    [SerializeField] private InputAction _switch;
    [SerializeField] private InputAction _useModeSwitch;
    [SerializeField] private Transform _rightHandTransform;
    [SerializeField] private Transform _leftHandTransform;
    private Transform _rightHandItem;
    private Transform _leftHandItem;

    private void Awake()
    {
        _drop.performed += context => DeEquip();
        _use.performed += context => Use();
        _use.canceled += context => StopUse();
        _useModeSwitch.performed += context => SwitchUseMode();
        _switch.performed += context => Switch();
    }
    private void LateUpdate()
    {
        if (_rightHandItem != null || _leftHandItem != null)
            UpdateHands();
    }

    private void Use()
    {
        if (_use.ReadValue<float>() > 0 && _rightHandItem != null)
            StartCoroutine(_rightHandItem.gameObject.GetComponent<Item>().Using());
        else if (_use.ReadValue<float>() < 0 && _leftHandItem != null)
            StartCoroutine(_leftHandItem.gameObject.GetComponent<Item>().Using());
        else
            return;
    }

    private void StopUse()
    {
        if (_rightHandItem != null)
            _rightHandItem.gameObject.GetComponent<Item>().StopUsing();

        if (_leftHandItem != null)
            _leftHandItem.gameObject.GetComponent<Item>().StopUsing();
    }

    private void SwitchUseMode()
    {
        if (_rightHandItem != null)
            _rightHandItem.gameObject.GetComponent<Item>().ModeSwitch();
    }

    private void UpdateHands()
    {
        if (_rightHandItem != null)
        {
            _rightHandItem.position = _rightHandTransform.position;
            _rightHandItem.rotation = _rightHandTransform.rotation;
        }

        if (_leftHandItem != null)
        {
            _leftHandItem.position = _leftHandTransform.position;
            _leftHandItem.rotation = _leftHandTransform.rotation;
        }
    }

    private void Switch()
    {
        Transform previousRightItem = _rightHandItem;
        Transform previousLeftItem = _leftHandItem;

        _leftHandItem = previousRightItem;
        _rightHandItem = previousLeftItem;
    }

    private void DeEquip()
    {
        if (_rightHandItem != null)
        {
            _rightHandItem.gameObject.GetComponent<Item>().DeEquip();
            _rightHandItem.SetParent(null);
            _rightHandItem = null;
        }
        else if (_leftHandItem != null)
        {
            _leftHandItem.gameObject.GetComponent<Item>().DeEquip();
            _leftHandItem.SetParent(null);
            _leftHandItem = null;
        }
    }

    public bool Equip(GameObject equipable)
    {
        if (_rightHandItem == null)
        {
            _rightHandItem = equipable.transform;
            _rightHandItem.SetParent(GetComponentsInChildren<Transform>()[1]);
            return true;
        }
        else if (_leftHandItem == null)
        {
            _leftHandItem = equipable.transform;
            _leftHandItem.SetParent(GetComponentsInChildren<Transform>()[1]);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDestroy()
    {
        _drop.performed -= context => DeEquip();
        _use.performed -= context => Use();
        _use.canceled -= context => StopUse();
        _useModeSwitch.performed -= context => SwitchUseMode();
        _switch.performed -= context => Switch();
    }

    private void OnEnable()
    {
        _use.Enable();
        _useModeSwitch.Enable();
        _drop.Enable();
        _switch.Enable();
    }

    private void OnDisable()
    {
        _use.Enable();
        _useModeSwitch.Enable();
        _drop.Disable();
        _switch.Disable();
    }
}
