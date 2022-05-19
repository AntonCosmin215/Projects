using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiController : MonoBehaviour
{
    public static UiController instance;
    [SerializeField]
    Text _healthText;
    [SerializeField]
    Slider _healthSlider;
    [SerializeField]
    Text _ammoText;
    [SerializeField]
    Slider _ammoSlider;
    [SerializeField]
    Image _damageImage;
    [SerializeField]
    float _fadeAmount, _fadeSpeed;
    public GameObject pauseMenu;
    [Header("LoadingScreen")]
    public Image loadingScreen;
    public float fadespeed;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    private void Start()
    {
        _healthSlider.maxValue = PlayerHealthController.instance.GetMaxHealth();
        loadingScreen.color = new Color(loadingScreen.color.r, loadingScreen.color.g, loadingScreen.color.b,1);
    }
    public void UpdateHealth()
    {
        _healthSlider.value = PlayerHealthController.instance.GetCurrentHealth();
        _healthText.text = "HEALTH: " + (PlayerHealthController.instance.GetCurrentHealth()*10).ToString() + "/"+(PlayerHealthController.instance.GetMaxHealth()*10).ToString();
    }
    public void UpdateAmmoUI(int currentAmmo, int maxAmmo)
    {
        _ammoSlider.maxValue = maxAmmo;
        _ammoSlider.value = currentAmmo;
        _ammoText.text = "AMMO: "+ currentAmmo.ToString()+"/"+ maxAmmo.ToString();
    }

    public void GetDamage()
    {
        _damageImage.color = new Color(_damageImage.color.r, _damageImage.color.g, _damageImage.color.b, _fadeAmount);
    }
    private void Update()
    {
        if(_damageImage.color.a > 0)
        {
            _damageImage.color = new Color(_damageImage.color.r, _damageImage.color.g, _damageImage.color.b, Mathf.MoveTowards(_damageImage.color.a, 0f, _fadeSpeed * Time.deltaTime));
        }
        if (GameManager.instance._isLoading)
        {
            loadingScreen.color = new Color(loadingScreen.color.r, loadingScreen.color.g, loadingScreen.color.b, Mathf.MoveTowards(loadingScreen.color.a, 1, fadespeed * Time.deltaTime));
        }
        else
        {
            loadingScreen.color = new Color(loadingScreen.color.r, loadingScreen.color.g, loadingScreen.color.b, Mathf.MoveTowards(loadingScreen.color.a, 0, fadespeed * Time.deltaTime));
        }
    }
}
