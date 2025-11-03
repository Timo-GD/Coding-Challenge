using UnityEngine;
using UnityEngine.InputSystem;

public class PickupCast : MonoBehaviour
{
    [SerializeField] private InputAction _pickup;

    private void Awake()
    {
        _pickup.performed += context => TryPickUp();
    }

    private void TryPickUp()
    {

    }

    private void OnDestroy()
    {
        _pickup.performed -= context => TryPickUp();
    }

    private void OnEnable()
    {
        _pickup.Enable();
    }

    private void OnDisable()
    {
        _pickup.Disable();
    }
}
