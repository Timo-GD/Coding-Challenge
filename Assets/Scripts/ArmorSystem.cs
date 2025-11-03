using UnityEngine;

public class ArmorSystem : MonoBehaviour
{
    private Transform _headArmor;
    private Transform _chestArmor;
    private Transform _legArmor;
    private Transform _feetArmor;
    public bool Equip(string armorPiece)
    {
        if (_headArmor != null && armorPiece == "Head")
        {
            return true;
        }
        else if (_chestArmor != null && armorPiece == "Chest")
        {
            return true;

        }
        else if (_legArmor != null && armorPiece == "Leg")
        {
            return true;

        }
        else if (_feetArmor != null && armorPiece == "Feet")
        {
            return true;

        }
        else
        {
            return false;
        }
    }
}
