using System.Collections;
using UnityEngine;

public class AmmoClip : Item
{
    private bool _isEmpty;
    
    public override IEnumerator Using()
    {
        //TODO
        //Make this prettier if possible;

        if (transform.parent.GetComponentInChildren<Gun>() == null || _isEmpty)
            yield break;
            
        transform.parent.GetComponentInChildren<Gun>().Reload();
        _hand.DropItem(true);
        _isEmpty = true;

        yield return new WaitForSeconds(5f);
        Reload();
    }
    private void Reload()
    {
        _isEmpty = false;
    }

}
