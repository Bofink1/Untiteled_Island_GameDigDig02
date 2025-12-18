using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JournalUI : MonoBehaviour
{
    public static JournalUI instance;

    public GameObject JournalPanel;
    public TextMeshProUGUI journalTextDisplay;

    private bool IsOpen;

    [TextArea(4, 4)]
    public string JournalText;

    public string PastJournalText;
    public string NewJournalText;

    private const string ENTRY_SEPARATOR = "<br>";
    private HashSet<string> addedEntries = new HashSet<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("JournalUI instance created successfully");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            IsOpen = !IsOpen;
            JournalPanel.SetActive(IsOpen);
          
        }
    }

    public void AddJournalEntry(string entry)
    {
        Debug.Log("Added entry:: " + entry);

        if (string.IsNullOrEmpty(entry))
        {
            Debug.LogWarning("Journal entry is null or empty!");
            return;
        }

        if (addedEntries.Contains(entry))
        {
            Debug.Log("Duplicate entry blocked!");
            return;
        }

        addedEntries.Add(entry);

        if (!string.IsNullOrEmpty(JournalText))
        {
            JournalText += ENTRY_SEPARATOR + entry;
        }
        else
        {
            JournalText = entry;
        }

        Debug.Log("Updated JournalText: " + JournalText);
        UpdateJournalDisplay();
    }

    private void UpdateJournalDisplay()
    {
        if (journalTextDisplay != null)
        {
            journalTextDisplay.text = JournalText;
            Debug.Log("Journal display updated. Text length: " + JournalText.Length);
        }
        else
        {
            Debug.LogError("journalTextDisplay reference is null!");
        }
    }

    public void ClearJournal()
    {
        JournalText = string.Empty;
        addedEntries.Clear();
        UpdateJournalDisplay();
        Debug.Log("Journal cleared");
    }
}
