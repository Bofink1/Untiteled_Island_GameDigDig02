using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestObject : MonoBehaviour
{

    public string questID;
    public float holdTime = 2f;

    public float currentHold = 0f;
    private bool playerInRange = false;
    public GameObject Cross;
    public GameObject SpawnOnPickUp;
    public Slider progressBar;
    private Camera mainCamera;





    // Start is called before the first frame update
    void Start()
    {
        QuestManager.Instance.AddQuest(questID);
        mainCamera = Camera.main;
        if (progressBar != null)
        {
            progressBar.gameObject.SetActive(false);
            progressBar.value = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (playerInRange)
        {
            if (Input.GetKey(KeyCode.E))
            {
                currentHold += Time.deltaTime;
                if (progressBar != null)
                {
                    progressBar.value = currentHold / holdTime;
                }

                if (currentHold >= holdTime)
                {
                    SpawnOnPickUp.transform.position = transform.position;
                    SpawnOnPickUp.SetActive(true);
                    QuestManager.Instance.CompleteQuest(questID);
                    Destroy(gameObject);
                    Cross.SetActive(true);
                    playerInRange = false;

                    if (progressBar != null)
                    {
                        progressBar.gameObject.SetActive(false);
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                currentHold = 0f;
                if (progressBar != null)
                {
                    progressBar.value = 0f;
                }
            }
        }
        else
        {
            currentHold = 0f;
            if (progressBar != null)
            {
                progressBar.value = 0f;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (progressBar != null)
            {
                progressBar.gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            currentHold = 0f;
            if (progressBar != null)
            {
                progressBar.value = 0f;
                progressBar.gameObject.SetActive(false);
            }
        }
    }

}
