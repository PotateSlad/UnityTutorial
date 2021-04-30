using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicController : MonoBehaviour
{

    public bool facingRight = true;
    public float moveSpeed = 10f;
    public float maxSpeed = 20f;
    public float jumpForce = 20f;
    public int maxJumpLength = 45;
    private int count = 0;
    private Rigidbody2D rb;
    private bool grounded;

    private GameObject groundCheck;

    private Animator animWalk;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animWalk = transform.Find("Graphics").GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            grounded = false;
        }
    }

    //Used for physics and movement updates
    private void FixedUpdate()
    {
        //Set grounding
        //if(groundCheck.CompareTag lol idk)

        //Horizontal movement. Add GetAxis * moveSpeed to x-axis, with velocity abs. value capping at maxSpeed.
        float h = Input.GetAxis("Horizontal");

        if (System.Math.Abs(rb.velocity.x) < maxSpeed)
        {
            transform.position += new Vector3(h * moveSpeed * Time.deltaTime, 0, 0);
        }

        //Now that we've moved, determine if the sprite needs flipped and flip it if so.
        if ((facingRight && Input.GetAxis("Horizontal") < -0.1) || (!facingRight && Input.GetAxis("Horizontal") > 0.1))
        {
            Flip();
        }

        //Jumping. Add jumpForce on Jump GetButtonDown, then add progressively less for 20 more frames if GetButton continues to be true. Stop and reset counter at 20 frames or GetButtonUp.
        if (Input.GetButtonDown("Jump") && grounded)
        {
            transform.position += new Vector3(0, jumpForce * Time.deltaTime, 0);
            count = 0;
        }
        if (Input.GetButton("Jump") && count < maxJumpLength)
        {
            float decimalCount = 1 - (count / maxJumpLength);
            transform.position += new Vector3(0, jumpForce * decimalCount * Time.deltaTime, 0);
            count++;
        }
        if (Input.GetButtonUp("Jump"))
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
