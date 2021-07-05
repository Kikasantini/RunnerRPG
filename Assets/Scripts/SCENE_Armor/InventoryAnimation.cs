using System.Collections;
using UnityEngine;

public class InventoryAnimation : MonoBehaviour
{
    public GameObject inventoryPanel;
    private int a = 27;
    private float b = .05f;

    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        StartCoroutine(OpeningInventory());
    }

    IEnumerator OpeningInventory()
    {
        Vector2 posOriginal = new Vector2(0, 0);
        Vector2 posNew = new Vector2(0, 0);
        posOriginal = inventoryPanel.transform.position;
        posNew = inventoryPanel.transform.position;

        for (int i = 0; i < a; i++)
        {
            posNew.y -= b;
            inventoryPanel.transform.position = posNew;
            yield return null;

        }

    }

    public void CloseInventory()
    {
        Vector2 posNew = new Vector2(0, 0);
        posNew = inventoryPanel.transform.position;
        posNew.y += (float)(a * b);
        inventoryPanel.transform.position = posNew;
        inventoryPanel.SetActive(false);
    }
}
