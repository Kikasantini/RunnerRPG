using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public int wood;
    public int iron;
    public int gold;

    public Text goldText, woodText, ironText;
 

    void Update()
    {
        goldText.text = gold.ToString();
        woodText.text = wood.ToString();
        ironText.text = iron.ToString();
    }
}
