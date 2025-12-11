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
        Debug.Log("ChooseOption called. Option text: " + option.text);
        Debug.Log("Journal entry: " + (string.IsNullOrEmpty(option.JournalEntry) ? "EMPTY" : option.JournalEntry));

        if (!string.IsNullOrEmpty(option.JournalEntry))
        {
            if (JournalUI.instance != null)
            {
                Debug.Log("Calling AddJournalEntry on JournalUI instance");
                JournalUI.instance.AddJournalEntry(option.JournalEntry);
            }
            else
            {
                Debug.LogWarning("JournalUI instance not found!");
            }
        }
        else
        {
            Debug.Log("No journal entry to add for this option");
        }

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
