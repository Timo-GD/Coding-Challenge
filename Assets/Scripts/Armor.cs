using System;
using UnityEngine;

public class Armor : Item
{
    public int _armorType;
    public override void Awake()
    {
        Debug.Log(tag);
        _armorType = (int)Enum.Parse(typeof(ArmorType), tag);
        Debug.Log(_armorType);
        base.Awake();
    }
}
