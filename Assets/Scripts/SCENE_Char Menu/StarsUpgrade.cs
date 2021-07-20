using UnityEngine;
using UnityEngine.UI;

public class StarsUpgrade : MonoBehaviour
{
    public CharacterSO[] chars;
    public CharacterSO character;
    public IntVariable level;
    public IntVariable coins;

    // Painel de Upgrade de stars
    public GameObject upgradePanel;
    public Image[] stars;
    public Color disabledStar;
    public Text costText;
    //public Button upgradeButton;
    public GameObject upgradeButtonGO;

    // Level mínimo para upar Stars:
    private int star1 = 5;
    private int star2 = 10;
    private int star3 = 15;

    // Custo dos upgrades em coin:
    private int coins1 = 10;
    private int coins2 = 100;
    private int coins3 = 1000;

    public void Start()
    {

    }

    public void OnClick()
    {
        //upgradeButton.interactable = false;
        upgradeButtonGO.SetActive(false);
        int heroIndex = 0;

        foreach (CharacterSO c in chars)
        {
            if (c.selected)
            {
                character = c;
                break;
            }
            heroIndex++;
        }

        for (int i = 0; i < stars.Length; i++)
        {
            if (i < character.stars)
            {
                stars[i].color = Color.white;
            }
            else
            {
                stars[i].color = disabledStar;
            }
        }


        if (character.stars == 0)
        {
            if (level.Value < star1 || coins.Value < coins1)
            {
                costText.text = "You must be <b>Level " + star1 + "</b> and have <b>" + coins1 + " coins</b> to upgrade.";
                return;
            }
        }
        else if (character.stars == 1)
        {
            if (level.Value < star2 || coins.Value < coins2)
            {
                costText.text = "You must be <b>Level " + star2 + " </b> and have <b>" + coins2 + " coins</b> to upgrade.";
                return;
            }
        }
        else if (character.stars == 2)
        {
            if (level.Value < star3 || coins.Value < coins3)
            {
                costText.text = "You must be <b>Level " + star3 + "</b> and have <b>" + coins3 + " coins</b> to upgrade.";
                return;
            }
        }
        else if (character.stars == 3)
        {
            costText.text = "No more upgrades available.";
            return;
        }

        costText.text = "An upgrade is available.";
        //upgradeButton.interactable = true;
        upgradeButtonGO.SetActive(true);

    }

    public void ClickOnUpgrade()
    {
        if (character.stars == 0)
            UpgradeStars(coins1);
        else if (character.stars == 1)
            UpgradeStars(coins2);
        else if (character.stars == 2)
            UpgradeStars(coins3);
    }

    private void UpgradeStars(int cost)
    {
        character.stars++;
        coins.Value -= cost;
        //UpdateStarsUI();
        upgradePanel.SetActive(false);
    }

    /*
    public void UpdateStarsUI()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            if (i < character.stars)
            {
                stars[i].color = Color.white;
            }
            else
            {
                stars[i].color = disabledStar;
            }
        }
    }
    */
}
