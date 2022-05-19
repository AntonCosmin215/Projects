using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField]
    float _timeToChangeDirDefault;
    float _timeToChangeDir;
    [SerializeField]
    float _moveSpeed;
    private void Start()
    {
        _timeToChangeDir = _timeToChangeDirDefault;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * _moveSpeed * Time.deltaTime);
        _timeToChangeDir -= Time.deltaTime;
        if (_timeToChangeDir<=0f)
        {
            _moveSpeed = -1 * _moveSpeed;
            _timeToChangeDir = _timeToChangeDirDefault;
        }
    }
}
