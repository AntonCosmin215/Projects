using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    int damage;

    private void Start()
    {
        StartCoroutine(DezactivColl());
    }
    public int GetDamage()
    {
        return damage;
    }
    IEnumerator DezactivColl()
    {
        yield return new WaitForSeconds(.3f);
        GetComponent<SphereCollider>().enabled = false;
    }
}
