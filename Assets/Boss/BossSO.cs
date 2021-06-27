using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss", menuName = "Boss")]

public class BossSO : ScriptableObject
{
    public string name;
    public int level;
    public Sprite bossSprite;

    public int hp;
    public int baseDamage;
    public float magDef;
    public float phyDef;
    public bool isMagic;

    public bool next;

    
    // hp final = hp + (level * 5)
    // dano final = baseDamage + (level * 5)
    // magDef final = magDef + (level * (3 ou 1)) depende se o boss é mágico ou físico
    // phyDef final = phyDef + (level * (3 ou 1))

}
