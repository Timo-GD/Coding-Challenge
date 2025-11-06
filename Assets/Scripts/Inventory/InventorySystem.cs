using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private InputAction _switchItem;

    private Dictionary<PickupCast, Item> _inventoryItems = new();
    private PickupCast[] _hands;
    private bool _isAlreadyDropped;

    public bool Equip(PickupCast hand, Item equipable)
    {
        if (_inventoryItems.ContainsKey(hand))
            return false;


        equipable.Equip(hand);
        equipable.transform.SetParent(transform, true);
        _inventoryItems.Add(hand, equipable);

        return true;
    }

    public bool DeEquip(PickupCast hand, bool selfdrop)
    {
        if (_isAlreadyDropped)
        {
            _isAlreadyDropped = false;
            return false;
        }

        if (_inventoryItems.Count > 1 && !selfdrop)
            _isAlreadyDropped = true;
        
        _inventoryItems[hand].transform.SetParent(null);
        _inventoryItems[hand].DeEquip();
        _inventoryItems.Remove(hand);

        return true;
    }

    private void Awake()
    {
        _switchItem.performed += context => SwitchItems();
        _hands = GetComponentsInChildren<PickupCast>();
    }

    private void LateUpdate()
    {
        if(_inventoryItems.Count != 0)
            UpdateHands();
    }

    private void UpdateHands()
    {
        foreach(KeyValuePair<PickupCast, Item> handAndItem in _inventoryItems)
        {
            handAndItem.Value.transform.position = handAndItem.Key.transform.position;
            handAndItem.Value.transform.rotation = handAndItem.Key.transform.rotation;
        }
    }

    private void SwitchItems()
    {
        if (_hands.Length == 0)
            return;

        Item[] oldItems = new Item[_hands.Length];

        for (int i = 0; i < _hands.Length; i++)
            oldItems[i] = _hands[i].HeldItem;

        for(int i = 0; i < _hands.Length; i++)
        {
            int targetIndex;

            if (_switchItem.ReadValue<float>() > 0)
                targetIndex = (i + 1) % _hands.Length;
            else
                targetIndex = (i - 1 + _hands.Length) % _hands.Length;

            _hands[targetIndex].SwitchItem(oldItems[i]);

            if (_hands[targetIndex].HeldItem != null)
                _inventoryItems[_hands[targetIndex]] = _hands[targetIndex].HeldItem;
            else
                _inventoryItems.Remove(_hands[targetIndex]);
        }
    }

    private void OnDestroy()
    {
        _switchItem.performed -= context => SwitchItems();
    }

    private void OnEnable()
    {
        _switchItem.Enable();
    }

    private void OnDisable()
    {
        _switchItem.Disable();
    }
}
