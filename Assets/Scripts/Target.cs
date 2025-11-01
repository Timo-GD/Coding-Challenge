using UnityEngine;
using UnityEngine.InputSystem;

public class Target : MonoBehaviour
{
    [SerializeField] InputAction _interact;
    private LayerMask _layerMask;
    private void Awake()
    {
        _layerMask = LayerMask.GetMask("Item");
        _interact.performed += context => CheckInteraction();
    }
    private void CheckInteraction()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, Mathf.Infinity, _layerMask))
            raycastHit.collider.GetComponent<Item>().Interact(transform.position);
    }

    private void OnEnable()
    {
        _interact.Enable();
    }

    private void ODisable()
    {
        _interact.Disable();
    }
}
