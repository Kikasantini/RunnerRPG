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
    private Row10[] rows;

    private bool resultsChecked = false;
    private bool achouPremio = false;
    private bool podePremiar = false;

    private int[] quantidades = new int[10]; // quanto de cada item deu no spin

    public IntVariable coins;
    public SkillSO[] skill; // Lista das 3 skills

    public Text coinAnim; // Dinheiro gastando e ganhando (animação)
    
    
    public Text coinAnimText;
    public Text plusText;
    public Text lessText;

    private Vector2 posicaoInicial = new Vector2(0, 0);
    private Vector2 posicaoFinal = new Vector2(0, 0);

    public Button spinButton;
    public GameObject pannelOverMenu; // Won't allow to change scene if spinning

    public IntVariable gems;
    public IntVariable[] equipTokens;

    public BoolVariable brokenMachine;
    private readonly int fixingCost = 5;
    public GameObject brokenMachinePanel;
    public Text priceFixing;

    public IntVariable potA;
    public IntVariable potB;

    // Machine 2 :
    public Text m2status;
    public Text m2prize;
    public Image m2sprite;

    private readonly int spinPrice = 10;

    // Replacing working and broken machine
    public GameObject row1GO;
    public GameObject row2GO;
    public GameObject row3GO;
    public GameObject workingMachineGO;
    public GameObject brokenMachineGO;

    // Jackpot Panel
    public GameObject jackpotPanel;
    public Text qtSkill;
    public Text qtCoin;
    public Text qtGem;
    public Image randSkillSprite;

    private Vector2 posText = new Vector2(0, 0);
    private Vector2 posTextInit = new Vector2(0, 0);
    private Vector2 scaleText = new Vector2(0, 0);
    private Vector2 scaleTextInit = new Vector2(1, 1);


    private void Start()
    {
        //StartCoroutine(CoinUpDown());
            

        ShowCorrectMachine(brokenMachine.Value);
        jackpotPanel.SetActive(false);

        rows[0].rowStopped = true;
        rows[1].rowStopped = true;
        rows[2].rowStopped = true;

        MachineShowPrizeeInfo("Welcome", false, coins.Sprite, "");

        priceFixing.text = "$ " + fixingCost.ToString();

        if (brokenMachine.Value)
        {
            brokenMachinePanel.SetActive(true);
            m2status.text = "";
        }
            

        //skillSprite.enabled = false;
        //posicaoInicial = skillPrize.transform.position;
    }

    void Update()
    {
            // If any row is still moving :
            if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
            {
                resultsChecked = false;
            }

            // If all rows are stopped & results aren't checked yet & you can give the prize
            if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked && podePremiar == true)
            {
                spinButton.interactable = true;
                pannelOverMenu.SetActive(false);
                CheckResults();
            }
    }

    public void ClickSpinButton()
    {
        // If all rows are stopped :
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            // If you have enough coins :
            if (coins.Value >= spinPrice)
            {
                MachineShowPrizeeInfo("", false, coins.Sprite, "");

                podePremiar = true;
                spinButton.interactable = false;
                pannelOverMenu.SetActive(true);
                HandlePulled();
                CoinAnimation(-spinPrice);
                coins.Value -= spinPrice;
                jackpotPanel.SetActive(false);
            }
            else
            {
                m2status.text = "Not enough coins";
            }
        }
    }

    private void RewardPlayer (int numberOfRewards, int rewardIndex)
    {
        if (numberOfRewards == 3)
            numberOfRewards = 10;

        int quantity = numberOfRewards;


        switch (rewardIndex)
        {
            case 0: // Coins
                quantity = numberOfRewards * 10;
                coins.Value += quantity;
                CoinAnimation(quantity);
                MachineShowPrizeeInfo("Coins, coins and more coins", true, coins.Sprite, quantity + " coins");
                break;

            case 1: // Skill 1 : Blaze
                skill[0].quantity += quantity;
                MachineShowPrizeeInfo("Congratulations, you got a skill match", true, skill[0].image, quantity + " " + skill[0].skillName);
                break;

            case 2: // Gems
                quantity = numberOfRewards * 5;
                gems.Value += quantity;
                MachineShowPrizeeInfo("Congratulations, you got Gems", true, gems.Sprite, quantity + " Gems");
                break;

            case 3: // Token chest
                System.Random rand = new System.Random();
                int tokenIndex = rand.Next(0, 5);
                quantity = numberOfRewards + rand.Next(0, 4) + rand.Next(0, 4);
                equipTokens[tokenIndex].Value += quantity;
                MachineShowPrizeeInfo("Cool, you got a random Token bundle", true, equipTokens[tokenIndex].Sprite, quantity + " " + equipTokens[tokenIndex].nickname + " tokens");
                break;

            case 4: // Skill 2 : Absorb Energy
                skill[1].quantity += quantity;
                MachineShowPrizeeInfo("Congratulations, you got a skill match", true, skill[1].image, quantity + " " + skill[1].skillName);
                break;

            case 5: // Pink potion
                MachineShowPrizeeInfo("Congratulations, you got potions", true, potB.Sprite, "X pink pot");
                break;

            case 6: // Green potion
                MachineShowPrizeeInfo("Congratulations, you got potions", true, potA.Sprite, "X green pot");
                break;

            case 7: // Skill 3 : Stone Skin
                skill[2].quantity += quantity;
                MachineShowPrizeeInfo("Congratulations, you got a skill match", true, skill[2].image, quantity + " " + skill[2].skillName);
                break;

            case 8: // JACKPOT
                break;
            
            case 9:
                break;
        }
    }

    /*
    private void ShowPrize(String name, int number, Sprite sprite)
    {
        skillSprite.enabled = true;
        yourPRIZEText.SetActive(false);
        skillSprite.sprite = sprite;

        StartCoroutine(AnimatePrize(skillPrize));

        prizeText.DOFade(0, 0);
        prizeText.text = number + " " + name;
        prizeText.enabled = true;
        prizeText.DOFade(1, 2);  
    }
    */

    /*
    IEnumerator AnimatePrize(GameObject skillPrize)
    {
        posicaoFinal = skillPrize.transform.position;
        for (int i = 0; i < 20; i++)
        {
            posicaoFinal.y += (float)(i * 0.008);
            skillPrize.transform.position = posicaoFinal;
            yield return new WaitForSeconds(0.02f);
        }
        canShowSkillInfo = true;
        yield return new WaitForSeconds(1f);
    }
    */

    /*
    IEnumerator CoinPrize(int quantity)
    {
        //coinPrize.SetActive(true);
        //yourPRIZEText.SetActive(false);
        CoinAnimation(quantity);
        
        posicaoFinal = coinPrize.transform.position;

        for (int i = 0; i < 20; i++)
        {
            posicaoFinal.y += (float)(i * 0.008);
            coinPrize.transform.position = posicaoFinal;
            yield return new WaitForSeconds(0.02f);
        }

        prizeText.DOFade(0, 0);
        prizeText.text = quantity + " coins";
        prizeText.enabled = true;
        prizeText.DOFade(1, 2);
        
    }
    */

    private void CheckResults()
    {
        // Putting 0 in all quantities
        for (int i = 0; i < 10; i++)
            quantidades[i] = 0;

        // aumenta a quantidade do item que parou (nas 3 rows)
        for (int i = 0; i < 3; i++)
            quantidades[(int)rows[i].stoppedSlot]++;

        // Teste máquina quebrada: (quantidades[9] == 3)
        if ((quantidades[9] == 2 || quantidades[9] == 3) && !brokenMachine.Value)
        {
            MachineShowPrizeeInfo("You broke the machine", false, coins.Sprite, "");     
            BreakMachine();
        }
        else if (quantidades[8] == 2 || quantidades[8] == 3) // quantidades[8] == 3
        {
            ItIsJackpot();
        }
        else
        {
            if(!resultsChecked && !brokenMachine.Value)
                CheckResultsPrize();
        }
        resultsChecked = true;
    }

    /*
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
    */

    void CoinAnimation(int value)
    {
        Vector2 posInicial = new Vector2();
        if (value < 0) // gastando moeda // cor vermelha // animação pra baixo
        {   
            posInicial.x = coinAnim.transform.position.x;
            posInicial.y = 3.7f; // 3.7f;
            coinAnim.transform.position = posInicial;
            coinAnim.DOFade(1, 0);
            coinAnim.text = value.ToString();
            coinAnim.color = Color.red;
            coinAnim.DOFade(0, 2);
            coinAnim.transform.DOMoveY(posInicial.y -0.8f, 1, false); // (float to, float duration, bool snapping)
            
        }
        else // ganhando moeda // cor verde // animação pra cima
        {
            posInicial.x = coinAnim.transform.position.x;
            posInicial.y = 3.7f;
            coinAnim.transform.position = posInicial;
            coinAnim.DOFade(1, 0);
            coinAnim.text = "+" + value;
            coinAnim.color = Color.blue;
            coinAnim.DOFade(0, 2);
            coinAnim.transform.DOMoveY(posInicial.y + 0.8f, 1, false); // (float to, float duration, bool snapping)
        }

        coinAnim.transform.DOMoveY(posInicial.y, 0.1f, false); // (float to, float duration, bool snapping)
                                                                   // coinAnim.transform.position = posInicial;
    }

    /*
    public void ShowSkillInfo()
    {
        if (!canShowSkillInfo)
            return;

        skillInfoPanel.SetActive(true);
        text.text = "Name: "+ skill[skillIndex - 1].skillName;
        
        if (skill[skillIndex - 1].isMagic)
            type.text = "Nature: Magical";
        else
            type.text = "Nature: Physical";

        // Mostrar o char aqui:
        if ((skillIndex - 1) == 0 || (skillIndex - 1) == 3 || (skillIndex - 1) == 6) // Mage
            charPicture.sprite = charSprite[0];
        else if ((skillIndex - 1) == 1 || (skillIndex - 1) == 4 || (skillIndex - 1) == 7) // Warrior
            charPicture.sprite = charSprite[1];
        else if ((skillIndex - 1) == 2 || (skillIndex - 1) == 5 || (skillIndex - 1) == 8) // Priest
            charPicture.sprite = charSprite[2];

        if (skill[skillIndex - 1].damage > 0)
        {
            description.text = skill[skillIndex - 1].descriptionPt1 + " " + skill[skillIndex - 1].damage + " " + skill[skillIndex - 1].descriptionPt2;
        }
        else
        {
            description.text = skill[skillIndex - 1].descriptionPt1;
        }
    }
    */

    private void BreakMachine()
    {
        brokenMachine.Value = true;
        jackpotPanel.SetActive(false);
        ShowCorrectMachine(brokenMachine.Value);
    }

    public void FixMachine()
    {
        if (coins.Value >= fixingCost)
        {
            coins.Value -= fixingCost;
            CoinAnimation(-fixingCost);
            brokenMachine.Value = false;
            ShowCorrectMachine(brokenMachine.Value);
        }
        else
        {
            m2status.text = "No money to fix";
        }
    }


    private void CheckResultsPrize()
    { 
        for (int j = 0; j < 10; j++)
        {
            if ((quantidades[j] == 3 || quantidades[j] == 2) && achouPremio == false)
            {
                RewardPlayer(quantidades[j], j);
                achouPremio = true;
            }
        }

        if (achouPremio == false)
        {
            int coinsToGive;
            System.Random random = new System.Random();
            if(random.Next(0, 10) <= 7)
            {
                coinsToGive = random.Next(5, 11); // from 5 to 10
                Debug.Log("random.Next <= 7 ..... coins to give = " + coinsToGive);
            }
            else
            {
                coinsToGive = random.Next(11, 21); // from 11 to 20
                Debug.Log("random.Next > 7 ..... coins to give = " + coinsToGive);
            }

            MachineShowPrizeeInfo("No match", true, coins.Sprite, coinsToGive + " coins");

            coins.Value += coinsToGive;
            CoinAnimation(coinsToGive);
            achouPremio = false;
        }
        else
            achouPremio = false;
    }

    private void ShowCorrectMachine(bool value) // true = broken; false = working
    {
        row1GO.SetActive(!value);
        row2GO.SetActive(!value);
        row3GO.SetActive(!value);
        workingMachineGO.SetActive(!value);

        brokenMachineGO.SetActive(value);
        brokenMachinePanel.SetActive(value);
    }

    private void ItIsJackpot()
    {

        // Give prizes
        System.Random rand = new System.Random();
        
        int randCoins = rand.Next(150, 250);
        coins.Value += randCoins;
        CoinAnimation(randCoins);

        int randGems = rand.Next(150, 200);
        gems.Value += randGems;

        int randIndex = rand.Next(0, 3);
        skill[randIndex].quantity += 15;

        Debug.Log("Jackpot - coins: " + randCoins);
        Debug.Log("Jackpot - gems: " + randGems);
        Debug.Log("Jackpot - skill: 15 " + skill[randIndex].skillName);

        // Setting up the panel
        qtSkill.text = "15";
        qtCoin.text = randCoins.ToString();
        qtGem.text = randGems.ToString();
        randSkillSprite.sprite = skill[randIndex].image;


        MachineShowPrizeeInfo("JACKPOT", false, coins.Sprite, "");
        jackpotPanel.SetActive(true);
        // Give many prizes randomly
        // Activate panel to show prizes
    }

    private void MachineShowPrizeeInfo(string status, bool visible, Sprite img, string text)
    {
        m2status.text = status;

        m2sprite.enabled = visible;
        m2sprite.sprite = img;

        m2prize.text = text;
    }

    IEnumerator CoinUpDown()
    {
        Color novaCor;
        int alpha = 255;

        posTextInit.x = coinAnimText.transform.position.x;
        posTextInit.y = coinAnimText.transform.position.y;
        posText = posTextInit;

        scaleText = scaleTextInit;

        for (int i = 0; i < 50; i++)
        {
            posText.y -= .02f;
            coinAnimText.transform.position = posText;

            scaleText.x -= 0.01f;
            scaleText.y -= 0.01f;
            coinAnimText.transform.localScale = scaleText;

            // Alpha não ta funfando :
            alpha -= 1;
            novaCor = new Color(0, 0, 0, alpha);
            coinAnimText.color = novaCor;

            yield return new WaitForSeconds(.01f);
        }

        /*
        yield return new WaitForSeconds(5f);

        while (newColor.a > 0)
        {
            newColor.a -= 2;
            Debug.Log(newColor.a);
            coinAnimText.color = newColor;
            yield return new WaitForSeconds(.01f);
        }


            // plusText
            // lessText
            yield return new WaitForSeconds(0.5f);

        */
    }



        /*
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
        */
     
        
}