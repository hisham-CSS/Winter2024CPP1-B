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
    AudioSource audioSource;

    //Inspector balance variables
    [SerializeField] float speed = 7.0f;
    [SerializeField] int jumpForce = 10;

    //groundcheck stuff
    [SerializeField] bool isGrounded;
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask isGroundLayer;
    [SerializeField] float groundCheckRadius = 0.02f;

    //Audio Clips
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip stompSound;

    //Coroutine
    Coroutine jumpForceChange = null;

    public void StartJumpForceChange()
    {
        if (jumpForceChange == null)
        {
            jumpForceChange = StartCoroutine(JumpForceChange());
            return;
        }

        StopCoroutine(jumpForceChange);
        jumpForceChange = null;
        jumpForce /= 2;
        StartJumpForceChange();
    }

    IEnumerator JumpForceChange()
    {
        jumpForce *= 2;
        yield return new WaitForSeconds(5.0f);
        jumpForce /= 2;
        jumpForceChange = null;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

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
        if (Time.timeScale == 0) return;
        
        float xInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, isGroundLayer);

        if (isGrounded)
        {
            rb.gravityScale = 1;
            anim.ResetTrigger("JumpAtk");
        }

        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);

        if (clipInfo[0].clip.name == "Fire")
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = new Vector2(xInput * speed, rb.velocity.y);
            if (Input.GetButtonDown("Fire1"))
            {
                anim.SetTrigger("Fire");
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            audioSource.PlayOneShot(jumpSound);
        }

        if (Input.GetButtonDown("Jump") && !isGrounded)
        {
            anim.SetTrigger("JumpAtk");
        }

        anim.SetFloat("Input", Mathf.Abs(xInput));
        anim.SetBool("IsGrounded", isGrounded);

        //Sprite Flipping
        if (xInput != 0) sr.flipX = (xInput < 0);
        
    }

    public void IncreaseGravity()
    {
        rb.gravityScale = 5;
    }

    //Trigger functions are called most times - but still generally require one physics body
    //called on the frame you enter the trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.lives--;
        }

        if (collision.CompareTag("Squish"))
        {
            collision.transform.parent.gameObject.GetComponent<Enemy>().TakeDamage(9999);
            Destroy(collision.gameObject);

            //do our bouncy stuff here
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            audioSource.PlayOneShot(stompSound);
        }    
    }

    //called on the frame you exit the trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    //called on frame 2 onwards as you stay in the trigger
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    //Collision functions are only called when one of the two objects that collide is a dynamic rigidbody

    //called on the frame that you enter the collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
    }

    //called on the frame that you exit the collsion
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    //called on frame 2 onwards while you stay in the collision
    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }
}
