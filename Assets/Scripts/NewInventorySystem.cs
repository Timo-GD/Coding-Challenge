using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewInventorySystem : MonoBehaviour
{
    [SerializeField] private InputAction _switchItem;
    private Dictionary<GameObject, Item> _inventoryItems = new();

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

        if (_switchItem.ReadValue<float>() == 0)
            return;

        PickupCast[] hands = GetComponentsInChildren<PickupCast>();

        if (hands.Length == 0)
            return;

        Item[] oldItems = new Item[hands.Length];

        for (int i = 0; i < hands.Length; i++)
            oldItems[i] = hands[i].OldItem;

        for(int i = 0; i < hands.Length; i++)
        {
            int targetIndex;

            if (_switchItem.ReadValue<float>() > 0)
                targetIndex = (i + 1) % hands.Length;
            else
                targetIndex = (i - 1 + hands.Length) % hands.Length;

            hands[targetIndex].SwitchItem(oldItems[i]);

            Debug.Log("Hand Item:   " + hands[targetIndex].gameObject);

            foreach (KeyValuePair<GameObject, Item> items in _inventoryItems)
            {
                Debug.Log("Items    " + items);
            }
            if (hands[targetIndex].HeldItem != null)
                _inventoryItems[hands[targetIndex].gameObject] = hands[targetIndex].HeldItem;
            else
                _inventoryItems.Remove(hands[targetIndex].gameObject);

            // if (_inventoryItems.ContainsKey(hands[targetIndex].gameObject) && hands[i].HeldItem != null)
            //     _inventoryItems[hands[targetIndex].gameObject] = hands[i].HeldItem;
            // else if (_inventoryItems.ContainsKey(hands[targetIndex].gameObject) && hands[i].HeldItem == null)
            //     _inventoryItems.Remove(hands[targetIndex].gameObject);
            // else if (!_inventoryItems.ContainsKey(hands[targetIndex].gameObject) && hands[i].HeldItem != null)
            //     _inventoryItems[hands[targetIndex].gameObject] = hands[i].HeldItem;
            // else
            //     return;

            // if (hands[targetIndex].OldItem == null)
            //     _inventoryItems.Remove(hands[targetIndex].gameObject);
            // else
            //     _inventoryItems[hands[targetIndex].gameObject] = hands[i].HeldItem;
            
        }

        // PickupCast[] hands = GetComponentsInChildren<PickupCast>();
        // if (hands.Length == 0)
        //     return;
        // for (int i = 0; i < hands.Length; i++)
        // {
        //     Debug.Log("Curent hand: " + hands[i]);
        //     if (_switchItem.ReadValue<float>() > 0)
        //         hands[i + 1 >= hands.Length ? 0 : i + 1].SwitchItem(hands[i].OldItem);
        //     else if (_switchItem.ReadValue<float>() < 0)
        //         hands[i - 1 < 0 ? hands.Length - 1 : i - 1].SwitchItem(hands[i].OldItem);


        //     /*(Debug.Log("Inventory:   " + hands[i].gameObject + "   " + hands[i].HeldItem);
        //     if (hands[i].HeldItem == hands[i + 1 >= hands.Length ? 0 : i + 1].HeldItem)
        //     {
        //         hands[i].SwitchItem(null);
        //         continue;
        //     }

        //     if (_switchItem.ReadValue<float>() > 0)
        //         hands[i].SwitchItem(hands[i + 1 >= hands.Length ? 0 : i + 1].HeldItem);
        //     else if (_switchItem.ReadValue<float>() < 0)
        //         hands[i].SwitchItem(hands[i - 1 < 0 ? hands.Length - 1 : i - 1].HeldItem);


        //     if (hands[i].HeldItem == null)
        //         _inventoryItems.Remove(hands[i].gameObject);
        //     else
        //         _inventoryItems[hands[i].gameObject] = hands[i].HeldItem;
        //         */
        // }
        // for (int i = 0; i < _inventoryItems.Count; i++)
        // {
        //     // Debug.Log("Inventory count:  " + _inventoryItems.Count);
        //     // Debug.Log("I:   " + i);
        //     // Debug.Log(_inventoryItems.ElementAt(i));
        //     // if (_switchItem.ReadValue<float>() > 0)
        //     //     Debug.Log(_inventoryItems[_inventoryItems.ElementAt(i + 1 == _inventoryItems.Count ? i : i + 1).Key]);
        //     // else if(_switchItem.ReadValue<float>() < 0)
        //     //     Debug.Log(_inventoryItems[_inventoryItems.ElementAt(i - 1).Key]);

        //     // Debug.Log("I:   " + i + "Dictionary:    " + _inventoryItems.ElementAt(i));

        //     if (_switchItem.ReadValue<float>() > 0)
        //         _inventoryItems[_inventoryItems.ElementAt(i).Key] = (i + 1 >= _inventoryItems.Count) ? _inventoryItems.ElementAt(0).Value : _inventoryItems.ElementAt(i + 1).Value;
        //     else if (_switchItem.ReadValue<float>() < 0)
        //         _inventoryItems[_inventoryItems.ElementAt(i).Key] = (i - 1 < 0) ? _inventoryItems.ElementAt(_inventoryItems.Count).Value : _inventoryItems.ElementAt(i - 1).Value;
        //     else
        //         continue;

        //     // if(i + 1 >= _inventoryItems.Count)
        //     // {
        //     //     _inventoryItems.Add()
        //     // }

        //     Debug.Log(_inventoryItems.ElementAt(i));

        //     if (_inventoryItems.ElementAt(i).Value == null)
        //         _inventoryItems.Remove(_inventoryItems.ElementAt(i).Key);

        //     // _inventoryItems[_inventoryItems.ElementAt(i).Key] = (i + 1 == _inventoryItems.Count) ? _inventoryItems.Remove(_inventoryItems.ElementAt(i).Key) : _inventoryItems.ElementAt(i + 1).Value;//_inventoryItems.ElementAt(i + 1 == _inventoryItems.Count ? i : i + 1).Value == null ? null : _inventoryItems.ElementAt(i == _inventoryItems.Count ? 0 : i + 1).Value;

        //     // _inventoryItems[_inventoryItems.ElementAt(i).Key].GetComponent<PickupCast>().SwitchItem(_inventoryItems.ElementAt(i).Value);

        // }
        

    }

    public bool Equip(GameObject hand, Item equipable)
    {
        if (_inventoryItems.ContainsKey(hand))
            return false;


        equipable.transform.SetParent(hand.transform, true);
        equipable.transform.position = hand.transform.position;
        equipable.transform.rotation = transform.rotation;
        _inventoryItems.Add(hand, equipable);

        Debug.Log(_inventoryItems.Keys);
        return true;
    }

    public void DeEquip(GameObject hand)
    {
        _inventoryItems[hand].DeEquip();
        _inventoryItems[hand].transform.SetParent(null);
        _inventoryItems.Remove(hand);
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
