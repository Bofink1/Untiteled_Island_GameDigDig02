using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCharacter : MonoBehaviour
{
    public List<GameObject> CharacterModels;
    public Transform SpawnPoint;
    public GameObject PreModel;


    private void Start()
    {
        
        if (CharacterModels.Count > 0)
        {
            Destroy(PreModel);
            GameObject RandomModel = CharacterModels[Random.Range(0, CharacterModels.Count)];
            Quaternion offsetRotation = SpawnPoint.rotation * Quaternion.Euler(0, 90, 0);
            Instantiate(RandomModel, SpawnPoint.position, offsetRotation, transform);

        }

        

    }

}
