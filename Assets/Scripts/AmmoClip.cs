using UnityEngine;

public class AmmoClip : Item
{
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        BoxColliders = GetComponentsInChildren<BoxCollider>();
    }

    public override void Use()
    {
        //TODO
        //Make this prettier if possible;
        if (transform.parent.GetComponentInChildren<Gun>() == null)
            return;
        transform.parent.GetComponentInChildren<Gun>().Reload();
        Destroy(gameObject);
    }
}
