using UnityEngine;
using UnityEngine.InputSystem;

public class InverntorySystem : MonoBehaviour
{
    [SerializeField] private InputAction _drop;
    [SerializeField] private InputAction _use;
    [SerializeField] private InputAction _switch;
    [SerializeField] private Transform _rightHandTransform;
    [SerializeField] private Transform _leftHandTransform;
    private Transform _rightHandItem;
    private Transform _leftHandItem;

    private void Awake()
    {
        _drop.performed += context => DeEquip();
        _use.performed += context => Use();
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
            _rightHandItem.gameObject.GetComponent<Item>().Use();
        else if (_use.ReadValue<float>() < 0 && _leftHandItem != null)
            _leftHandItem.gameObject.GetComponent<Item>().Use();
        else
            return;
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
            _rightHandItem = null;
        }
        else if (_leftHandItem != null)
        {
            _leftHandItem.gameObject.GetComponent<Item>().DeEquip();
            _leftHandItem = null;
        }
    }

    public bool Equip(GameObject equipable)
    {
        if (_rightHandItem == null)
        {
            _rightHandItem = equipable.transform;
            return true;
        }
        else if (_leftHandItem == null)
        {
            _leftHandItem = equipable.transform;
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
        _switch.performed -= context => Switch();
    }

    private void OnEnable()
    {
        _use.Enable();
        _drop.Enable();
        _switch.Enable();
    }

    private void OnDisable()
    {
        _use.Enable();
        _drop.Disable();
        _switch.Disable();
    }
}
