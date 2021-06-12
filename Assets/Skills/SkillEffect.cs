using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EffectType { shield, heal, dot, healAccordingToDamage, hot };
public enum Target { self, boss, allEnemies };

[System.Serializable]
public class SkillEffect
{
    public float intensity;
    public Target target;
    public EffectType effect;
   


}
