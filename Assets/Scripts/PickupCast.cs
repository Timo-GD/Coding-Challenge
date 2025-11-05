using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupCast : MonoBehaviour
{
    [SerializeField] private InputAction _pickup;
    [SerializeField] private InputAction _drop;


    private bool _canUse;
    private Item _heldItem;
    public Item HeldItem => _heldItem;
    private NewInventorySystem _inventorySystem;
    private RaycastHit[] _itemCastHits = new RaycastHit[1];
    private LayerMask _itemMask;
    private LayerMask _armorMask;

    private void Awake()
    {
        _pickup.performed += context => TryPickUp();
        _pickup.performed += context => Use();
        _pickup.canceled += context => StopUse();

        _drop.performed += context => DropItem();
        
        

        _itemMask = LayerMask.GetMask("Item");
        _inventorySystem = GetComponentInParent<NewInventorySystem>();
    }

    public void SwitchItem(Item newItem)
    {
        _heldItem = newItem;
        if(_heldItem != null)
            _heldItem.transform.SetParent(transform);
    }

    private void TryPickUp()
    {

        if (_heldItem != null)
            return;
            
        if (Physics.SphereCastNonAlloc(transform.position, .6f, transform.forward, _itemCastHits, 0, _itemMask) == 0)
            return;

        if (!_inventorySystem.Equip(gameObject, _itemCastHits[0].collider.GetComponent<Item>()))
            return;

        _canUse = true;
        _itemCastHits[0].collider.GetComponent<Item>().Equip(gameObject);
        _heldItem = _itemCastHits[0].collider.GetComponent<Item>();
    }

    private void Use()
    {
        if (_heldItem == null || !_canUse)
            return;

        StartCoroutine(_heldItem.Using());
    }

    private void StopUse()
    {
        if (_heldItem == null || !_canUse)
            return;

        _heldItem.StopUsing();
    }
    
    public void DropItem()
    {
        if (_heldItem == null)
            return;
        _canUse = false;
        _heldItem = null;
        _inventorySystem.DeEquip(gameObject);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, .6f);
    }

    private void OnDestroy()
    {
        _pickup.performed -= context => TryPickUp();
        _pickup.performed -= context => Use();
        _pickup.canceled -= context => StopUse();
        _drop.performed -= context => DropItem();
    }

    private void OnEnable()
    {
        _pickup.Enable();
        _drop.Enable();
    }

    private void OnDisable()
    {
        _pickup.Disable();
        _drop.Disable();
    }
}
