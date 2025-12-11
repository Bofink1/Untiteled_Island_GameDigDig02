using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    public GameObject FirstText;
    public GameObject SecondText;
    public bool IsMenuOpen;
    public void Start()
    {
        FirstText.SetActive(true);
        IsMenuOpen = false;
    }
    // Update is called once per frame
   public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            IsMenuOpen = !IsMenuOpen; // toggla true/false

            FirstText.SetActive(!IsMenuOpen);
            SecondText.SetActive(IsMenuOpen);

            Debug.Log(IsMenuOpen ? "Controls Shown" : "Controls Hidden");
        }

    }
}
