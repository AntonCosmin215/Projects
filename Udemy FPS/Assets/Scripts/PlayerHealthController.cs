using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    [SerializeField]
    int _healthMAX,_currentHealth;
    [SerializeField]
    float _invincibleLength;
    float _invincibleCount;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance._isLoading = false;
        _currentHealth = _healthMAX;
        UiController.instance.UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (_invincibleCount > 0)
        {
            _invincibleCount -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 && _invincibleCount <=0f && other.GetComponent<Bullet>().IsTargetPlayer())
        {
            _invincibleCount = _invincibleLength;
            DealDamage(other.GetComponent<Bullet>().GetDamage());
        }else
            if (other.gameObject.layer == 10)
        {
            _invincibleCount = _invincibleLength;
            DealDamage(other.GetComponent<Explosion>().GetDamage());
        }
    }

    void DealDamage(int damage)
    {
        _currentHealth -= damage;
        AudioManager.instance.PlaySFX(7);
        UiController.instance.GetDamage();
        UiController.instance.UpdateHealth();
        if (_currentHealth <=0)
        {
            AudioManager.instance.StopBGM();
            AudioManager.instance.PlaySFX(6);
            gameObject.SetActive(false);
            _currentHealth = 0;
            GameManager.instance.ReloadLevelPlayerDied();
        }
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }
    public int GetMaxHealth()
    {
        return _healthMAX;
    }
    public void AddHealth(int howmuch)
    {
        _currentHealth += howmuch;
        if (_currentHealth > _healthMAX)
            _currentHealth = _healthMAX;
        UiController.instance.UpdateHealth();
    }
}
