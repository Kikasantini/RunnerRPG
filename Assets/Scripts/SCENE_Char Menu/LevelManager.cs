using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //private int totalExp = 0; // Substituí por IntVariable xp
    private int levelMaxExp;
    public IntVariable playerLevel;
    public Text levelText;

    public IntVariable xp;

    // Progress bar (Panel Info & Settings):
    public Image progressBar;
    public Text barText;
    public Text barLevel;
    float percentage;

    void Start()
    {
        UpdateLevelUI();
        levelMaxExp = 10 + (playerLevel.Value + 1) * playerLevel.Value;
        SetProgressBar(xp.Value, levelMaxExp);
    }

    public void UpdateLevelUI()
    {
        levelText.text = "Level " + (playerLevel.Value + 1);
        barLevel.text = "Level " + (playerLevel.Value + 1);
    }

    public void AddExpPoints(int exp)
    {
        // Mudar exp de acordo com battle ID aqui.

        levelMaxExp = 10 + (playerLevel.Value + 1) * playerLevel.Value;
        if ((xp.Value + exp) < levelMaxExp)
        {
            xp.Value += exp;
        }
        else
        {
            xp.Value = (xp.Value + exp) - levelMaxExp;
            playerLevel.Value++;
            levelMaxExp = 10 + (playerLevel.Value + 1) * playerLevel.Value;
            UpdateLevelUI();
        }
        SetProgressBar(xp.Value, levelMaxExp);
    }

    public void SetProgressBar(int exp, int max)
    {
        percentage = (float)exp / max;
        progressBar.fillAmount = percentage;
        barText.text = (System.Math.Round((float)exp, 1)).ToString() + " / " + max.ToString();
    }
}
