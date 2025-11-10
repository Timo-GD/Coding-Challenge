using System;
using CodingChallenge.Items;

namespace CodingChallenge.Armor
{
    public class ArmorItem : Item
    {
        /// <summary>
        /// Returns the armor type that is linked to the tag of the gameobject;
        /// </summary>
        /// <returns>The armor type that is linked to the tag of the gameobject;</returns>
        public virtual ArmorType GetArmorType()
        {
            return (ArmorType)Enum.Parse(typeof(ArmorType), tag);
        }
    }
}
