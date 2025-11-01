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
        if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, Mathf.Infinity, _layerMask))
            return;
        if (GetComponentInParent<InverntorySystem>().Equip(raycastHit.collider.gameObject))
            raycastHit.collider.GetComponent<Item>().Equip();
    }

    private void OnDestroy()
    {
        _interact.performed -= context => CheckInteraction();
    }

    private void OnEnable()
    {
        _interact.Enable();
    }

    private void OnDisable()
    {
        _interact.Disable();
    }
}
