using UnityEngine;

public class InverntorySystem : MonoBehaviour
{
    [SerializeField] Transform _rightHandTransform;
    [SerializeField] Transform _leftHandTransform;
    private Rigidbody _rightHandItem;
    private Rigidbody _leftHandItem;
    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
}
