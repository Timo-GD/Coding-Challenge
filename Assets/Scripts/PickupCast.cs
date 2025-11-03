using UnityEngine;
using UnityEngine.InputSystem;

public class PickupCast : MonoBehaviour
{
    [SerializeField] private InputAction _pickup;

    RaycastHit[] _itemCastHits;
    LayerMask _itemMask;
    LayerMask _armorMask;

    private void Awake()
    {
        _pickup.performed += context => TryPickUp();
        _itemMask = LayerMask.GetMask("Item");
    }

    private void TryPickUp()
    {

        if (Physics.SphereCastNonAlloc(transform.position, 5f, transform.forward, _itemCastHits, Mathf.Infinity, _itemMask) == 0)
            return;

        for(int i = 0; i < _itemCastHits.Length; i++)
        {
            // if (!_itemCastHits[i].collider.TryGetComponent<Item>(out Item item))
            //     continue;

            // item.Equip();

            if (!GetComponentInParent<InverntorySystem>().Equip(_itemCastHits[i].collider.gameObject))
                continue;

            _itemCastHits[i].collider.GetComponent<Item>().Equip();
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1f);

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
