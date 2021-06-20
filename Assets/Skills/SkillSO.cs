using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Type { MAG, PHY } // Skill mágica ou física

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
