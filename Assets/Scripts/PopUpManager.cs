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
    private float tempo;
    public Image prizeSprite;

    public IntVariable[] tokens;

    void Update()
    {
        tempo += Time.deltaTime;
        if (enabled == false && tempo >= 10) // a cada 10 segundos tem 5% de chance de aparecer a PopUp
        {
            SpawnChance();
            tempo = 0;
        }
    }

    public void SpawnChance()
    {
        int rand = UnityEngine.Random.Range(1, 101);
        if (rand >= 95)
            SpawnPopUp();
    }

    public void SpawnPopUp()
    {
        enabled = true;
        button.SetActive(true);
        int index = UnityEngine.Random.Range(0, 10);
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
                text.text = amount + " coin(s)";
                prizeSprite.sprite = coins.Sprite;
                break;

            case 1: // Armor fragments
                amount = UnityEngine.Random.Range(1, 11);
                fragments.Value += amount;
                text.text = amount + " gem(s)";
                prizeSprite.sprite = fragments.Sprite;
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
        button.SetActive(false);
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
        text.text = amount + " " + tokens[type].nickname + " Token(s)";
        prizeSprite.sprite = tokens[type].Sprite;
    }
}


