using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float jumpForce;
    public float maxJumpForce;
    public float maxJumpForceWithFirePressed;
    public float addJumpForce;
    public float addSpeed;
    public float minSpeed;
    public float maxSpeed;
    public float maxSpeedWithFirePressed;
    public float airSpeed = 50f;

    public bool airControl = true;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private Transform footCheck;
    private Animator animator;
    private bool controllable = true;

    private bool faceRight = true;
    private bool grounded = true;
    private bool jumping = false;
    private bool fliping = false;
    private bool startJump = false;

    private float curMaxJumpHeight;
    private float curMoveSpeed;
    private float jumpTime = 0f;
    private float movementInputValue;

    public void StopJump()
    {
        jumping = false;
        startJump = false;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        footCheck = transform.Find("FootCheck");
    }

	
	// Update is called once per frame
	void Update () {

        movementInputValue = Input.GetAxis("Horizontal");

        if (Input.GetButton("Fire1"))
        {
            curMaxJumpHeight = maxJumpForceWithFirePressed;
            curMoveSpeed = maxSpeedWithFirePressed;
            Fire();
        }
        else
        {
            curMaxJumpHeight = maxJumpForce;
            curMoveSpeed = maxSpeed;
        }
        if (jumpTime >= curMaxJumpHeight)
        {
            startJump = false;
            jumping = false;
        }

        if(fliping && rb.velocity.x > -0.5f && rb.velocity.x < 0.5f)
        {
            fliping = false;
            animator.SetBool("Fliping", fliping);
        }

        if(Input.GetButtonDown("Jump2") && !jumping && grounded)
        {
            jumping = true;
            jumpTime = 0f;
        }

        else if(Input.GetButton("Jump2") && jumpTime < curMaxJumpHeight)
        {
            jumpTime = Mathf.Max(rb.velocity.y + addJumpForce * Time.deltaTime, jumpForce);
            jumpTime = Mathf.Min(jumpTime, maxJumpForce);
        }

        else if(Input.GetButtonUp("Jump2"))
        {
            jumping = false;
            startJump = false;
        }
        
	}

    void FixedUpdate()
    {
        if (!controllable)
            return;
            grounded = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(footCheck.position, 0.2f, whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    grounded = true;
            }

            animator.SetBool("Ground", grounded);
        Move();
    }


    void Move()
    {
        if(grounded || airControl)
        {
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
            if (grounded)
            {
                float force = Mathf.Max(rb.velocity.x + addSpeed * Time.deltaTime, minSpeed);
                if (movementInputValue == 0)
                    force = 0;
                force = Mathf.Min(force, maxSpeed);
                
                rb.velocity = new Vector2(force * movementInputValue, rb.velocity.y);
            }
            else
                rb.velocity = (new Vector2(airSpeed * Time.deltaTime * movementInputValue, rb.velocity.y));

            if (faceRight && movementInputValue < 0)
                Flip();
            else if (!faceRight && movementInputValue > 0)
                Flip();
        }

        if((grounded && jumping) || startJump)
        {
            if (startJump == false)
                rb.velocity = new Vector2(0f, rb.velocity.y);
            startJump = true;
            animator.SetBool("Ground", false);

            rb.velocity = new Vector2(rb.velocity.x, jumpTime);
        }
    }

    public void SetControllable(bool b)
    {
        controllable = b;
    }


    void Fire()
    {

    }

    void Flip()
    {
        if(grounded)
            fliping = true;
        faceRight = !faceRight;
        animator.SetBool("Fliping", fliping);
        Vector3 temp = transform.localScale;
        temp = new Vector3(temp.x * -1, temp.y, temp.z);
        transform.localScale = temp;
    }

    
}
