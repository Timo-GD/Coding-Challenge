using System.Collections;
using UnityEngine;

public class Rock : Item
{
    private bool _isHeld;
    private int _throwForce;
    private int _maxThrowForce = 10;
    private int _minThrowForce = 5;
    
    private void FixedUpdate()
    {
        if (Rigidbody.useGravity)
            Rigidbody.AddForce(Physics.gravity * Rigidbody.mass);
            
    }

    public override IEnumerator Using()
    {
        _isHeld = true;
        while(_isHeld)
        {
            _throwForce++;
            _throwForce = Mathf.Clamp(_throwForce, _minThrowForce, _maxThrowForce);
            yield return new WaitForSeconds(.5f);
        }
    }

    public override void StopUsing()
    {
        if (!_isHeld)
            return;
        _isHeld = false;
        Rigidbody.AddForce(transform.parent.forward * _throwForce, ForceMode.Impulse);
        GetComponentInParent<InverntorySystem>().DeEquip(transform);
        _throwForce = 4;
    }
    
}
