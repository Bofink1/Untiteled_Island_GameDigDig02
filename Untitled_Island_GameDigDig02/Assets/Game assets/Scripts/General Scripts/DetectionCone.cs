using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCone2 : MonoBehaviour
{

    public Transform respawnPoint;
    public GameObject screenCover;

    float timer = 0f;
    bool isDead = false;
    GameObject player;

    private void Start()
    {

        respawnPoint = GameObject.Find("RespawnPoint").transform;
        screenCover = GameObject.Find("ScreenCover");

    }
    void Update()
    {
        if (isDead)
        {
            timer += Time.deltaTime;

            if (timer >= 5f)
            {
                screenCover.SetActive(false);
                isDead = false;
                timer = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;

            // show screen
            screenCover.SetActive(true);

            // teleport
            player.transform.position = respawnPoint.position;

            // start timer
            isDead = true;
            timer = 0f;
        }



    }

}
