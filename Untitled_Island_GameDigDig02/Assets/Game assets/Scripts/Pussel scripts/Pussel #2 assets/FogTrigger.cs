using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

public class FogTrigger : MonoBehaviour
{


    [Header("Fog Settings")]
    public float targetFogDensity = 0.0125f;
    public float transitionSpeed = 1f;
    public bool revertOnExit = true;

    public float originalFogDensity;
   // private bool inTrigger = true;



    // Start is called before the first frame update
    void Start()
    {
        originalFogDensity = RenderSettings.fogDensity;
    }

    // Update is called once per frame

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            StopAllCoroutines();
            StartCoroutine(ChangeFogDensity(targetFogDensity));
            Debug.Log("Player entered fog zone!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (revertOnExit && other.CompareTag("Player"))
        {
            
            StopAllCoroutines();
            StartCoroutine(ChangeFogDensity(originalFogDensity));
            Debug.Log("Player exited fog zone!");
        }
    }

    private IEnumerator ChangeFogDensity(float target)
    {
        float start = RenderSettings.fogDensity;
        float t = 0f;

        while (Mathf.Abs(RenderSettings.fogDensity - target) > 0.001f)
        {
            t += Time.deltaTime * transitionSpeed;
            RenderSettings.fogDensity = Mathf.Lerp(start, target, t);
            yield return null; 
        }

        RenderSettings.fogDensity = target;
    }
    
}
