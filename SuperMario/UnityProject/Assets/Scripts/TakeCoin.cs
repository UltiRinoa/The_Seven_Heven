using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCoin : MonoBehaviour {


    private float totalTime = 0.03f;


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            Taked();
        }
    }

    public void Taked()
    {
        StartCoroutine(Impacted());
        Destroy(gameObject, 0.5f);
    }

    IEnumerator Impacted()
    {
        while (totalTime > 0)
        {
            Vector2 pos = transform.position;
            pos.y += 0.3f;
            transform.position = pos;
            totalTime -= 0.01f;
            yield return 0.01f;
        }
    }
}
