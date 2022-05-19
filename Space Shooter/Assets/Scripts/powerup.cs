using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup : MonoBehaviour
{
    [SerializeField]
    float _downforce;
    [SerializeField]
    int _powerUpType;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _downforce);
    }

    public int GetPowerUpType()
    {
        return _powerUpType;
    }
}
