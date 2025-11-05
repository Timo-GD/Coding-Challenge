using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewInventorySystem : MonoBehaviour
{
    [SerializeField] private InputAction _switchItem;
    private Dictionary<GameObject, Item> _inventoryItems = new();
    private bool _isAlreadyDropped;
    private bool _isAlreadySwitched;

    private void Awake()
    {
        _switchItem.performed += context => SwitchItems();
    }

    private void LateUpdate()
    {
        UpdateHands();
    }

    private void UpdateHands()
    {
        foreach(KeyValuePair<GameObject, Item> handAndItem in _inventoryItems)
        {
            handAndItem.Value.transform.position = handAndItem.Key.transform.position;
            handAndItem.Value.transform.rotation = handAndItem.Key.transform.rotation;
        }
    }

    private void SwitchItems()
    {
        PickupCast[] hands = GetComponentsInChildren<PickupCast>();

        if (hands.Length == 0)
            return;

        Item[] oldItems = new Item[hands.Length];

        for (int i = 0; i < hands.Length; i++)
            oldItems[i] = hands[i].HeldItem;

        for(int i = 0; i < hands.Length; i++)
        {
            int targetIndex;

            if (_switchItem.ReadValue<float>() > 0)
                targetIndex = (i + 1) % hands.Length;
            else
                targetIndex = (i - 1 + hands.Length) % hands.Length;

            hands[targetIndex].SwitchItem(oldItems[i]);

            if (hands[targetIndex].HeldItem != null)
                _inventoryItems[hands[targetIndex].gameObject] = hands[targetIndex].HeldItem;
            else
                _inventoryItems.Remove(hands[targetIndex].gameObject);
        }
    }

    public bool Equip(GameObject hand, Item equipable)
    {
        if (_inventoryItems.ContainsKey(hand))
            return false;


        equipable.transform.SetParent(transform, true);
        equipable.transform.position = hand.transform.position;
        equipable.transform.rotation = transform.rotation;
        _inventoryItems.Add(hand, equipable);

        return true;
    }

    public bool DeEquip(GameObject hand, bool selfdrop)
    {
        if (_isAlreadyDropped)
        {
            _isAlreadyDropped = false;
            return false;
        }
        if (_inventoryItems.Count > 1 && !selfdrop)
            _isAlreadyDropped = true;
        _inventoryItems[hand].DeEquip();
        _inventoryItems[hand].transform.SetParent(null);
        _inventoryItems.Remove(hand);
        return true;
    }
    
    public bool SwitchUseMode(GameObject hand)
    {
        if (_isAlreadySwitched)
        {
            _isAlreadySwitched = false;
            return false;
        }
        if (_inventoryItems.Count > 1)
            _isAlreadySwitched = true;

        _inventoryItems[hand].ModeSwitch();
        return true;
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
