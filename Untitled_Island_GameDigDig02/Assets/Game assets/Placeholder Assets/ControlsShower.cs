using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlsShower : MonoBehaviour
{

    public GameObject ControlsText;
    public TextMeshProUGUI ShowText;
    bool IsOpen;
    private void Start()
    {
        IsOpen = false;
        ControlsText.SetActive(false);
        ShowText.text = ">(Show Controls)<";
    }
   
    public void toggleontrols()
    {
        if (IsOpen == false)
        {

            ControlsText.SetActive(true);
            ShowText.text = "<(Hide Controls)>";
            IsOpen = true;

        }
        else
        {

            ControlsText.SetActive(false);
            ShowText.text = ">(Show Controls)<";
            IsOpen = false;

        }

    }

}
