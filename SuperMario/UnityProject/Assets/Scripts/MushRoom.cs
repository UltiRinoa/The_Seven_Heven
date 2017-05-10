using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoom : MonoBehaviour {

    public float speed = 1f;
    public float gravity = -9.8f;
    public LayerMask whatIsGround;


    private Transform leftCheck;
    private Transform rightCheck;
    private Transform bottomCheck;
    private Rigidbody2D rb;
    private float time = 0;

    private int dir = 1;

	void Awake()
    {
        leftCheck = transform.Find("LeftCheck");
        rightCheck = transform.Find("RightCheck");
        bottomCheck = transform.Find("BottomCheck");
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        Collider2D[] leftColliders = Physics2D.OverlapCircleAll(leftCheck.position, 0.1f, whatIsGround);
        
        Collider2D[] rightColliders = Physics2D.OverlapCircleAll(rightCheck.position, 0.1f, whatIsGround);

        Collider2D[] bottomColliders = Physics2D.OverlapCircleAll(bottomCheck.position, 0.1f, whatIsGround);

        if (leftColliders.Length != 0)
            dir = -dir;

        else if (rightColliders.Length != 0)
            dir = -dir;

        if (bottomColliders.Length == 0)
        {
            Vector2 vector = rb.velocity;
            vector.y = time * gravity;
            rb.velocity = vector;
            time += Time.fixedDeltaTime;
        }

        else
        {
            Vector2 vector = rb.velocity;
            vector.y = 0;
            rb.velocity = vector;
            time = 0;
        }

        Vector2 pos = transform.position;
        pos.x += dir * speed * Time.fixedDeltaTime;
        transform.position = pos;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Player")
        {
            coll.transform.GetComponent<Player>().LevelUp();
            Destroy(gameObject);
        }
            
    }
}
