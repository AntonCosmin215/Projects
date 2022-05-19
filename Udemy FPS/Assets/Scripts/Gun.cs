using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    GameObject _bullet;
    [SerializeField]
    bool _canAutoFire;
    [SerializeField]
    float _fireRate,_fireRateCount;
    [SerializeField]
    int _ammoCount,_maxAmmo;
    [SerializeField]
    Transform _firePointPoz;
    [SerializeField]
    float _focursFOV;
    [SerializeField]
    string _gunName;
    // Start is called before the first frame update
    void Start()
    {
        _ammoCount = _maxAmmo;
        UiController.instance.UpdateAmmoUI(_ammoCount,_maxAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (_fireRateCount>0)
        {
            _fireRateCount -= Time.deltaTime;
        }
    }

    public GameObject Bullet()
    {
        return _bullet;
    }
    public bool CanAutoFire()
    {
        return _canAutoFire;
    }
    public float FireRate()
    {
        return _fireRate;
    }
    public float FireRateCount()
    {
        return _fireRateCount;
    }
    public void ResetRateCount()
    {
        _fireRateCount = _fireRate;
    }
    public int AmmoCount()
    {
        return _ammoCount;
    }
    public void DecreaseAmmo()
    {
        _ammoCount--;
        if (_ammoCount<0)
        {
            _ammoCount = 0;
        }
        UiController.instance.UpdateAmmoUI(_ammoCount, _maxAmmo);
    }
    public void AmmoPickUp()
    {
        _ammoCount = _maxAmmo;
        UiController.instance.UpdateAmmoUI(_ammoCount,_maxAmmo);
    }
    public int MaxAmmo()
    {
        return _maxAmmo;
    }
    public Transform GetFirePoz()
    {
        return _firePointPoz;
    }
    public float GetFOV()
    {
        return _focursFOV;
    }
    public string GetGunName()
    {
        return _gunName;
    }
}
