using System.Collections;
using UnityEngine;

public class InventoryAnimation : MonoBehaviour
{
    public GameObject inventoryPanel;

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

        for (int i = 0; i < 130; i++)
        {
            posNew.y -= .01f;
            inventoryPanel.transform.position = posNew;
            yield return new WaitForSeconds(.001f);

        }

    }

    public void CloseInventory()
    {
        Vector2 posNew = new Vector2(0, 0);
        posNew = inventoryPanel.transform.position;
        posNew.y += (float)(130 * .01);
        inventoryPanel.transform.position = posNew;
        inventoryPanel.SetActive(false);
    }
}
