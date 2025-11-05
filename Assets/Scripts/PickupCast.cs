using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupCast : MonoBehaviour
{
    [SerializeField] private InputAction _pickup;
    [SerializeField] private InputAction _drop;

    private Item _heldItem;
    private NewInventorySystem _inventorySystem;
    private RaycastHit[] _itemCastHits = new RaycastHit[1];
    private LayerMask _itemMask;
    private LayerMask _armorMask;

    private void Awake()
    {
        _pickup.performed += context => TryPickUp();

        
        

        _itemMask = LayerMask.GetMask("Item");
        _inventorySystem = GetComponentInParent<NewInventorySystem>();
    }



    private void TryPickUp()
    {

        if (_heldItem != null)
            return;
            
        if (Physics.SphereCastNonAlloc(transform.position, .6f, transform.forward, _itemCastHits, 0, _itemMask) == 0)
            return;

        if (!_inventorySystem.Equip(gameObject, _itemCastHits[0].collider.GetComponent<Item>()))
            return;

        _pickup.performed += context => Use();
        _pickup.canceled += context => StopUse();
        _heldItem = _itemCastHits[0].collider.GetComponent<Item>();
        _itemCastHits[0].collider.GetComponent<Item>().Equip(gameObject);
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
    




    private void OnDestroy()
    {
        _pickup.performed -= context => TryPickUp();
    }

    private void OnEnable()
    {
        _pickup.Enable();
        _drop.Disable();
    }

    private void OnDisable()
    {
        _pickup.Disable();
        _drop.Disable();
    }
}
