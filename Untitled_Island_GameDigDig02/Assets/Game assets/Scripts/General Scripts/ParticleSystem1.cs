using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystem1 : MonoBehaviour
{
    public GameObject Light;
    private void OnTriggerEnter(Collider other)
    {
        Light.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Light.SetActive(false);    
    }
}
