using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NpcInteract : MonoBehaviour
{

    public DialogueNode startingDialogue;
    // public Quest questToComplete;
    public bool IsQuestStep;
    private AudioSource audioSource;
    private bool playerInRange = false;

    private void Start()
    {

        audioSource = GetComponent<AudioSource>();

    }
    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Start dialogue
            FindObjectOfType<DialogueUI>().StartDialogue(startingDialogue);
            audioSource.Play();

            // Complete quest step if applicable
            /* if (IsQuestStep && questToComplete != null)
             {
                 QuestManager.instance.CompleteObjective(questToComplete, 0);
             }*/
        }

       /* if (audioSource.isPlaying)
        {

            Debug.Log("Audio Played!");

        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }


}
