using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    private DialogueUI dialogueUI;

    [Header("Fade Settings")]
    public CanvasGroup fadeGroup; // Assign your Black Panel's CanvasGroup here
    public GameObject EndingImage;
    public float fadeDuration = 1f;
    public float displayDuration = 2f;

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
        // 1. Journal Entry Logic
        if (!string.IsNullOrEmpty(option.JournalEntry))
        {
            if (JournalUI.instance != null)
            {
                JournalUI.instance.AddJournalEntry(option.JournalEntry);
            }
        }

        // 2. Transition/Ending Logic
        if (option.IsEnding == true)
        {
            StartCoroutine(EndingSequence(option));
        }
        else
        {
            HandleProgression(option);
        }
    }

    private IEnumerator EndingSequence(DialogueOption option)
    {
        // --- FADE SCREEN TO BLACK ---
        dialogueUI.EndDialogue();
        yield return StartCoroutine(Fade(1f));
        // Load in the image (SetActive)
        EndingImage.SetActive(true);
        // Wait while image is visible
        yield return new WaitForSeconds(displayDuration);
        yield return StartCoroutine(Fade(0f));
        // Proceed to end or next node
        HandleProgression(option);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        if (fadeGroup == null) yield break;

        float startAlpha = fadeGroup.alpha;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null;
        }
        fadeGroup.alpha = targetAlpha;
    }

    private void HandleProgression(DialogueOption option)
    {
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