using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{
    [SerializeField] private GameObject _bulletGameObject;
    [SerializeField] private Transform _bulletExitPoint;
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

    public override void Use()
    {
        if (_magazineSize < 0)
            return;
        GameObject currentBullet = _bulletPool[_magazineSize - 1];
        _magazineSize -= 1;
        currentBullet.SetActive(true);
        currentBullet.transform.position = _bulletExitPoint.position;
        currentBullet.transform.rotation = _bulletExitPoint.rotation;
        currentBullet.GetComponent<Bullet>().Fire();
    }
}
