using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Type { MAG, PHY } // Skill m�gica ou f�sica

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]

public class SkillSO : ScriptableObject
{
    public string skillName;
    public Image image; // n�o funciona assim?

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
