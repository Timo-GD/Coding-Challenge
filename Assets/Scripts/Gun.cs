using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{
    [SerializeField] private GameObject _bulletGameObject;
    [SerializeField] private Transform _bulletExitPoint;
    private List<GameObject> _bulletPool = new();
    private int _magazineSize;
    private bool _isAutoFireMode;
    private bool _isAutoFiring;
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        BoxColliders = GetComponentsInChildren<BoxCollider>();
        _magazineSize = 30;
    }

    private void Start()
    {
        for (int i = 1; i <= _magazineSize; i++)
        {
            GameObject bullet = Instantiate(_bulletGameObject);
            bullet.SetActive(false);
            _bulletPool.Add(bullet);
        }
    }

    private IEnumerator AutomaticFire()
    {
        while (_isAutoFiring)
        {
            Fire();
            yield return new WaitForSeconds(0.25f);
        }
    }
    
    private void Fire()
    {
        if (_magazineSize <= 0)
            return;
        GameObject currentBullet = _bulletPool[_magazineSize - 1];
        _magazineSize -= 1;
        currentBullet.SetActive(true);

        currentBullet.transform.position = _bulletExitPoint.position;
        currentBullet.transform.rotation = _bulletExitPoint.rotation;
        if (Physics.Raycast(transform.parent.position, transform.forward, out RaycastHit rayCastHit, Mathf.Infinity))
            currentBullet.GetComponent<Bullet>().Fire(rayCastHit.point);
        else
            currentBullet.GetComponent<Bullet>().Fire(transform.parent.forward * 1000);
    }

    public override IEnumerator Using()
    {
        if (!_isAutoFireMode)
        {
            Fire();
            yield break;
        }
        _isAutoFiring = true;
        while (_isAutoFiring)
        {
            Fire();
            yield return new WaitForSeconds(0.25f);
        }
        // StartCoroutine(AutomaticFire());
    }

    public override void StopUsing()
    {
        _isAutoFiring = false;
    }

    public override void ModeSwitch()
    {
        _isAutoFireMode = _isAutoFireMode ? false : true;
    }
    
    public void Reload()
    {
        _magazineSize = 30;
    }
}
