using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    public Button[] buttons;
    public GameObject[] imageUnder;
    public GameObject[] canvas;

    public GameObject startPanel;
    public GameObject menuPrincipal;

    // Update is called once per frame

    public void ShowCanvas(int index)
    {
        
        for (int i = 0; i < canvas.Length; ++i)
        {
            if(i == index)
            {
                canvas[i].SetActive(true);
                buttons[i].interactable = false;
                imageUnder[i].SetActive(true);
            }
            else
            {
                canvas[i].SetActive(false);
                buttons[i].interactable = true;
                imageUnder[i].SetActive(false);
            }
                
        }
        menuPrincipal.SetActive(true);
        if (index == 3)
            startPanel.SetActive(true);
    }
    
    void Update()
    {
        
    }
}
