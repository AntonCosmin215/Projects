using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float _moveSpeed,_lifeTime;
    Rigidbody theRb;
    [SerializeField]
    GameObject _bulletEffect;
    [SerializeField]
    int _damage;
    [SerializeField]
    bool _targetIsPlayer;
    // Start is called before the first frame update
    void Start()
    {
        theRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        theRb.velocity = transform.forward * _moveSpeed;
        _lifeTime -= Time.deltaTime;
        if (_lifeTime<=0f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    { 
        Instantiate(_bulletEffect,transform.position + transform.forward * -_moveSpeed * Time.deltaTime,transform.rotation);
        Destroy(gameObject);
    }
    public int GetDamage()
    {
        return _damage;
    }
    public bool IsTargetPlayer()
    {
        return _targetIsPlayer;
    }
}
