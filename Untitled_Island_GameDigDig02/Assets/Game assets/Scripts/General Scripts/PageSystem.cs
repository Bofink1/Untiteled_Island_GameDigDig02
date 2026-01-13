using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PageSystem : MonoBehaviour
{
    public TextMeshProUGUI TextBox;
    public int CurrentPage;
    int ValidPages;

    private void Start()
    {

        TextBox.ForceMeshUpdate();
        ValidPages = TextBox.textInfo.pageCount;

    }

    public void NextPage()
    {
        TextBox.ForceMeshUpdate();
        ValidPages = TextBox.textInfo.pageCount;

        if (CurrentPage < ValidPages)
        {

            CurrentPage++;

        }
       else
        {

            Debug.Log("No valid page is next!");

        }
        TextBox.pageToDisplay = CurrentPage;

    }

    public void PreviousPage()
    {
        if (CurrentPage == 0)
        {
            Debug.Log("Already at the first page, cant move into negative!");
        }
        else
        {
            CurrentPage -=1;
        }

        TextBox.pageToDisplay = CurrentPage;

    }

}
