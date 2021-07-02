using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    private bool enabled = false;
    public GameObject[] popUps;
    private GameObject chosenPopUp;
    public Transform spawnPoint;
    private int count = 100;
    public GameObject button;
    public IntVariable fragments;
    public IntVariable coins;

    void FixedUpdate()
    {

        if (enabled == false)
        {
            SpawnChance();
        }

        
    }

    public void SpawnChance()
    {
        int rand = UnityEngine.Random.Range(0, 500);
        if (rand == 10)
            SpawnPopUp();
    }

    public void SpawnPopUp()
    {
        enabled = true;
        button.SetActive(true);
        int index = UnityEngine.Random.Range(0, 9);
        Debug.Log("index é " + index);
        chosenPopUp = Instantiate(popUps[index]);
        chosenPopUp.transform.position = spawnPoint.position;

        Invoke(nameof(DestroyPopUp), 5f);
    }

    public void GivePrize()
    {
        int randomPrize = UnityEngine.Random.Range(0, 3);
        int amount;

        switch (randomPrize)
        {
            case 0:
                amount = UnityEngine.Random.Range(10, 51);
                coins.Value += amount;
                Debug.Log("Ganhou " + amount + " coins");
                break;

            case 1:
                amount = UnityEngine.Random.Range(10, 51);
                fragments.Value += amount;
                Debug.Log("Ganhou " + amount + " upgrade fragments");
                break;

            case 2:
                amount = UnityEngine.Random.Range(1, 3);
                GiveToken(amount);
                Debug.Log("Ganhou " + amount + " tokens");
                break;
        }


    }

    public void OnClick()
    {
        button.SetActive(false);
        GivePrize();
    }

    private void DestroyPopUp()
    {
        Destroy(chosenPopUp);
        enabled = false;
    }

    private void GiveToken(int amount)
    {
        // dar token aleatório
        // chest, pants, boots, shoulderguards, gloves, weapon
    }
}


