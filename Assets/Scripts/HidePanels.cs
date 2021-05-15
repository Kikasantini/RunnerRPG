using UnityEngine;
using UnityEngine.UI;

public class HidePanels : MonoBehaviour
{
    //public GameObject Panel1;
    //public GameObject Panel2;
    public GameObject Panel;

    public void HidePanel(GameObject Panel)
    {
        Debug.Log("Entrou na HidePanel");
        //Panel1.SetActive(true);
        //Panel2.SetActive(true);
        Panel.SetActive(false);
        
    }

    

    /*
    public GameObject Panel1;
    public GameObject Panel2;

    public void hidePanels(string panel1Name, string panel2Name)
    { 
        Panel1.SetActive(true);
        Panel2.SetActive(true);
    }
    */
}
