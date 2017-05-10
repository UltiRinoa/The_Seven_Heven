using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float animTime = 1f;
    public float moveTime = 2f;
    public float moveSpeed = 10f;
    public Transform leftEdge;
    public Transform rightEdge;
    private int dir = -1;
    private Animator m_Anim;
    private bool isDead = false;
    private bool controllable = true;
    private GameManager gm;
    void Awake()
    {
        m_Anim = GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Player" && !isDead && coll.GetComponent<Player>().invincible == false)
        {
            coll.GetComponent<Player>().TakeDamage(1);
            //gm.gameStop = true;
        }
   
    }

    void FixedUpdate()
    {
        if (isDead || controllable == false)
            return;
        Vector2 pos = transform.position;
        pos.x += dir * moveSpeed * Time.fixedDeltaTime;
        transform.position = pos;
        if (pos.x > leftEdge.position.x - 0.1 && pos.x < leftEdge.position.x + 0.1)
        {
            
            Flip();
        }
        if (pos.x > rightEdge.position.x - 0.1 && pos.x < rightEdge.position.x + 0.1)
            Flip();
    }

    IEnumerator DeadAnim()
    {
        while (animTime > 0)
        {
            Vector2 pos = transform.position;
            pos.y -= 0.05f;
            transform.position = pos;
            animTime -= 0.01f;
            yield return 0.01f;
        }
    }

    public void KillEnemy()
    {
        m_Anim.SetBool("IsDead", true);
        isDead = true;
        Vector3 temp = transform.localScale;
        temp = new Vector3(temp.x, temp.y * - 1, temp.z);
        transform.localScale = temp;
        StartCoroutine(DeadAnim());
        gm.enemys.Remove(transform);
        Destroy(gameObject, animTime);
    }

    void Flip()
    {
        dir = -dir;
        Vector3 temp = transform.localScale;
        temp = new Vector3(temp.x * -1, temp.y, temp.z);
        transform.localScale = temp;
    }

    private void SetControllableTrue()
    {
        controllable = true;
    }

    public void SetControllable(bool b)
    {
        controllable = b;
    }

}
