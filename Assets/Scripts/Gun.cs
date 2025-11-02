using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{
    [SerializeField] private GameObject _bulletGameObject;
    [SerializeField] private Transform _bulletExitPoint;
    private Transform _parentTransform;
    private List<GameObject> _bulletPool = new();
    private int _magazineSize;
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
    public override void Equip()
    {
        base.Equip();
        _parentTransform = GetComponentInParent<Target>().gameObject.transform;
    }

    public override void Use()
    {
        if (_magazineSize <= 0)
            return;
        GameObject currentBullet = _bulletPool[_magazineSize - 1];
        _magazineSize -= 1;
        currentBullet.SetActive(true);

        currentBullet.transform.position = _bulletExitPoint.position;
        currentBullet.transform.rotation = _bulletExitPoint.rotation;
        if (Physics.Raycast(_parentTransform.position, transform.forward, out RaycastHit rayCastHit, Mathf.Infinity))
            currentBullet.GetComponent<Bullet>().Fire(rayCastHit.point);
        else
            currentBullet.GetComponent<Bullet>().Fire(_parentTransform.forward * 1000);
    }
    
    public void Reload()
    {
        _magazineSize = 30;
    }
}
