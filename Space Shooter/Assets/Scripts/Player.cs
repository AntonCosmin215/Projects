using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject laser;
    [SerializeField]
    float _speedHorizontal,_speedVertical;
    float _horizontal, _vertical;
    [SerializeField]
    float _fireCooldown;
    float nextFire;
    [SerializeField]
    bool _canTripleShoot;
    [SerializeField]
    int _speedBoost;
    int _powerUpType;

    [Header("Health")]
    [SerializeField]
    int _playerhealth;
    public GameObject playerExplosion;

    [Header("Shield")]
    bool _shieldActive;
    [SerializeField]
    GameObject _shield;

    [Header("Sounds")]
    [SerializeField]
    AudioClip _collect;
    [SerializeField]
    AudioSource _laserSound;
    [SerializeField]
    AudioClip _explosion;

    [Header("Damaged")]
    [SerializeField]
    GameObject[] damaged;
    void Start()
    {
        transform.position = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<powerup>() != null)
        {
            _powerUpType = collision.gameObject.GetComponent<powerup>().GetPowerUpType();
            switch (_powerUpType)
            {
                case 0:
                    _canTripleShoot = true;
                    StartCoroutine(TripleShopPowerDownCo());
                    break;
                case 1:
                    addSpeed(_speedBoost);
                    StartCoroutine(SpeedPowerDownCo());
                    break;
                case 2:
                    _shieldActive= true;
                    _shield.SetActive(true);
                    break;
                default:
                    break;
            }
            AudioSource.PlayClipAtPoint(_collect, Camera.main.transform.position);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Enemy"))
        {
            Damage();
        }
    }

    void Damage()
    {
        if (!_shieldActive)
        {
            _playerhealth--;
            switch (_playerhealth)
            {
                case 2:
                    damaged[0].SetActive(true);
                    break;
                case 1:
                    damaged[1].SetActive(true);
                    damaged[0].SetActive(true);
                    break;
                default:
                    break;
            }
            UiController.Instance.UpdateLives(_playerhealth);
            if (_playerhealth <= 0)
            {
                Instantiate(playerExplosion, transform.position, transform.rotation);
                AudioSource.PlayClipAtPoint(_explosion, Camera.main.transform.position);
                Destroy(gameObject);
            }
        }else
            NoMoreShield();
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
    }
    void addSpeed(int speedboost)
    {
        _speedHorizontal += speedboost;
        _speedVertical += speedboost;
    }
    void NoMoreShield()
    {
        _shieldActive = false;
        _shield.SetActive(false);
    }
    IEnumerator SpeedPowerDownCo()
    {
        yield return new WaitForSeconds(5);
        addSpeed(-_speedBoost);
    }
    IEnumerator TripleShopPowerDownCo()
    {
        yield return new WaitForSeconds(5);
        _canTripleShoot = false;
    }
    void Shoot() {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && Time.time > nextFire)
        {
            _laserSound.Play();
            nextFire = Time.time + _fireCooldown;
            if (!_canTripleShoot)
            {
                Instantiate(laser, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(laser, transform.position + new Vector3(0.552f, -0.106f, 0), Quaternion.identity);
                Instantiate(laser, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
                Instantiate(laser, transform.position + new Vector3(-0.552f, -0.106f, 0), Quaternion.identity);
            }
        }
    }
    void Movement()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * Time.deltaTime * _speedHorizontal * _horizontal);
        transform.Translate(Vector3.up * Time.deltaTime * _speedVertical * _vertical);
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else
            if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }
        if (transform.position.x > 8.75f)
        {
            transform.position = new Vector3(8.75f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.75f)
        {
            transform.position = new Vector3(-8.75f, transform.position.y, 0);
        }
    }
}
