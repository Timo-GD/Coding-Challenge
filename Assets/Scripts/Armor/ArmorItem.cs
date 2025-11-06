using System;
using CodingChallenge.Items;

namespace CodingChallenge.Armor
{
    public class ArmorItem : Item
    {
        public virtual ArmorType GetArmorType()
        {
            return (ArmorType)Enum.Parse(typeof(ArmorType), tag);
        }
    }
}
