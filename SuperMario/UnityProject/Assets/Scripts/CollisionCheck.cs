using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour {

    private Transform footCheck;
    private Transform headCheck;
    private Player player;


    void Awake()
    {
        footCheck = transform.Find("FootCheck");
        headCheck = transform.Find("HeadCheck");
        player = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if (player.isdead == true || player.invincible == true)
            return;
        Collider2D[] collideWithHead = Physics2D.OverlapCircleAll(headCheck.position, 0.1f);
        for(int i = 0; i < collideWithHead.Length; i++)
        {
            if (collideWithHead[i].tag == "BoxGround")
                collideWithHead[i].isTrigger = true;
            if (collideWithHead[i].tag == "Brick")
                collideWithHead[i].GetComponent<PlayerImpacting>().impacting = true;
        }

        Collider2D[] collideWithFoot = Physics2D.OverlapCircleAll(footCheck.position, 0.1f);
        for (int i = 0; i < collideWithFoot.Length; i++)
        {
            if (collideWithFoot[i].tag == "BoxGround")
                collideWithFoot[i].isTrigger = false;
            if (collideWithFoot[i].tag == "Enemy")
                collideWithFoot[i].GetComponent<EnemyAI>().KillEnemy();
        }
    }
}
