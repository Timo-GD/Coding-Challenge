using System;
using UnityEngine;

public class Armor : Item
{
    public virtual ArmorType GetArmorType()
    {
        return (ArmorType)Enum.Parse(typeof(ArmorType), tag);
    }
}
