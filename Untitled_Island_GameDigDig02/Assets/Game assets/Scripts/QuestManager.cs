using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance; // Singleton for easy access

    private Dictionary<string, bool> questCompletion = new Dictionary<string, bool>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddQuest(string questID)
    {
        if (!questCompletion.ContainsKey(questID))
            questCompletion[questID] = false;
    }

    public void CompleteQuest(string questID)
    {
        if (questCompletion.ContainsKey(questID))
        {
            questCompletion[questID] = true;
            Debug.Log("Quest " + questID + " completed!");
        }
    }

    public bool IsCompleted(string questID)
    {
        return questCompletion.ContainsKey(questID) && questCompletion[questID];
    }
}
