using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewInventorySystem : MonoBehaviour
{
    [SerializeField] private InputAction _switchItem;
    private Dictionary<GameObject, Item> _inventoryItems = new();



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


}
