using System.Collections;
using UnityEngine;

public class FogTrigger : MonoBehaviour
{
    [Header("Fog Settings")]
    public float targetFogDensity = 0.0125f;
    public float transitionSpeed = 1f;
    public bool revertOnExit = true;

    private float originalFogDensity;

    [Header("Skybox Settings")]
    public Material skyboxInside;
    public Material skyboxOutside;

    [Header("Water Settings")]
    public GameObject waterObject;

    void Start()
    {
        originalFogDensity = RenderSettings.fogDensity;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(ChangeFogDensity(targetFogDensity));

            // Change skybox
            if (skyboxInside != null)
            {
                RenderSettings.skybox = skyboxInside;
                DynamicGI.UpdateEnvironment();
            }

            // Disable water
            if (waterObject != null)
            {
                waterObject.SetActive(false);
            }

            Debug.Log("Player entered fog zone!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (revertOnExit && other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(ChangeFogDensity(originalFogDensity));

            // Revert skybox
            if (skyboxOutside != null)
            {
                RenderSettings.skybox = skyboxOutside;
                DynamicGI.UpdateEnvironment();
            }

            // Enable water again
            if (waterObject != null)
            {
                waterObject.SetActive(true);
            }

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