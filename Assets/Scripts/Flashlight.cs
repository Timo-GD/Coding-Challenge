using UnityEngine;

public class Flashlight : Item
{
    private Light _light;
    private Rigidbody _rigidBody;
    private BoxCollider _boxCollider;
    private bool _isEquiped;

    private void Awake()
    {
        _light = GetComponent<Light>();
        _rigidBody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
    }
    public override void Use()
    {
        _light.enabled = _light.enabled ? false : true;
    }

    public override void Equip()
    {
        _rigidBody.useGravity = false;
        _boxCollider.enabled = false;
    }

    public override void DeEquip()
    {
        _rigidBody.useGravity = true;
        _boxCollider.enabled = true;
    }
}
