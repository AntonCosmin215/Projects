using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    private Vector3 targetttt;
    private float vitezaa;
    // Start is called before the first frame update
    
    private void Update()
    {
        if (transform.position.y > 6f)
            Destroy(gameObject);
        transform.position = Vector3.MoveTowards(transform.position, targetttt, vitezaa * Time.deltaTime);
    }
    public void setTarget(Vector3 targertransform, float viteza)
    {
        targetttt = targertransform;
        vitezaa = viteza;
    }
}
