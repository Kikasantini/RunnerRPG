using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameControl10 : MonoBehaviour
{
    public static event Action HandlePulled = delegate { };

    [SerializeField]
    private Text prizeText;

    [SerializeField]
    private Row10[] rows;

    private bool resultsChecked = false;
    private bool achouPremio = false;
    private bool podePremiar = false;

    private int[] quantidades = new int[10]; // quanto de cada item deu no spin

    public IntVariable coins;
    public CharacterSO[] characters;

    public GameObject imgSkill1;
    public GameObject imgSkill2;
    public GameObject imgSkill3;
    public GameObject imgSkill4;
    public GameObject imgSkill5;
    public GameObject imgSkill6;
    public GameObject imgSkill7;
    public GameObject imgSkill8;
    public GameObject imgSkill9;
    public GameObject imgCoin;
    public GameObject giftLid;

    //Vector2 posOriginal = new Vector2();

    private void Start()
    {
        //posOriginal.x = giftLid.transform.position.x;
        //posOriginal.y = giftLid.transform.position.y;
    }

    void Update()
    {
            if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
            {
                prizeText.enabled = false;
                resultsChecked = false;
            }
            if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked && podePremiar == true)
            {
                CheckResults();
            }
    }

    public void ClickSpinButton()
    {

        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && coins.Value >= 10)
        {

            //giftLid.transform.position = posOriginal;

            prizeText.enabled = false;
            imgSkill1.SetActive(false);
            imgSkill2.SetActive(false);
            imgSkill3.SetActive(false);
            imgSkill4.SetActive(false);
            imgSkill5.SetActive(false);
            imgSkill6.SetActive(false);
            imgSkill7.SetActive(false);
            imgSkill8.SetActive(false);
            imgSkill9.SetActive(false);
            imgCoin.SetActive(false);

            podePremiar = true;
            HandlePulled();
            coins.Value -= 10;
        }
    }

    private void RewardPlayer (int numberOfRewards, int rewardType)
    {
        StartCoroutine(OpenGift());
        prizeText.enabled = true;

        if (numberOfRewards == 3)
            numberOfRewards = 11;  

        switch (rewardType)
        {
            case 0:
                coins.Value += numberOfRewards * 5;
                //prizeText.text = numberOfRewards * 5 + " coins";
                prizeText.text = (numberOfRewards * 5).ToString();
                imgCoin.SetActive(true);
                break;
            case 1:
                characters[0].skill1 += numberOfRewards - 1;
                //prizeText.text = (numberOfRewards - 1) + " Skill 1 (Mage)";
                prizeText.text = (numberOfRewards - 1).ToString();
                imgSkill1.SetActive(true);
                break;
            case 2:
                characters[1].skill1 += numberOfRewards - 1;
                //prizeText.text = (numberOfRewards - 1) + " Skill 1 (Warrior)";
                prizeText.text = (numberOfRewards - 1).ToString();
                imgSkill4.SetActive(true);
                break;
            case 3:
                characters[2].skill1 += numberOfRewards - 1;
                //prizeText.text = (numberOfRewards - 1) + " Skill 1 (Priest)";
                prizeText.text = (numberOfRewards - 1).ToString();
                imgSkill7.SetActive(true);
                break;
            case 4:
                characters[0].skill2 += numberOfRewards - 1;
                //prizeText.text = (numberOfRewards - 1) + " Skill 2 (Mage)";
                prizeText.text = (numberOfRewards - 1).ToString();
                imgSkill2.SetActive(true);
                break;
            case 5:
                characters[1].skill2 += numberOfRewards - 1;
                //prizeText.text = (numberOfRewards - 1) + " Skill 2 (Warrior)";
                prizeText.text = (numberOfRewards - 1).ToString();
                imgSkill5.SetActive(true);
                break;
            case 6:
                characters[2].skill2 += numberOfRewards - 1;
                //prizeText.text = (numberOfRewards - 1) + " Skill 2 (Priest)";
                prizeText.text = (numberOfRewards - 1).ToString();
                imgSkill8.SetActive(true);
                break;
            case 7:
                characters[0].skill3 += numberOfRewards - 1;
                //prizeText.text = (numberOfRewards - 1) + " Skill 3 (Mage)";
                prizeText.text = (numberOfRewards - 1).ToString();
                imgSkill3.SetActive(true);
                break;
            case 8:
                characters[1].skill3 += numberOfRewards - 1;
                //prizeText.text = (numberOfRewards - 1) + " Skill 3 (Warrior)";
                prizeText.text = (numberOfRewards - 1).ToString();
                imgSkill6.SetActive(true);
                break;
            case 9:
                characters[2].skill3 += numberOfRewards - 1;
                //prizeText.text = (numberOfRewards - 1) + " Skill 3 (Priest)";
                prizeText.text = (numberOfRewards - 1).ToString();
                imgSkill9.SetActive(true);
                break;
        }
    }

    private void CheckResults()
    {
        quantidades[0] = 0;
        quantidades[1] = 0;
        quantidades[2] = 0;
        quantidades[3] = 0;
        quantidades[4] = 0;
        quantidades[5] = 0;
        quantidades[6] = 0;
        quantidades[7] = 0;
        quantidades[8] = 0;
        quantidades[9] = 0;   
    
        for(int i = 0; i < 3; i++) // aumenta a quantidade do item que parou (nas 3 rows)
            quantidades[(int)rows[i].stoppedSlot]++;
      
        for (int j = 0; j < 10; j++)
        {
            if((quantidades[j] == 3 || quantidades[j] == 2) && achouPremio == false)
            {
                RewardPlayer(quantidades[j], j);
                achouPremio = true;
            }
        }

        if (achouPremio == false)
        {
            StartCoroutine(OpenGift());
            prizeText.enabled = true;
            prizeText.text = "5";
            imgCoin.SetActive(true);
            coins.Value += 5;
            achouPremio = false;
        }
        else
            achouPremio = false;

        resultsChecked = true;
    }

    IEnumerator OpenGift()
    {
        Vector2 posGift = new Vector2();
        posGift.x = giftLid.transform.position.x;
        posGift.y = giftLid.transform.position.y;

        for (int i = 0; i < 10; i++) // abre o presente
        {
            posGift.x -= 0.05f;
            giftLid.transform.position = posGift;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 10; i++) // fecha o presente
        {
            posGift.x += 0.05f;
            giftLid.transform.position = posGift;
            yield return new WaitForSeconds(0.05f);
        }
    }


}
