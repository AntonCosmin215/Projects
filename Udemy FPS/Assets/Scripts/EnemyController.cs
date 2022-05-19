using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    bool _chasing;
    [SerializeField]
    float _distanceToChase = 10f, _distanceToLose = 15f, _distanceToStop = 2f;

    Vector3 _targetPoint, _startPoint;

    [SerializeField]
    NavMeshAgent _agent;

    [SerializeField]
    float _keepChasingTime = 5f;
    float _chaseCounter;

    [SerializeField]
    GameObject _bullet;
    [SerializeField]
    Transform _firePoint;

    [SerializeField]
    float _fireRate, _waitBetweenShots = 2f, _timeToShoot = 1f;
    float _fireCount, _shotWaitCounter, _shootTimeCounter;

    [SerializeField]
     Animator _anim;


    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _startPoint = transform.position;

        _shootTimeCounter = _timeToShoot;
        _shotWaitCounter = _waitBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        _targetPoint = PlayerController.instance.transform.position;
        _targetPoint.y = transform.position.y;
        if (!GameManager.instance._isLoading)
        {
            if (!_chasing)
            {
                if (Vector3.Distance(transform.position, _targetPoint) < _distanceToChase)
                {
                    _chasing = true;

                    _shootTimeCounter = _timeToShoot;
                    _shotWaitCounter = _waitBetweenShots;
                }

                if (_chaseCounter > 0)
                {
                    _chaseCounter -= Time.deltaTime;

                    if (_chaseCounter <= 0)
                    {
                        _agent.destination = _startPoint;
                    }
                }

                if (_agent.remainingDistance < .25f)
                {
                    _anim.SetBool("isMoving", false);
                }
                else
                {
                    _anim.SetBool("isMoving", true);
                }
            }
            else
            {

                if (Vector3.Distance(transform.position, _targetPoint) > _distanceToStop)
                {
                    _agent.destination = _targetPoint;
                }
                else
                {
                    _agent.destination = transform.position;
                }

                if (Vector3.Distance(transform.position, _targetPoint) > _distanceToLose)
                {

                    _chasing = false;


                    _chaseCounter = _keepChasingTime;

                }
                if (_shotWaitCounter > 0)
                {
                    _shotWaitCounter -= Time.deltaTime;

                    if (_shotWaitCounter <= 0)
                    {
                        _shootTimeCounter = _timeToShoot;
                    }

                    _anim.SetBool("isMoving", true);
                }
                else
                {

                    if (PlayerController.instance.gameObject.activeInHierarchy)
                    {

                        _shootTimeCounter -= Time.deltaTime;

                        if (_shootTimeCounter > 0)
                        {
                            _fireCount -= Time.deltaTime;

                            if (_fireCount <= 0)
                            {
                                _fireCount = _fireRate;

                                _firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 1.2f, 0f));

                                //check the angle to the player
                                Vector3 targetDir = PlayerController.instance.transform.position - transform.position;
                                float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

                                if (Mathf.Abs(angle) < 30f)
                                {

                                    Instantiate(_bullet, _firePoint.position, _firePoint.rotation);

                                    _anim.SetTrigger("shoot");
                                }
                                else
                                {
                                    _shotWaitCounter = _waitBetweenShots;
                                }
                            }

                            _agent.destination = transform.position;
                        }
                        else
                        {
                            _shotWaitCounter = _waitBetweenShots;
                        }
                    }

                    _anim.SetBool("isMoving", false);
                }
            }
        }
    }
}
