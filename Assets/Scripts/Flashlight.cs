using UnityEngine;

public class Flashlight : Item
{
    private Light _light;
    private void Awake()
    {
        _light = GetComponentInChildren<Light>();
        Rigidbody = GetComponent<Rigidbody>();
        BoxColliders = GetComponentsInChildren<BoxCollider>();
    }
    public override void Use()
    {
        _light.enabled = _light.enabled ? false : true;
    }
}
