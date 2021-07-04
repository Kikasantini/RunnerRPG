using UnityEngine;
using UnityEngine.UI;

public class HidePanels : MonoBehaviour
{
    public GameObject Panel;

    public void HidePanel(GameObject Panel)
    {
        Panel.SetActive(false);   
    }
}
