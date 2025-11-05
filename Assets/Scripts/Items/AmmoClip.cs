using System.Collections;
using UnityEngine;

public class AmmoClip : Item
{
    private bool _isEmpty;
    
    private void Reload()
    {
        _isEmpty = false;
    }

    public override IEnumerator Using()
    {
        //TODO
        //Make this prettier if possible;

        if (transform.parent.GetComponentInChildren<Gun>() == null || _isEmpty)
            yield break;
            
        transform.parent.GetComponentInChildren<Gun>().Reload();
        Hand.GetComponent<PickupCast>().DropItem();
        _isEmpty = true;

        yield return new WaitForSeconds(5f);
        Reload();
    }
}
