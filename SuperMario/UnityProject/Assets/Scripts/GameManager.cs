using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Transform respawnPoint;
    public Canvas ui;
    public List<Transform> enemys;
    public static GameManager gm;
    public Player playerScript;
    public bool gameStop;
    void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
        else
            Destroy(this);

    }

    void Update()
    {
        if(playerScript != null && playerScript.isdead == true)
            ui.gameObject.SetActive(true);

        if (playerScript == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerScript = player.GetComponent<Player>();

        }

        if(Input.GetKey(KeyCode.R) && playerScript == null)
        {
            //Instantiate(player, respawnPoint.position, respawnPoint.rotation);
            //ui.gameObject.SetActive(false);
            //Camera.main.transform.position = new Vector3(0, 0, -10);
            SceneManager.LoadScene(0);
        }
         
    }

    public void GameStop()
    {
        gameStop = true;
        playerScript.invincible = true;
        playerScript.GetComponent<PlayerControl>().SetControllable(false);
        for (int i = 0; i < enemys.Count; i++)
        {
            enemys[i].GetComponent<EnemyAI>().SetControllable(false);
        }

    }

    public void Continue()
    {
        gameStop = false;
        playerScript.GetComponent<PlayerControl>().SetControllable(true);
        for (int i = 0; i < enemys.Count; i++)
        {
            enemys[i].GetComponent<EnemyAI>().SetControllable(true);
        }
    }
}
