using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField]
    float _movementSpeed, _gravityModifier,_jumpPower, _runSpeed ;
    [SerializeField]
    CharacterController _characterController;
    Vector3 _movementInput;
    [SerializeField]
    Transform _cameraTr;
    Animator _anim;

    [Header("ForJump")]
    bool _canDoubleJump;
    [SerializeField]
    Transform _checkGroundPoint;
    [SerializeField]
    LayerMask _whatIsGround;
    bool _outOfGround;

    [Header("PlayerPreferences")]
    public bool InvertX;
    public bool InvertY;
    public float MouseSensitivity;

    [Header("Fire")]
    Gun _currentGun;
    Transform _firePoz;

    [Header("Collect and SwitchWeapon")]
    [SerializeField]
    List<Gun> _guns = new List<Gun>();
    [SerializeField]
    List<Gun> _gunsCanCollect = new List<Gun>();
    int _currentGunIndex=0;

    [Header("Focus")]
    [SerializeField]
    Transform _focusInitTransform, _focusAfterTransform;
    Vector3 _focusInitPoz;
    [SerializeField]
    float _moveWeaponSpeed;
    [SerializeField]
    Transform _muzzleFlash;
    [SerializeField]
    float _maxViewAngle;

    bool bounce;
    float bounceAmmont;

    public AudioSource[] _steps;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        _focusInitPoz = _focusInitTransform.localPosition;
        ActivateWeapon();
        _characterController = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UiController.instance.pauseMenu.activeInHierarchy && !GameManager.instance._isLoading)
        {
            //_movementInput.x = Input.GetAxis("Horizontal") * Time.deltaTime;
            //_movementInput.z = Input.GetAxis("Vertical") * Time.deltaTime;

            // store y velocity

            float _yStore = _movementInput.y;

            Vector3 _vertMove = transform.forward * Input.GetAxis("Vertical");
            Vector3 _horiMove = transform.right * Input.GetAxis("Horizontal");

            _movementInput = _vertMove + _horiMove;
            _movementInput.Normalize();

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                _movementInput = _movementInput * _runSpeed;
            }
            else
                _movementInput = _movementInput * _movementSpeed;

            _movementInput.y = _yStore;
            _movementInput.y += Physics.gravity.y * _gravityModifier * Time.deltaTime;

            if (_characterController.isGrounded)
            {
                _movementInput.y = Physics.gravity.y * _gravityModifier * Time.deltaTime;
                _canDoubleJump = false;
                _outOfGround = false;
            }
            if (!_characterController.isGrounded)
            {
                foreach (AudioSource i in _steps)
                {
                    i.Stop();
                }
            }

            if (Physics.OverlapSphere(_checkGroundPoint.position, 0.25f, _whatIsGround).Length > 0 && Input.GetButtonDown("Jump"))
            {
                foreach (AudioSource i in _steps)
                {
                    i.Stop();
                }
                AudioManager.instance.PlaySFX(8);
                _outOfGround = true;
                _movementInput.y = _jumpPower;
                _canDoubleJump = true;
            }
            else if (_canDoubleJump && Input.GetButtonDown("Jump"))
            {
                AudioManager.instance.PlaySFX(8);
                _movementInput.y = _jumpPower;
                _canDoubleJump = false;
            }
            if (bounce)
            {
                _movementInput.y = bounceAmmont;
                _canDoubleJump = true;
                bounce = false;
            }
            _characterController.Move(_movementInput * Time.deltaTime);

            //controll camera rotation
            Vector2 _mousePoz = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxis("Mouse Y")) * MouseSensitivity;
            if (InvertX)
            {
                _mousePoz.x = -_mousePoz.x;
            }
            if (InvertY)
            {
                _mousePoz.y = -_mousePoz.y;
            }
            transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + _mousePoz.x, transform.rotation.eulerAngles.z);
            _cameraTr.rotation = Quaternion.Euler(_cameraTr.rotation.eulerAngles + new Vector3(-_mousePoz.y, 0f, 0f));

            if (_cameraTr.rotation.eulerAngles.x>_maxViewAngle && _cameraTr.rotation.eulerAngles.x < 180f)
            {
                _cameraTr.rotation = Quaternion.Euler(_maxViewAngle, _cameraTr.rotation.eulerAngles.y, _cameraTr.rotation.eulerAngles.z);
            }
            else
                if(_cameraTr.rotation.eulerAngles.x < 360f-_maxViewAngle && _cameraTr.rotation.eulerAngles.x > 180f)
                     _cameraTr.rotation = Quaternion.Euler(360f-_maxViewAngle, _cameraTr.rotation.eulerAngles.y,_cameraTr.rotation.eulerAngles.z);

            _anim.SetFloat("moveSpeed", _movementInput.magnitude);
            _anim.SetBool("onAir", _outOfGround);

            _muzzleFlash.gameObject.SetActive(false);
            _muzzleFlash.position = _firePoz.position;
            // fire manager
            if (_currentGun.FireRateCount() <= 0)
            {
                if (Input.GetMouseButtonDown(0) && !_currentGun.CanAutoFire())
                {
                    RaycastHit hit;


                    if (Physics.Raycast(_cameraTr.position, _cameraTr.forward, out hit, 50f))
                    {
                        if (Vector3.Distance(_cameraTr.position, hit.point) > 2)
                        {
                            _firePoz.LookAt(hit.point);
                        }
                    }
                    else
                    {
                        _firePoz.LookAt(_cameraTr.position + _cameraTr.forward * 30f);
                    }
                    fireShot();

                }
                if (Input.GetMouseButton(0) && _currentGun.CanAutoFire())
                {
                    RaycastHit hit;

                    if (Physics.Raycast(_cameraTr.position, _cameraTr.forward, out hit, 50f))
                    {
                        if (Vector3.Distance(_cameraTr.position, hit.point) > 3)
                        {
                            _firePoz.LookAt(hit.point);
                        }
                    }
                    else
                    {
                        _firePoz.LookAt(_cameraTr.position + _cameraTr.forward * 30f);
                    }
                    fireShot();
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Switchweapon_Q();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Switchweapon_E();
            }
            if (Input.GetMouseButtonDown(1))
            {
                CameraController.instance.Focus(_currentGun.GetFOV());
            }
            if (Input.GetMouseButton(1))
            {
                _focusInitTransform.localPosition = Vector3.MoveTowards(_focusInitTransform.localPosition, _focusAfterTransform.localPosition, _moveWeaponSpeed * Time.deltaTime);
            }
            else
                _focusInitTransform.localPosition = Vector3.MoveTowards(_focusInitTransform.localPosition, _focusInitPoz, _moveWeaponSpeed * Time.deltaTime);
            if (Input.GetMouseButtonUp(1))
            {
                CameraController.instance.UnFocus();
            }
        }
        else
        {
            foreach (AudioSource i in _steps)
            {
                i.Stop();
            }
        }
    }
    void fireShot()
    {
        if (_currentGun.AmmoCount() > 0)
        {
            _muzzleFlash.position = _firePoz.position;
            _muzzleFlash.gameObject.SetActive(true);
            _currentGun.DecreaseAmmo();
            Instantiate(_currentGun.Bullet(), _firePoz.position, _firePoz.rotation);
            _currentGun.ResetRateCount();
        }
    }

    public void AmmoPickUpCollected()
    {
        _currentGun.AmmoPickUp();
    }
    void Switchweapon_E()
    {
        _currentGun.gameObject.SetActive(false);

        _currentGunIndex++;
        _currentGunIndex %= _guns.Count;

        ActivateWeapon();
    }
    void Switchweapon_Q()
    {
        _currentGun.gameObject.SetActive(false);

        _currentGunIndex--;
        if (_currentGunIndex < 0)
        {
            _currentGunIndex = _guns.Count-1;
        }
        ActivateWeapon();
    }
    void ActivateWeapon()
    {
        _currentGun = _guns[_currentGunIndex];
        _currentGun.gameObject.SetActive(true);
        _firePoz = _currentGun.GetFirePoz();
        UiController.instance.UpdateAmmoUI(_currentGun.AmmoCount(), _currentGun.MaxAmmo());
    }
    public void AddGun(string gunname)
    {
        bool collected=false;

        foreach (Gun item in _gunsCanCollect)
        {
            if (item.GetGunName() == gunname)
            {
                collected = true;
                _guns.Add(item);
                _gunsCanCollect.Remove(item);
                break;
            }
        }
    }

    public void Bounce(float bounceForce)
    {
        bounce = true;
        bounceAmmont= bounceForce;
    }
}
