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

    //public GameObject imgCoin;
    public GameObject coinPrize;
    
    public Text coinAnim;
    public GameObject thePrize;
    public GameObject yourPRIZEText;
    public GameObject giftLid;

    private Vector2 posicaoInicial = new Vector2(0, 0);
    private Vector2 posicaoFinal = new Vector2(0, 0);
    //private Vector2 posInicialCoin = new Vector2(0, 0);

    private void Start()
    {
        skillSprite.enabled = false;
        posicaoInicial = thePrize.transform.position;
        //posInicialCoin = thePrize.transform.position;

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
            prizeText.enabled = false;

            skillSprite.enabled = false;
            coinPrize.SetActive(false);

            podePremiar = true;
            HandlePulled();
            CoinAnimation(-10);
            coins.Value -= 10;

            yourPRIZEText.SetActive(true);

            // Prêmio revelado volta ao tamanho e posição iniciais
            thePrize.transform.DOScale(1, 0);
            thePrize.transform.position = posicaoInicial;
            coinPrize.transform.position = posicaoInicial;

        }
    }

    private void RewardPlayer (int numberOfRewards, int rewardType)
    {
        StartCoroutine(OpenGift());
        skillSprite.enabled = true;

        if (numberOfRewards == 3)
            numberOfRewards = 10;  

        switch (rewardType)
        {
            case 0: // 10 ou 50 coins
                skillSprite.enabled = false;
                coins.Value += numberOfRewards * 5;
                CoinAnimation(numberOfRewards * 5);
                prizeText.enabled = true;
                prizeText.text = (numberOfRewards * 5) + " coins";
                coinPrize.SetActive(true);
                break;
            case 1: // Skill 1 (Mage)
                skill[0].quantity += numberOfRewards;
                ShowPrize(skill[0].skillName, numberOfRewards, skill[0].image);
                break;
            case 2: // Skill 1 (Warrior)
                skill[1].quantity += numberOfRewards;
                ShowPrize(skill[1].skillName, numberOfRewards, skill[1].image);
                break;
            case 3: // Skill 1 (Priest)
                skill[2].quantity += numberOfRewards;
                ShowPrize(skill[2].skillName, numberOfRewards, skill[2].image);
                break;
            case 4: // Skill 2 (Mage)
                skill[3].quantity += numberOfRewards;
                ShowPrize(skill[3].skillName, numberOfRewards, skill[3].image);
                break;
            case 5: // Skill 2 (Warrior)
                skill[4].quantity += numberOfRewards;
                ShowPrize(skill[4].skillName, numberOfRewards, skill[4].image);
                break;
            case 6: // Skill 2 (Priest)
                skill[5].quantity += numberOfRewards;
                ShowPrize(skill[5].skillName, numberOfRewards, skill[5].image);
                break;
            case 7: // Skill 3 (Mage)
                skill[6].quantity += numberOfRewards;
                ShowPrize(skill[6].skillName, numberOfRewards, skill[6].image);
                break;
            case 8: // Skill 3 (Warrior)
                skill[7].quantity += numberOfRewards;
                ShowPrize(skill[7].skillName, numberOfRewards, skill[7].image);
                break;
            case 9: // Skill 3 (Priest)
                skill[8].quantity += numberOfRewards;
                ShowPrize(skill[8].skillName, numberOfRewards, skill[8].image);
                break;
        }
    }


    private void ShowPrize(String name, int number, Sprite sprite)
    {
        yourPRIZEText.SetActive(false);
        skillSprite.sprite = sprite;

        StartCoroutine(AnimatePrize(thePrize));
        prizeText.enabled = true;
        prizeText.text = number + " " + name;
        prizeText.DOFade(0, 0);
        prizeText.DOFade(1, 0.5f);


    }

    IEnumerator AnimatePrize(GameObject thePrize)
    {
        posicaoFinal = thePrize.transform.position;
        //thePrize.transform.DOScale(2f, 0.5f);

        for (int i = 0; i < 20; i++)
        {
            posicaoFinal.y += (float)(i * 0.01);
            thePrize.transform.position = posicaoFinal;
            //thePrize.transform.Rotate()
            //thePrize.transform.DOScale((float)((1*(1+i)/10)), .1f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator CoinPrize(int quantity)
    {
        coinPrize.SetActive(true);
        CoinAnimation(5);
        yourPRIZEText.SetActive(false);



        posicaoFinal = coinPrize.transform.position;

        for (int i = 0; i < 20; i++)
        {
            posicaoFinal.y += (float)(i * 0.01);
            coinPrize.transform.position = posicaoFinal;
            yield return new WaitForSeconds(0.01f);
        }

        prizeText.enabled = true;
        prizeText.text = quantity + " coins";

        yield return new WaitForSeconds(1f);


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
            StartCoroutine(CoinPrize(5));
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
