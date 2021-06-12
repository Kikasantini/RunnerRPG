using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

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
    public SkillSO[] skill; // Lista das 9 skills

    public Image skillSprite; // Imagem da skill que ganhou
    public Text skillName;  // Nome da skill que ganhou

    public GameObject imgCoin;
    public GameObject giftLid;

    public Text coinAnim;

    private void Start()
    {
        skillSprite.enabled = false;
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
            skillName.enabled = false;
            prizeText.enabled = false;

            skillSprite.enabled = false;
            imgCoin.SetActive(false);

            podePremiar = true;
            HandlePulled();
            CoinAnimation(-10);
            coins.Value -= 10;

        }
    }

    private void RewardPlayer (int numberOfRewards, int rewardType)
    {
        StartCoroutine(OpenGift());
        prizeText.enabled = true;
        skillName.enabled = true;
        skillSprite.enabled = true;

        if (numberOfRewards == 3)
            numberOfRewards = 10;  

        switch (rewardType)
        {
            case 0: // 10 ou 50 coins
                skillSprite.enabled = false;
                skillName.enabled = false;
                coins.Value += numberOfRewards * 5;
                CoinAnimation(numberOfRewards * 5);
                prizeText.text = (numberOfRewards * 5).ToString();
                imgCoin.SetActive(true);
                break;
            case 1: // Skill 1 (Mage)
                prizeText.text = numberOfRewards.ToString();
                skillName.text = skill[0].skillName;
                skill[0].quantity += numberOfRewards;
                skillSprite.sprite = skill[0].image;
                break;
            case 2: // Skill 1 (Warrior)
                prizeText.text = numberOfRewards.ToString();
                skillName.text = skill[1].skillName;
                skill[1].quantity += numberOfRewards;
                skillSprite.sprite = skill[1].image;
                break;
            case 3: // Skill 1 (Priest)
                prizeText.text = numberOfRewards.ToString();
                skillName.text = skill[2].skillName;
                skill[2].quantity += numberOfRewards;
                skillSprite.sprite = skill[2].image;
                break;
            case 4: // Skill 2 (Mage)
                prizeText.text = numberOfRewards.ToString();
                skillName.text = skill[3].skillName;
                skill[3].quantity += numberOfRewards;
                skillSprite.sprite = skill[3].image;
                break;
            case 5: // Skill 2 (Warrior)
                prizeText.text = numberOfRewards.ToString();
                skillName.text = skill[4].skillName;
                skill[4].quantity += numberOfRewards;
                skillSprite.sprite = skill[4].image;
                break;
            case 6: // Skill 2 (Priest)
                prizeText.text = numberOfRewards.ToString();
                skillName.text = skill[5].skillName;
                skill[5].quantity += numberOfRewards;
                skillSprite.sprite = skill[5].image;
                break;
            case 7: // Skill 3 (Mage)
                prizeText.text = numberOfRewards.ToString();
                skillName.text = skill[6].skillName;
                skill[6].quantity += numberOfRewards;
                skillSprite.sprite = skill[6].image;
                break;
            case 8: // Skill 3 (Warrior)
                prizeText.text = numberOfRewards.ToString();
                skillName.text = skill[7].skillName;
                skill[7].quantity += numberOfRewards;
                skillSprite.sprite = skill[7].image;
                break;
            case 9: // Skill 3 (Priest)
                prizeText.text = numberOfRewards.ToString();
                skillName.text = skill[8].skillName;
                skill[8].quantity += numberOfRewards;
                skillSprite.sprite = skill[8].image;
                break;
        }
    }

    private void CheckResults()
    {
        for (int i = 0; i < 10; i++)
            quantidades[i] = 0;

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
            prizeText.text = "5 coins";
            imgCoin.SetActive(true);
            CoinAnimation(5);
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

    void CoinAnimation(int value)
    {
        

        if(value < 0) // gastando moeda
        {
            Vector2 posInicial = new Vector2();
            posInicial.x = coinAnim.transform.position.x;
            posInicial.y = 3.7f;
            coinAnim.transform.position = posInicial;
            coinAnim.DOFade(1, 0);
            // cor vermelha
            // animação pra baixo
            coinAnim.text = value.ToString();
            coinAnim.color = Color.red;
            coinAnim.DOFade(0, 2);
            coinAnim.transform.DOMoveY(3.5f, 1, false); // (float to, float duration, bool snapping)
        }
        else // ganhando moeda
        {
            Vector2 posInicial = new Vector2();
            posInicial.x = coinAnim.transform.position.x;
            posInicial.y = 3.5f;
            coinAnim.transform.position = posInicial;
            coinAnim.DOFade(1, 0);
            // cor verde
            // animação pra cima
            coinAnim.text = "+" + value;
            coinAnim.color = Color.blue;
            coinAnim.DOFade(0, 2);
            coinAnim.transform.DOMoveY(3.7f, 1, false); // (float to, float duration, bool snapping)
        }

    }
}
