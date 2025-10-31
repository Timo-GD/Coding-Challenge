using UnityEngine;

public class Flashlight : Item
{
    private Light _light;

    private void Awake()
    {
        _light = GetComponent<Light>();
    }
    public override void Use()
    {
        _light.enabled = _light.enabled ? false : true;
    }
}
