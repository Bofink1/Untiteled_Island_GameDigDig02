using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    public int sceneBuildIndex;
    public GameObject ViewBlock;
    private void OnTriggerStay(Collider other)
    {

        if (QuestManager.Questscomplteted >= 7f)
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                ViewBlock.SetActive(true);
                SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
            }

        }

    }


}
