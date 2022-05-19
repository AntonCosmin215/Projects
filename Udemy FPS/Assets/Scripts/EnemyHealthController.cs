using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField]
    int _lives;
    bool _canTakeDamage = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 && _canTakeDamage && !other.GetComponent<Bullet>().IsTargetPlayer())
        {
            //Debug.Log("Got Hit - Target : Enemy");
            _canTakeDamage = false;
            float difPozY = other.transform.position.y - transform.position.y;
            if (difPozY >= 0.325f && difPozY <= 0.874f)
            {
                DealDamage(other.gameObject.GetComponent<Bullet>().GetDamage()*2);
            }
            else
                DealDamage(other.gameObject.GetComponent<Bullet>().GetDamage());
            StartCoroutine(CantakeDamage());
        }else
             if (other.gameObject.layer == 10)
        {
            _canTakeDamage = false;
            DealDamage(other.GetComponent<Explosion>().GetDamage());
            StartCoroutine(CantakeDamage());
        }
    }
    IEnumerator CantakeDamage()
    {
        yield return new WaitForSeconds(0.1f);
        _canTakeDamage = true;
    }
    void DealDamage(int bulletdamage)
    {
        //Debug.Log("Show");
        _lives -= bulletdamage;
        if (_lives<=0)
        {
            Destroy(gameObject);
        }
    }
}
