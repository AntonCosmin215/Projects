using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    GameObject _bullet;

    [SerializeField]
    Transform _gun,_firePoz1,_firePoz2;

    [SerializeField]
    float _distanceToPlayer, _timeToShoot, _rotateSpeed;
    float _timeCount = 1.5f;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance._isLoading == false)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position + new Vector3(0, 1.3f, 0)) < _distanceToPlayer)
            {
                _gun.LookAt(PlayerController.instance.transform.position + new Vector3(0, 1.2f, 0));

                _timeCount -= Time.deltaTime;
                if (_timeCount <= 0)
                {
                    Instantiate(_bullet, _firePoz1.position, _firePoz1.rotation);
                    Instantiate(_bullet, _firePoz2.position, _firePoz2.rotation);
                    _timeCount = _timeToShoot;
                }
            }
            else
            {
                _timeCount = _timeToShoot;
                _gun.transform.rotation = Quaternion.Lerp(_gun.rotation, Quaternion.Euler(0, _gun.rotation.eulerAngles.y + 10f, 0), _rotateSpeed * Time.deltaTime);
            }
        }
    }
}
