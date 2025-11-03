using UnityEngine;

public class ArmorSystem : MonoBehaviour
{
    [SerializeField] Transform _headArmorTransform;
    [SerializeField] Transform _chestArmorTransform;
    [SerializeField] Transform _legArmorTransform;
    [SerializeField] Transform _feetArmorTransform;
    private Transform _headArmor;
    private Transform _chestArmor;
    private Transform _legArmor;
    private Transform _feetArmor;

    private void LateUpdate()
    {
        if (_headArmor != null || _chestArmor != null || _legArmor != null || _feetArmor != null)
            UpdateArmor();
    }

    private void UpdateArmor()
    {
        if (_headArmor != null)
        {
            _headArmor.position = _headArmorTransform.position;
            _headArmor.rotation = _headArmorTransform.rotation;
        }

        if (_chestArmor != null)
        {
            _chestArmor.position = _chestArmorTransform.position;
            _chestArmor.rotation = _chestArmorTransform.rotation;
        }

        if (_legArmor != null)
        {
            _legArmor.position = _legArmorTransform.position;
            _legArmor.rotation = _legArmorTransform.rotation;
        }

        if (_feetArmor != null)
        {
            _feetArmor.position = _feetArmorTransform.position;
            _feetArmor.rotation = _feetArmorTransform.rotation;
        }
    }

    public bool Equip(GameObject armorPiece)
    {
        if (_headArmor == null && armorPiece.tag == "Head")
        {
            _headArmor = armorPiece.transform;
            _headArmor.parent = transform;
            return true;
        }
        else if (_chestArmor == null && armorPiece.tag == "Chest")
        {
            _chestArmor = armorPiece.transform;
            _chestArmor.parent = transform;
            return true;

        }
        else if (_legArmor == null && armorPiece.tag == "Leg")
        {
            _legArmor = armorPiece.transform;
            _legArmor.parent = transform;
            return true;

        }
        else if (_feetArmor == null && armorPiece.tag == "Feet")
        {
            _feetArmor = armorPiece.transform;
            _feetArmor.parent = transform;
            return true;

        }
        else
        {
            return false;
        }
    }
}
