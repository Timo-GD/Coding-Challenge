using System.Collections;
using UnityEngine;

public class AmmoClip : Item
{
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        BoxColliders = GetComponentsInChildren<BoxCollider>();
    }

    public override IEnumerator Using()
    {
        //TODO
        //Make this prettier if possible;
        if (transform.parent.GetComponentInChildren<Gun>() == null)
            yield break;
        transform.parent.GetComponentInChildren<Gun>().Reload();
        Destroy(gameObject);
        yield break;
    }
}
