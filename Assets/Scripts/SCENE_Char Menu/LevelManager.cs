using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private int levelMaxExp;
    public IntVariable playerLevel;
    public IntVariable xp;

    void Start()
    {
        //levelMaxExp = 10 + (playerLevel.Value + 1) * playerLevel.Value;
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
        
    }
}
