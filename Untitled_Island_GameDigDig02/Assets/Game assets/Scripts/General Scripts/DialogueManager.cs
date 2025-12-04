using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    private DialogueUI dialogueUI;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        dialogueUI = FindObjectOfType<DialogueUI>();
    }

    public void StartDialogue(DialogueNode node)
    {
        dialogueUI.StartDialogue(node);
    }

    public void ChooseOption(DialogueOption option)
    {
        // start quest if assigned
       // if (option.questToStart != null)
     //       QuestManager.instance.StartQuest(option.questToStart);

        // complete quest objective if assigned
      //  if (option.questToComplete != null)
        //    QuestManager.instance.CompleteObjective(option.questToComplete, option.objectiveIndex);

        // continue or end dialogue
        if (option.nextNode != null)
        {
            dialogueUI.StartDialogue(option.nextNode);
        }
        else
        {
            dialogueUI.EndDialogue();
        }
    }
}
