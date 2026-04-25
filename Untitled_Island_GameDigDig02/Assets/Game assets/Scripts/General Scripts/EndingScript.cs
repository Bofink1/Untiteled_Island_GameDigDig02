using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    
    public GameObject ShipRepair;
    public GameObject Text;
   

    private void Update()
    {

        if (QuestManager.Questscomplteted >= 5f)
        {

            ShipRepair.SetActive(true);
            Text.SetActive(true);
            // ViewBlock.SetActive(true);
            // SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);


        }

    }
}
