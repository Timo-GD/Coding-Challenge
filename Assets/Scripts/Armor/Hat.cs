using CodingChallenge.Inventory;
using UnityEngine;

namespace CodingChallenge.Armor
{
    public class Hat : ArmorItem
    {
        public override void Equip(Hand hand)
        {
            //Increases the scale of the hat to allow the player to see it when it is picked up;
            if(hand == null)
                transform.localScale = new Vector3(1.5f, .25f, 1.5f);
            base.Equip(hand);
        }

        public override void DeEquip()
        {
            //Decreases the scale of the hat when it is dropped;
            transform.localScale = new Vector3(.5f, .25f, .5f);
            base.DeEquip();
        }
    }
}
