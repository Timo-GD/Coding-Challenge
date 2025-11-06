using System.Collections.Generic;
using UnityEngine;

public class ArmorSystem : MonoBehaviour
{
    private Dictionary<ArmorType, Armor> _armorPieces;
    private void Awake()
    {
        
    }
    private void LateUpdate()
    {
        
    }
    public bool Equip(Armor armorPiece)
    {
        if (!_armorPieces.ContainsKey(armorPiece.GetArmorType()))
            return false;

        _armorPieces.Add(armorPiece.GetArmorType(), armorPiece);
        armorPiece.transform.SetParent(null);
        return true;
    }
}
