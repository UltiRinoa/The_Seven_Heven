using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFllow : MonoBehaviour {

    public float boundaryX;

    private Transform target;
    private Camera cam;

    
	void Awake()
    {
        cam = Camera.main;
    }


	// Update is called once per frame
	void LateUpdate () {
        if(target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
            return;
        }
        Vector3 pos = new Vector3(target.position.x, transform.position.y, transform.position.z);
        if (pos.x <= boundaryX)
            return;
        cam.transform.position = pos;       
	}
}
