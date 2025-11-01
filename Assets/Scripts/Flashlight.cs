using UnityEngine;

public class Flashlight : Item
{
    private Light _light;
    private void Awake()
    {
        _light = GetComponent<Light>();
        Rigidbody = GetComponent<Rigidbody>();
        BoxCollider = GetComponent<BoxCollider>();
    }
    public override void Use()
    {
        _light.enabled = _light.enabled ? false : true;
    }
}
