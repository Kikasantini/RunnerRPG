using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private int totalExp = 0;
    private int levelMaxExp;
    public IntVariable playerLevel;
    public Text levelText;

    // Progress bar (Panel Info & Settings):
    public Image progressBar;
    public Text barText;
    public Text barLevel;
    float percentage;

    void Start()
    {
        UpdateLevelUI();
        levelMaxExp = 10 + (playerLevel.Value + 1) * playerLevel.Value;
        SetProgressBar(totalExp, levelMaxExp);
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
        if ((totalExp + exp) < levelMaxExp)
        {
            totalExp += exp;
        }
        else
        {
            totalExp = (totalExp + exp) - levelMaxExp;
            playerLevel.Value++;
            levelMaxExp = 10 + (playerLevel.Value + 1) * playerLevel.Value;
            UpdateLevelUI();
        }
        SetProgressBar(totalExp, levelMaxExp);
    }

    public void SetProgressBar(int exp, int max)
    {
        percentage = (float)exp / max;
        progressBar.fillAmount = percentage;
        barText.text = (System.Math.Round((float)exp, 1)).ToString() + " / " + max.ToString();
    }
}
