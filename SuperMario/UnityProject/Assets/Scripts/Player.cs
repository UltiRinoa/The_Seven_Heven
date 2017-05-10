using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {


    public float delay = 1f;
    public float offset = 0.25f;
    public int animTime = 12;
    //public Sprite mario2;
    public int health;
    private Animator m_Anim;
    private PlayerControl playerControl;
    private BoxCollider2D m_collider;
    private Transform footCheck;
    private Transform headCheck;
    private Vector3 currentPos;
    public bool isdead = false;
    public Transform mario;
    public bool invincible = false;
    public Transform mario2;
    public int blinkTime = 6;
    public bool isReborn = false;
    //private GameManager gm;
    void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_collider = GetComponent<BoxCollider2D>();
        footCheck = transform.Find("FootCheck");
        headCheck = transform.Find("HeadCheck");
        playerControl = GetComponent<PlayerControl>();
        
    }

    void Start()
    {
        m_Anim.SetInteger("Level", health);
        GameManager.gm.playerScript = this;
        //if (isReborn)
            

    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        playerControl.SetControllable(false);
        //GameManager.gm.GameStop();

        if (health <= 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            m_Anim.SetBool("Dead", true);
            Destroy(gameObject, delay);
            isdead = true;
        }
        else
        {
            m_Anim.SetBool("Hit", true);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            StartCoroutine(Blink());
        }
    }
    
    void Update()
    {
        if (transform.position.y < -5)
            TakeDamage(9);

    }

    public void LevelUp()
    {
        health++;
        switch(health)
        {
            case 2:
                playerControl.SetControllable(false);
                
                //transform.GetComponent<SpriteRenderer>().sprite = mario2;
                //footCheck.position = new Vector3(footCheck.position.x, footCheck.position.y - offset, footCheck.position.z);
                //headCheck.position = new Vector3(headCheck.position.x, footCheck.position.y + offset, footCheck.position.z);
                m_collider.size = new Vector2(0.36f, 0.9f);
                m_Anim.SetInteger("Level", 2);

                break;

            case 3:
                playerControl.SetControllable(false);
                m_Anim.SetInteger("Level", 3);
                break;

        }
    }

    void Reborn()
    {
        currentPos = new Vector3(transform.position.x, transform.position.y, 0);
        Transform clone = Instantiate(mario, currentPos, Quaternion.identity);
        //clone.localScale = transform.localScale;
        Destroy(gameObject);
    }

    /* void Continue()
     {
         GameManager.gm.Continue();
         m_Anim.SetBool("Hit", false);
         Transform clone = Instantiate(mario2, currentPos, Quaternion.identity);
         clone.GetComponent<Player>().isReborn = true;
         Destroy(this);
     }*/

    IEnumerator Blink()
    {
        invincible = true;
        yield return new WaitForSeconds(0.6f);
        playerControl.SetControllable(true);
        yield return new WaitForSeconds(0.4f);
        currentPos = new Vector3(transform.position.x, transform.position.y, 0);
        Transform clone = Instantiate(mario2, currentPos, Quaternion.identity);
        Destroy(gameObject);
    }
}
