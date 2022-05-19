using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayScene : MonoBehaviour
{
    public Image blackcreen;
    public float timeToShow;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (blackcreen.color.a > 0f)
        blackcreen.color = new Color(blackcreen.color.r, blackcreen.color.g, blackcreen.color.b, Mathf.MoveTowards(blackcreen.color.a, 0f, timeToShow * Time.deltaTime));
    }
}
