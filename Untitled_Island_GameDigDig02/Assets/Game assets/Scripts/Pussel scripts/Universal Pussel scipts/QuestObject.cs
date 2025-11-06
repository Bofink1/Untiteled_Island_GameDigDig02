using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestObject : MonoBehaviour
{

    public string questID;
    public float holdTime = 2f;

    public float currentHold = 0f;
    private bool playerInRange = false;
    public GameObject Cross;
    public bool SpawnSomethingOnPickUp = false;
    public GameObject SpawnOnPickUp;
   



    // Start is called before the first frame update
    void Start()
    {
        QuestManager.Instance.AddQuest(questID);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange)
        {
            if (Input.GetKey(KeyCode.E))
            {
                currentHold += Time.deltaTime;
                if(currentHold >= holdTime)
                {
                    if (SpawnSomethingOnPickUp == true)
                    {
                        SpawnOnPickUp.transform.position = transform.position;
                        SpawnOnPickUp.SetActive(true);
                    }
                    QuestManager.Instance.CompleteQuest(questID);
                    Destroy(gameObject);
                    Cross.SetActive(true);
                    playerInRange = false;
                   
                }
            }
        }
        else
        {
            currentHold = 0f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
            currentHold = 0f;
        }
    }
    
}
