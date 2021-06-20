using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Type { MAG, PHY } // Skill m�gica ou f�sica

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]

public class SkillSO : ScriptableObject
{
    public string skillName;
    public string description;
    public Sprite image;
    public int quantity;

    [SerializeField]
    public List<SkillEffect> effects;

    public Target dmgTarget;
    public int damage;
    public bool isMagic;

}
