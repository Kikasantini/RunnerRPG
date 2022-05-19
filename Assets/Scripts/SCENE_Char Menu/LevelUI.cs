using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    private int levelMaxExp;
    public IntVariable playerLevel;
    public IntVariable xp;
    public Text levelText;

    // Progress bar (Panel Info & Settings):
    public Image progressBar;
    public Text barText;
    //public Text barLevel;
    float percentage;


    void Start()
    {
        UpdateLevelUI();
        //levelMaxExp = 10 + (playerLevel.Value + 1) * playerLevel.Value;
        SetProgressBar();
    }

    public void UpdateLevelUI()
    {
        levelText.text = "Level " + (playerLevel.Value + 1);
        //barLevel.text = "Level " + (playerLevel.Value + 1);
        SetProgressBar();
    }

    public void SetProgressBar()
    {
        int curExp = xp.Value;
        int max = 10 + (playerLevel.Value + 1) * playerLevel.Value;
        percentage = (float)curExp / max;
        progressBar.fillAmount = percentage;
        barText.text = (System.Math.Round((float)curExp, 1)).ToString() + " / " + max.ToString();
    }

    public void AddExpPoints(int exp)
    {
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
        }
        UpdateLevelUI();
    }
}
