using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    //Testmode toggle
    public bool TestMode = true;

    //Component
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    //Inspector balance variables
    [SerializeField] float speed = 7.0f;
    [SerializeField] int jumpForce = 10;

    //groundcheck stuff
    [SerializeField] bool isGrounded;
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask isGroundLayer;
    [SerializeField] float groundCheckRadius = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (speed <= 0)
        {
            speed = 7.0f;
            if (TestMode) Debug.Log("Default Value of Speed has changed on " + gameObject.name);
        }

        if (jumpForce <= 0)
        {
            jumpForce = 7;
            if (TestMode) Debug.Log("Default Value of JumpForce has changed on " + gameObject.name);
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f;
            if (TestMode) Debug.Log("Groundcheck radius value has been defaulted on " + gameObject.name);
        }


        if (GroundCheck == null)
        {
            //GroundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
            GameObject obj = new GameObject();
            obj.transform.SetParent(gameObject.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "GroundCheck";
            GroundCheck = obj.transform;
            if (TestMode) Debug.Log("Groundcheck object was created on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, isGroundLayer);

        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        anim.SetFloat("Input", Mathf.Abs(xInput));
        anim.SetBool("IsGrounded", isGrounded);

        //Sprite Flipping
        if (xInput != 0) sr.flipX = (xInput < 0);


    }
}
