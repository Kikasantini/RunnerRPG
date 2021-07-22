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

    public void SetProgressBar(int exp, int max)
    {
        percentage = (float)exp / max;
        progressBar.fillAmount = percentage;
        barText.text = (System.Math.Round((float)exp, 1)).ToString() + " / " + max.ToString();
    }
}
