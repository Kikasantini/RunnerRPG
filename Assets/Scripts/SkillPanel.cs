using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    public Text name;
    public Text type;
    public Text description;
    public SkillSO[] skill;
    public GameObject panel;

    public void ShowSkillInfo(int index)
    {
        
        name.text = "Name: " + skill[index].skillName;
        
        if (skill[index].isMagic)
            type.text = "Nature: Magical";
        else
            type.text = "Nature: Physical";

        description.text = skill[index].description;
        panel.SetActive(true);
    }

    public void OnClick(int index)
    {
        ShowSkillInfo(index);
    }
}
