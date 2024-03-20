using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class StatItems : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite image;

    public int healthModifier;
    public int maxHealthModfier;
    public int cooldownModifier;
    public int speedModifier;
    public int dashCooldownSpeedModifier;
    public int dashSpeedModifier;
    public int corruptionModifier;

    public int rarity = 100;

    public int specialEffect;
}
