using UnityEngine;
using UnityEngine.InputSystem;

public class InverntorySystem : MonoBehaviour
{
    [SerializeField] private InputAction _drop;
    [SerializeField] private Transform _rightHandTransform;
    [SerializeField] private Transform _leftHandTransform;
    private Rigidbody _rightHandItem;
    private Rigidbody _leftHandItem;
    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _drop.performed += context => DeEquip();
    }
    private void FixedUpdate()
    {
        if (_rightHandItem != null || _leftHandItem != null)
            UpdateHands();
    }

    private void UpdateHands()
    {
        if (_rightHandItem != null)
        {
            _rightHandItem.MovePosition(_rightHandTransform.position);
            _rightHandItem.MoveRotation(_rightHandTransform.rotation);
        }

        if (_leftHandItem != null)
        {
            _leftHandItem.MovePosition(_leftHandTransform.position);
            _leftHandItem.MoveRotation(_leftHandTransform.rotation);
        }
    }

    private void DeEquip()
    {
        if (_rightHandItem != null)
        {
            _rightHandItem.gameObject.GetComponent<Item>().DeEquip();
            _rightHandItem = null;
        }

        if (_leftHandItem != null)
        {
            _leftHandItem.gameObject.GetComponent<Item>().DeEquip();
            _leftHandItem = null;
        }
    }

    public bool Equip(GameObject equipable)
    {
        if (_rightHandItem == null)
        {
            _rightHandItem = equipable.GetComponent<Rigidbody>();
            return true;
        }
        else if (_leftHandItem == null)
        {
            _leftHandItem = equipable.GetComponent<Rigidbody>();
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
    }

    private void OnEnable()
    {
        _drop.Enable();
    }

    private void ODisable()
    {
        _drop.Disable();
    }
}
