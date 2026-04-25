using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    
    public GameObject ShipRepair;
    private void OnTriggerStay(Collider other)
    {

        if (QuestManager.Questscomplteted >= 5f)
        {
            
            ShipRepair.SetActive(true);
          // ViewBlock.SetActive(true);
          // SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
            

        }

    }


}
