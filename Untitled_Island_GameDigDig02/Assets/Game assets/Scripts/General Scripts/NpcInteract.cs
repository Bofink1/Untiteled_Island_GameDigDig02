using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NpcInteract : MonoBehaviour
{

    public DialogueNode startingDialogue;
    // public Quest questToComplete;
    public bool IsQuestStep;

    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Start dialogue
            FindObjectOfType<DialogueUI>().StartDialogue(startingDialogue);

            // Complete quest step if applicable
           /* if (IsQuestStep && questToComplete != null)
            {
                QuestManager.instance.CompleteObjective(questToComplete, 0);
            }*/
        }
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
