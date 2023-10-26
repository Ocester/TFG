using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EggSO", menuName = "Scriptable Object/Egg", order = 1)]
public class EggSO : ScriptableObject
{
    public enum EggColour
    {
        white,
        yellow,
        pink,
        green,
        blue
    }
    public EggColour typeEgg;
    public Sprite imgEgg;
    public float respawnTime = 5.0f;
}
