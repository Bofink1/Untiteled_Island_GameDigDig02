using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;

public class DialogueUI : MonoBehaviour
{
    [Header("Main")]
    public TextMeshProUGUI dialogueText;
    public Button[] optionButtons;
    public GameObject panel;
    private DialogueNode currentNode;
    [Header("Player Movement Overides")]
    private CinemachineFreeLook freeLookCamera;
    private float originalXSpeed = 250f;
    private float originalYSpeed = 4f;
    private float originalWalkSpeed = 6f;
    private float originalSprintSpeed = 12f;



    private void Start()
    {
        freeLookCamera = FindObjectOfType<CinemachineFreeLook>();
        // Hide UI at start
        panel.SetActive(false);
        // Check if FreeLookCamera exists (safety check)
        if (freeLookCamera == null)
        {
            Debug.LogError("No Cinemachine FreeLook camera found in the scene!");
            return;
        }

        freeLookCamera.m_XAxis.m_MaxSpeed = originalXSpeed;
        freeLookCamera.m_YAxis.m_MaxSpeed = originalYSpeed;
    }

    public void StartDialogue(DialogueNode node)
    {
        panel.SetActive(true); // Show UI when dialogue starts
        ThirdPersonMovement.walkspeed = 0f;
        ThirdPersonMovement.sprintspeed = 0f;
        freeLookCamera.m_XAxis.m_MaxSpeed = 0f;
        freeLookCamera.m_YAxis.m_MaxSpeed = 0f;


        currentNode = node;
        dialogueText.text = node.npcText;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < node.options.Length)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = node.options[i].text;

                int optionIndex = i;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => OnOptionSelected(optionIndex));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnOptionSelected(int index)
    {
        DialogueOption option = currentNode.options[index];
        DialogueManager.instance.ChooseOption(option);
    }

    public void EndDialogue()
    {
        panel.SetActive(false); // Hide UI when dialogue ends
        ThirdPersonMovement.walkspeed = originalWalkSpeed;
        ThirdPersonMovement.sprintspeed = originalSprintSpeed;
        freeLookCamera.m_XAxis.m_MaxSpeed = originalXSpeed;
        freeLookCamera.m_YAxis.m_MaxSpeed = originalYSpeed;

    }
}
