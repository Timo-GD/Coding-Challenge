using System.Collections.Generic;
using UnityEngine;

namespace CodingChallenge.Armor
{
    public class ArmorSystem : MonoBehaviour
    {
        private Dictionary<ArmorItem, ArmorItem> _armorPieces = new();
        private ArmorItem[] _armorSlots;

        public bool Equip(ArmorItem armorPiece)
        {
            if (armorPiece == null)
                return false;

            for (int i = 0; i < _armorSlots.Length; i++)
            {
                if (armorPiece.GetArmorType() != _armorSlots[i].GetArmorType())
                    continue;

                if (_armorPieces.ContainsKey(_armorSlots[i]))
                    continue;

                armorPiece.Equip(null);
                armorPiece.transform.SetParent(_armorSlots[i].transform, true);
                _armorPieces.Add(_armorSlots[i], armorPiece);
                return true;
            }
            return false;
        }

        private void Awake()
        {
            _armorSlots = GetComponentsInChildren<ArmorItem>();
        }
        private void LateUpdate()
        {
            UpdateArmor();
        }

        private void UpdateArmor()
        {
            foreach (KeyValuePair<ArmorItem, ArmorItem> armor in _armorPieces)
            {
                armor.Value.transform.position = armor.Key.transform.position;
                armor.Value.transform.rotation = armor.Key.transform.rotation;
            }
        }


    }
}
