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

    public GameObject particle;

    public GameObject effectParticle;

    public bool HasEffect(EffectType type)
    {
        Debug.Log("1...");
        foreach (SkillEffect se in effects)
        {
            Debug.Log("2...");
            Debug.Log("Recebeu " + type + ". Comparou com " + se.effect);
            if (se.effect == type) // N�O TA ENTRANDO AQUI, N�O T� COMPARANDO CERTO
                return true;
        }
        Debug.Log("3...");
        return false;
    }

}
