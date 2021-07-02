using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    private bool enabled = false;
    public GameObject[] popUps;
    private GameObject chosenPopUp;
    public Transform spawnPoint;
    public GameObject button;
    public IntVariable fragments;
    public IntVariable coins;
    public GameObject prizePanel;
    public Text text;

    public IntVariable[] tokens;

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
            case 0: // Coins
                amount = UnityEngine.Random.Range(5, 20);
                coins.Value += amount;
                //Debug.Log("Ganhou " + amount + " coin(s)");
                text.text = amount + " coin(s)";
                break;

            case 1: // Armor fragments
                amount = UnityEngine.Random.Range(1, 11);
                fragments.Value += amount;
                //Debug.Log("Ganhou " + amount + " upgrade fragment(s)");
                text.text = amount + " fragment(s)";
                break;

            case 2: // Tokens
                amount = UnityEngine.Random.Range(1, 3);
                GiveToken(amount);
                break;
        }


    }

    public void OnClick()
    {
        button.SetActive(false);
        prizePanel.SetActive(true);
        GivePrize();
    }

    private void DestroyPopUp()
    {
        Destroy(chosenPopUp);
        enabled = false;
    }

    private void GiveToken(int amount)
    {
        // TOKENS (type):
        // 0 Chest
        // 1 Gloves
        // 2 Leggings
        // 3 Shoes
        // 4 Weapon

        int type = UnityEngine.Random.Range(0, 5);
        tokens[type].Value += amount;
        //Debug.Log("Ganhou " + amount + " tokens do tipo " + tokens[type].name);
        text.text = amount + " " + tokens[type].name;
    }
}


