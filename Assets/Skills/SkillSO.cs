using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Type { MAG, PHY } // Skill mágica ou física

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]

public class SkillSO : ScriptableObject
{
    public string skillName;
    public Sprite image;
    public int quantity;

    public int damage;
    public int dotDamage;
    public int dotTime; // em turnos
    public int heal;

    public bool isAOE;
    public bool isDOT;
    public bool isHeal;
    public bool isDamage;
    public bool isShield;
    public bool isStun;





}
