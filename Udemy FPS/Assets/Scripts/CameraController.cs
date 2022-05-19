using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    [SerializeField]
    Transform _cameraPoz;
    [SerializeField]
    Camera _camera;
    [SerializeField]
    float _startFOV, _targetFOV, _FOVspeed;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    private void Start()
    {
        _startFOV = 60;
        _targetFOV = _startFOV;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position= _cameraPoz.position;
        transform.rotation= _cameraPoz.rotation;
        _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _targetFOV, _FOVspeed * Time.deltaTime);
    }
    public void Focus(float fov)
    {
        _targetFOV = _startFOV - fov;
    }
    public void UnFocus()
    {
        _targetFOV = _startFOV;
    }
}
