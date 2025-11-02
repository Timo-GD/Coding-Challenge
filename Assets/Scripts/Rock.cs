using System.Collections;
using UnityEngine;

public class Rock : Item
{
    private bool _isHeld;
    private int _throwForce;
    private int _maxThrowForce = 10;
    private int _minThrowForce = 1;
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        BoxColliders = GetComponentsInChildren<BoxCollider>();
    }

    public override IEnumerator Using()
    {
        _isHeld = true;
        while(_isHeld)
        {
            _throwForce++;
            _throwForce = Mathf.Clamp(_throwForce, _minThrowForce, _maxThrowForce);
            Debug.Log(_throwForce);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public override void StopUsing()
    {
        _isHeld = false;
        Rigidbody.AddForce(transform.parent.forward * _throwForce, ForceMode.Impulse);
        GetComponentInParent<InverntorySystem>().DeEquip();
    }
    
}
