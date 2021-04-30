using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocityController : MonoBehaviour
{

    public bool facingRight = true;
    public float moveSpeed = 10f;
    public float maxSpeed = 20f;
    public float jumpForce = 20f;
    public int maxJumpLength = 10;
    private int count = 0;
    private Rigidbody2D rb;
    public bool grounded;

    private GameObject groundCheck;

    private Animator animWalk;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animWalk = transform.Find("Graphics").GetComponent<Animator>();
        //Physics2D.gravity = new Vector3(0, -30.0f, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            grounded = true;
        }
    }

    //Used for physics and movement updates
    private void FixedUpdate()
    {

        //Horizontal movement. Add GetAxis * moveSpeed to x-axis, with velocity abs. value capping at maxSpeed.
        float h = Input.GetAxis("Horizontal");
        //better gravity
        //rb.velocity -= new Vector2(0, 9.8f);

        if (System.Math.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.velocity += new Vector2(h * moveSpeed * Time.deltaTime, 0);
        }

        //Now that we've moved, determine if the sprite needs flipped and flip it if so.
        if ((facingRight && Input.GetAxis("Horizontal") < -0.1) || (!facingRight && Input.GetAxis("Horizontal") > 0.1))
        {
            Flip();
        }

        //Jumping. Add jumpForce on Jump GetButtonDown, then add progressively less for 20 more frames if GetButton continues to be true. Stop and reset counter at 20 frames or GetButtonUp.
        if (Input.GetButton("Jump") && grounded)
        {
            rb.velocity += new Vector2(0, jumpForce);
            count = 0;
            grounded = false;
        }
        else if (Input.GetButton("Jump") && count < maxJumpLength)
        {
            rb.velocity += new Vector2(0, jumpForce / count);
            count++;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            count = maxJumpLength;
        }

        //...And make sure the animator variables are set.
        animWalk.SetFloat("horizontalMovement", System.Math.Abs(h));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Flip()
    {
        Vector3 theScale = transform.Find("Graphics").localScale;
        theScale.x *= -1;
        transform.Find("Graphics").localScale = theScale;

        facingRight = !facingRight;
    }
}
