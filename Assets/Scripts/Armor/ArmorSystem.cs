using System.Collections.Generic;
using UnityEngine;

namespace CodingChallenge.Armor
{
    public class ArmorSystem : MonoBehaviour
    {
        private Dictionary<ArmorItem, ArmorItem> _armorPieces = new();
        private ArmorItem[] _armorSlots;

        /// <summary>
        /// Equips the armor piece on the slot corresponding with the armor type;
        /// </summary>
        /// <param name="armorPiece">The armorpiece that needs to be equiped;</param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates the position of the ArmorItem to prevent any desync issues;
        /// </summary>
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
