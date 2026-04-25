using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class ShipRepairScript : MonoBehaviour
{
    
    public CanvasGroup screenCover;
    public float fadeDuration = 1.0f;
    public float waitTime = 2.0f;

    
    public GameObject OldShip;
    public GameObject NewShip;
    public GameObject Parent;

    private bool isFading = false;
    private bool IsPlayerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        // Optional: Check if the 'other' is the Player tag
        if (other.CompareTag("Player"))
        {
            IsPlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInside = false;
        }
    }

    private void Update()
    {
        if (IsPlayerInside && Input.GetKeyDown(KeyCode.E) && !isFading)
        {
            StartCoroutine(FadeSequence());
        }
    }

    IEnumerator FadeSequence()
    {
        isFading = true;

     
        yield return StartCoroutine(Fade(0, 1));

      
        if (OldShip != null) OldShip.SetActive(false);
        if (NewShip != null) NewShip.SetActive(true);
       

       
        yield return new WaitForSeconds(waitTime);

      
        yield return StartCoroutine(Fade(1, 0));
       
        isFading = false;
        if (Parent != null) Parent.SetActive(false);
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            screenCover.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            yield return null;
        }
        screenCover.alpha = endAlpha;
    }
}