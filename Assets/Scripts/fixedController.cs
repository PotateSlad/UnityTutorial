using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixedController : MonoBehaviour
{

    public bool facingRight = true;
    public float moveSpeed = 15f;
    private Rigidbody2D rb;

    private Animator animWalk;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animWalk = transform.Find("Graphics").GetComponent<Animator>();
    }

    //Used for physics and movement updates
    private void FixedUpdate()
    {
        //Horizontal movement. Add GetAxis * moveSpeed to x-axis, with velocity abs. value capping at maxSpeed.
        float h = Input.GetAxis("Horizontal");
        if (h > 0)
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
        else if (h < 0)
        {
            transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
        }
        

        //Now that we've moved, determine if the sprite needs flipped and flip it if so.
        if ((facingRight && Input.GetAxis("Horizontal") < -0.1) || (!facingRight && Input.GetAxis("Horizontal") > 0.1))
        {
            Flip();
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
