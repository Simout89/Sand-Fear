using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directory : MonoBehaviour
{
    [SerializeField] private GameObject[] Pages;
    private int PagesNumber = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && PagesNumber - 1 >= 0)
        {
            CloseAllPages();
            PagesNumber--;
            Pages[PagesNumber].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.D) && PagesNumber + 1 < Pages.Length)
        {
            CloseAllPages();
            PagesNumber++;
            Pages[PagesNumber].SetActive(true);
        }
    }

    private void CloseAllPages()
    {
        for (int i = 0; i < Pages.Length; i++)
        {
            Pages[i].SetActive(false);
        }
    }
}
