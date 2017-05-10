using System.Collections;
using UnityEngine;

public class PlayerImpacting : MonoBehaviour {

    public Transform content;
    public float offset = 1f;
    public bool impacting = false;
    private bool impacted = false;
    private float totalTime = 0.08f;

    private Animator m_anim;
    
    void Awake()
    {
        m_anim = GetComponent<Animator>();
    }
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Player" && (coll.transform.position.y - transform.position.y) <= 0.44)
            coll.transform.GetComponent<PlayerControl>().StopJump();
    }

    IEnumerator Impacted()
    {
        while(totalTime > 0)
        {
            Vector2 pos = transform.position;
            if (totalTime > 0.04f)
                pos.y += 0.03f;
            else
                pos.y -= 0.03f;
            transform.position = pos;
            totalTime -= 0.01f;
            yield return 0.01f;
        }
    }

    void Update()
    {
        if (impacting)
        {
            Debug.Log("Test");
            if (impacted)
                return;
            impacted = true;
            m_anim.SetBool("Impact", true);
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
            Transform instance = Instantiate(content, pos, Quaternion.identity);
            TakeCoin tc = instance.GetComponent<TakeCoin>();
            if (tc != null)
                tc.Taked();
            StartCoroutine(Impacted());
        }
    }
}
