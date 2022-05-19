using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    public Text name;
    public Text type;
    public Text description;
    public SkillSO[] skill;
    public GameObject panel;
    public Image skillImage;

    public void ShowSkillInfo(int index)
    {
        
        name.text = skill[index].skillName;
        
        if (skill[index].isMagic)
            type.text = "Nature: Magical";
        else
            type.text = "Nature: Physical";

        if(skill[index].damage > 0)
        {
            description.text = skill[index].descriptionPt1 + " " + skill[index].damage + " " + skill[index].descriptionPt2;
        }
        else
        {
            description.text = skill[index].descriptionPt1;
        }
        skillImage.sprite = skill[index].image;
        panel.SetActive(true);
    }

    public void OnClick(int index)
    {
        ShowSkillInfo(index);
    }
}
