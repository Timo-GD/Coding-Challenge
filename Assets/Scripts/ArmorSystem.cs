using System.Collections.Generic;
using UnityEngine;

public class ArmorSystem : MonoBehaviour
{
    private Dictionary<ArmorType, Item> _armorPieces;
    private void Awake()
    {
        
    }
    private void LateUpdate()
    {
        
    }
    public bool Equip(Item armorPiece)
    {
        // _armorPieces.ContainsKey((ArmorType)armorPiece.ArmorType);
        return false;
    }
}
