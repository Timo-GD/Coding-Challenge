using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupCast : MonoBehaviour
{
    [SerializeField] private InputAction _pickup;
    [SerializeField] private InputAction _drop;
    [SerializeField] private InputAction _switchUseMode;

    private bool _canUse;
    private Item _heldItem;
    public Item HeldItem => _heldItem;
    private NewInventorySystem _inventorySystem;
    private RaycastHit[] _itemCastHits = new RaycastHit[1];
    private LayerMask _itemMask;
    private LayerMask _armorMask;

    private void Awake()
    {
        
        _pickup.performed += context => CheckUse();
        _pickup.canceled += context => StopUse();
        _drop.performed += context => DropItem(false);
        _switchUseMode.performed += context => SwitchUseMode();
        
        

        _itemMask = LayerMask.GetMask("Item");
        _inventorySystem = GetComponentInParent<NewInventorySystem>();
    }

    public void SwitchItem(Item newItem)
    {
        _heldItem = newItem;
        if (_heldItem != null)
        {
            _heldItem.Equip(gameObject);
            _canUse = true;
        }
        else
        {
            _canUse = false;
        }
    }
    
    private void CheckUse()
    {
        if (!_canUse)
            TryPickUp();
        else
            Use();
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
        if (_heldItem == null)
            return;

        StartCoroutine(_heldItem.Using());
    }

    private void StopUse()
    {
        if (_heldItem == null)
            return;

        _heldItem.StopUsing();
    }

    private void SwitchUseMode()
    {
        if (_heldItem == null)
            return;

        if (!_inventorySystem.SwitchUseMode(gameObject))
            return;
    }

    public void DropItem(bool selfdrop)
    {
        if (_heldItem == null)
            return;

        if (!_inventorySystem.DeEquip(gameObject, selfdrop))
            return;
        _canUse = false;
        _heldItem = null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, .6f);
    }

    private void OnDestroy()
    {
        _pickup.performed -= context => CheckUse();
        _pickup.canceled -= context => StopUse();
        _drop.performed -= context => DropItem(false);
        _switchUseMode.performed -= context => SwitchUseMode();
    }

    private void OnEnable()
    {
        _pickup.Enable();
        _drop.Enable();
        _switchUseMode.Enable();
    }

    private void OnDisable()
    {
        _pickup.Disable();
        _drop.Disable();
        _switchUseMode.Disable();
    }
}
