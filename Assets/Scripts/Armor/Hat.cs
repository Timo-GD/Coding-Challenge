using CodingChallenge.Armor;
using CodingChallenge.Inventory;
using UnityEngine;

namespace CodingChallenge.Armor
{
    public class Hat : ArmorItem
    {
        public override void Equip(Hand hand)
        {
            transform.localScale = new Vector3(1.5f, .25f, 1.5f);
            base.Equip(hand);
        }

        public override void DeEquip()
        {
            transform.localScale = new Vector3(.5f, .25f, .5f);
            base.DeEquip();
        }
    }
}
