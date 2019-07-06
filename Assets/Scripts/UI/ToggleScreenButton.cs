using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScreenButton : MonoBehaviour
{

    [SerializeField]
    private GameObject focusScreen;
    [SerializeField]
    private GameObject[] otherScreens;


    public void ToggleScreen()
    {
        if (focusScreen.activeSelf)
        {
            focusScreen.SetActive(false);
        }
        else
        {
            focusScreen.SetActive(true);
            if (otherScreens.Length != 0)
            {
                foreach(GameObject screen in otherScreens)
                {
                    screen.SetActive(false);
                }
            }
        }
    }
}
