using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public static playerMovement instance;

    public float moveSpeed;
    public Rigidbody2D theRB;
    public float jumpForce;
    public bool stopInput,dead;

    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    public GameObject[] avatar;
    public int nrAvatar;
    private GameObject theAvatar;

    [Header("pozitii:")]
    public float pozitieActuala;
    public float pozitieUrm;
    public Color normalcolor;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        nrAvatar = (int)Random.Range(0, avatar.Length-0.3f);
        avatar[nrAvatar].SetActive(true);
        pozitieActuala = transform.position.y;
        pozitieUrm = pozitieActuala + 4f;
        normalcolor = avatar[nrAvatar].GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopInput)
        {
            theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);
            if (isGrounded)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }
            if (theRB.velocity.x < 0)
            {
                avatar[nrAvatar].GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (theRB.velocity.x > 0)
            {
                avatar[nrAvatar].GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else
        {
            theRB.velocity = new Vector2(0f, 0f);
        }
        
    }
    public void changeAvatar()
    {
        avatar[nrAvatar].GetComponent<SpriteRenderer>().color = normalcolor;
        avatar[nrAvatar].SetActive(false);
        nrAvatar = (int)Random.Range(0, avatar.Length-0.3f);
        avatar[nrAvatar].SetActive(true);
    }
    public void takeDamage()
    {
        if (PlayerHealth.instance.health > 0)
        {
            Color currentred;
            currentred = avatar[nrAvatar].GetComponent<SpriteRenderer>().color;
            avatar[nrAvatar].GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(isOk(currentred));
        }
    }

    IEnumerator isOk(Color currentred)
    {
        yield return new WaitForSeconds(0.5f);
        avatar[nrAvatar].GetComponent<SpriteRenderer>().color = currentred;
    }

    public void Transparent()
    {
        avatar[nrAvatar].GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
    }
}
