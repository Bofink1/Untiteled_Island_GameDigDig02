using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    [System.Serializable]
    public class DialogueOption
    {
    [TextArea(2, 5)]
    public string text; // what the player sees
    public DialogueNode nextNode; // what dialogue comes next

    [Header("Optional Journal Entry")]
    [TextArea(4,4)]
    public string JournalEntry; 
  
    }

    [CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Dialogue System/Dialogue Node")]
    public class DialogueNode : ScriptableObject
    {
        public string npcText;
        public DialogueOption[] options;
    }

