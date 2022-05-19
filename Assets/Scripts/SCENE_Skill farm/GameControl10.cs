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

    public Text coinAnim; // Dinheiro gastando e ganhando (animação)
    
    public GameObject yourPRIZEText;
    public GameObject giftLid;
    public GameObject skillPrize;
    public GameObject coinPrize;
    public GameObject skillInfoPanel;

    private Vector2 posicaoInicial = new Vector2(0, 0);
    private Vector2 posicaoFinal = new Vector2(0, 0);

    // Skill info panel
    public Text text;
    public Text type;
    public Text description;
    private int skillIndex;
    private bool canShowSkillInfo = false;
    //public Image charPicture;
    //public Sprite[] charSprite;

    public Button spinButton;
    //public Button goBackButton;
    public GameObject pannelOverMenu; // Wont allow to change scene if spinning

    public IntVariable gems;
    public IntVariable[] equipTokens;

    //private bool brokenMachine = false;
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

    private int spinPrice = 10;

    private void Start()
    {
        rows[0].rowStopped = true;
        rows[1].rowStopped = true;
        rows[2].rowStopped = true;

        // Machine 2 :
        m2prize.text = "";
        m2sprite.enabled = false;
        //m2sprite.sprite = coins.Sprite;
        m2status.text = "";


        priceFixing.text = fixingCost.ToString();

        if (brokenMachine.Value)
            brokenMachinePanel.SetActive(true);

        skillSprite.enabled = false;
        posicaoInicial = skillPrize.transform.position;
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
                spinButton.interactable = true;
                //goBackButton.interactable = true;
                pannelOverMenu.SetActive(false);
                CheckResults();
            }
    }

    public void ClickSpinButton()
    {
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            if (coins.Value >= spinPrice)
            {
                // Machine 2 :
                m2prize.text = "";
                m2sprite.enabled = false;
                //m2sprite.sprite = coins.Sprite;
                m2status.text = "";

                canShowSkillInfo = false;
                prizeText.enabled = false;

                skillSprite.enabled = false;
                coinPrize.SetActive(false);

                podePremiar = true;
                spinButton.interactable = false;
                //goBackButton.interactable = false;
                pannelOverMenu.SetActive(true);
                HandlePulled();
                CoinAnimation(-spinPrice);
                coins.Value -= spinPrice;

                yourPRIZEText.SetActive(true);

                // Prêmio revelado volta ao tamanho e posição iniciais
                skillPrize.transform.DOScale(1, 0);
                skillPrize.transform.position = posicaoInicial;
                coinPrize.transform.position = posicaoInicial;
            }
            else
            {
                m2status.text = "Not enough coins";
            }
        }
    }

    private void RewardPlayer (int numberOfRewards, int rewardIndex)
    {
        StartCoroutine(OpenGift());


        if (numberOfRewards == 3)
            numberOfRewards = 10;

        skillIndex = rewardIndex;

        switch (rewardIndex)
        {
            case 0: // 10 ou 50 coins                
                coins.Value += (numberOfRewards * 10);
                StartCoroutine(CoinPrize(numberOfRewards * 10));
                break;
            case 1: // Skill 1 (Mage)
                skill[0].quantity += numberOfRewards;

                // Machine 2 :
                m2prize.text = numberOfRewards + " " + skill[0].skillName;
                m2sprite.enabled = true;
                m2sprite.sprite = skill[0].image;
                m2status.text = "Congratulations, you got a skill match";

                //ShowPrize(skill[0].skillName, numberOfRewards, skill[0].image);
                break;
            case 2: // Skill 1 (Warrior)
                    //skill[1].quantity += numberOfRewards;
                    //ShowPrize(skill[1].skillName, numberOfRewards, skill[1].image);
                    //break;

                // Gems
                gems.Value += (numberOfRewards * 5);
                //ShowPrize("Gems", numberOfRewards * 5, gems.Sprite);

                // Machine 2 :
                m2prize.text = (numberOfRewards * 5) + " Gems";
                m2sprite.enabled = true;
                m2sprite.sprite = gems.Sprite;
                m2status.text = "Congratulations, you got Gems match";


                break;
            case 3: // Skill 1 (Priest)
                // skill[2].quantity += numberOfRewards;
                // ShowPrize(skill[2].skillName, numberOfRewards, skill[2].image);
                // break;

                // Token chest
                System.Random rand = new System.Random();
                int tokenIndex = rand.Next(0, 5);
                numberOfRewards = numberOfRewards + rand.Next(0, 4) + rand.Next(0, 4);
                Debug.Log("Primeiro rand = " + tokenIndex + " Number of rewards = " + numberOfRewards); 

                //ShowPrize(equipTokens[tokenIndex].nickname + " Tokens", numberOfRewards, equipTokens[tokenIndex].Sprite);
                equipTokens[tokenIndex].Value += numberOfRewards;

                // Machine 2 :
                m2prize.text = numberOfRewards + " " + equipTokens[tokenIndex].nickname + " tokens";
                m2sprite.enabled = true;
                m2sprite.sprite = equipTokens[tokenIndex].Sprite;
                m2status.text = "Congratulations, you got a random Token bundle";


                break;
            case 4: // Skill 2 (Mage)
                skill[3].quantity += numberOfRewards;
                //ShowPrize(skill[3].skillName, numberOfRewards, skill[3].image);

                // Machine 2 :
                m2prize.text = numberOfRewards + " " + skill[3].skillName;
                m2sprite.enabled = true;
                m2sprite.sprite = skill[3].image;
                m2status.text = "Congratulations, you got a skill match";

                break;
            case 5: // Skill 2 (Warrior)
                    //skill[4].quantity += numberOfRewards;
                    //ShowPrize(skill[4].skillName, numberOfRewards, skill[4].image);

                // Pink potion

                // Machine 2 :
                m2prize.text = "X pink pot";
                m2sprite.enabled = true;
                m2sprite.sprite = potB.Sprite;
                m2status.text = "Congratulations, you got potions";

                break;
            case 6: // Skill 2 (Priest)
                //skill[5].quantity += numberOfRewards;
                //ShowPrize(skill[5].skillName, numberOfRewards, skill[5].image);
                
                // Green potion
                // Machine 2 :
                m2prize.text = "X green pot";
                m2sprite.enabled = true;
                m2sprite.sprite = potA.Sprite;
                m2status.text = "Congratulations, you got potions";

                break;
            case 7: // Skill 3 (Mage)
                skill[6].quantity += numberOfRewards;
                //ShowPrize(skill[6].skillName, numberOfRewards, skill[6].image);

                // Machine 2 :
                m2prize.text = numberOfRewards + " " + skill[6].skillName;
                m2sprite.enabled = true;
                m2sprite.sprite = skill[6].image;
                m2status.text = "Congratulations, you got a skill match";

                break;
            case 8: // Skill 3 (Warrior)
                //skill[7].quantity += numberOfRewards;
                //ShowPrize(skill[7].skillName, numberOfRewards, skill[7].image);

                // JACKPOT
                break;
            /*
            case 9: // Skill 3 (Priest)
                skill[8].quantity += numberOfRewards;
                ShowPrize(skill[8].skillName, numberOfRewards, skill[8].image);
                break;
            */
        }
    }


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

    IEnumerator CoinPrize(int quantity)
    {
        coinPrize.SetActive(true);
        yourPRIZEText.SetActive(false);
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

    private void CheckResults()
    {
        // Put zero in all quantities
        for (int i = 0; i < 10; i++)
            quantidades[i] = 0;

        for (int i = 0; i < 3; i++)
        { // aumenta a quantidade do item que parou (nas 3 rows)
            quantidades[(int)rows[i].stoppedSlot]++;
            //Debug.Log("quantidades " + (int)rows[i].stoppedSlot + " = " + quantidades[(int)rows[i].stoppedSlot]);
        }




        // Teste máquina quebrada:
        if (quantidades[9] == 1 && !brokenMachine.Value) // quantidades[9] == 3
        {
            // Machine 2 :
            m2prize.text = "";
            m2sprite.enabled = false;
            //m2sprite.sprite = ;
            m2status.text = "You broke the machine";
            
            
            BreakMachine();
        }
        else if(quantidades[9] == 2)
        {
            Debug.Log("Teste BAD MATCH, you dont get anything");
            m2status.text = "Teste BAD MATCH = you get nothing";
        }
        else if (quantidades[8] != 0) // quantidades[8] == 3
        {
            // Machine 2 :
            m2prize.text = "";
            m2sprite.enabled = false;
            //m2sprite.sprite = ;
            m2status.text = "JACKPOT";
            // Give many prizes randomly
            // Activate panel to show prizes
        }
        else
        {
            if(!resultsChecked && !brokenMachine.Value)
                CheckResultsPrize();
        }


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
        brokenMachinePanel.SetActive(true);
    }

    public void FixMachine()
    {
        if (coins.Value >= fixingCost)
        {
            coins.Value -= fixingCost;
            CoinAnimation(-fixingCost);
            brokenMachine.Value = false;
            brokenMachinePanel.SetActive(false);
        }
        else
        {
            //Debug.Log("No money to fix");
            m2status.text = "No money to fix";
        }
    }


    private void CheckResultsPrize()
    {
        //Debug.Log("primeira verificação");
        /*
        if (quantidades[9] == 3)
        {
            Debug.Log("3 bombas. Machine is broken");
            return;
        }
        */

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
            //StartCoroutine(OpenGift());
            

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

            // Machine 2 :
            m2prize.text = coinsToGive + " coins";
            m2sprite.enabled = true;
            m2sprite.sprite = coins.Sprite;
            m2status.text = "No match";

            coins.Value += coinsToGive;
            StartCoroutine(CoinPrize(coinsToGive));
            achouPremio = false;
        }
        else
            achouPremio = false;
    }

}
