using UnityEngine;
using UnityEngine.InputSystem;

public class PickupCast : MonoBehaviour
{
    [SerializeField] private InputAction _pickup;

    RaycastHit[] _itemCastHits = new RaycastHit[1];
    LayerMask _itemMask;
    LayerMask _armorMask;

    private void Awake()
    {
        _pickup.performed += context => TryPickUp();
        _itemMask = LayerMask.GetMask("Item");
    }

    private void TryPickUp()
    {

        // Debug.Log("After" + _itemCastHits.Length);
        // Debug.Log(Physics.SphereCastNonAlloc(new Ray(transform.position, transform.forward), 1.25f, _itemCastHits, Mathf.Infinity, _itemMask));
        // Debug.Log("Before" + _itemCastHits.Length);
        // Debug.Log(Physics.SphereCast(transform.position, 5f, transform.forward, out RaycastHit ItemHit, Mathf.Infinity, _itemMask));
        if (Physics.SphereCastNonAlloc(transform.position, 1.25f, transform.forward, _itemCastHits, Mathf.Infinity, _itemMask) == 0)
            return;

        // for(int i = 0; i < _itemCastHits.Length; i++)
        // {
        // if (!_itemCastHits[i].collider.TryGetComponent<Item>(out Item item))
        //     continue;

        // item.Equip();

        if (!GetComponentInParent<InverntorySystem>().Equip(_itemCastHits[0].collider.gameObject))
            return;
                // continue;

        _itemCastHits[0].collider.GetComponent<Item>().Equip();
            
        // }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1.25f);

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
