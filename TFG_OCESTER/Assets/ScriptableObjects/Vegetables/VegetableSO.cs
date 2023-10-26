using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VegetableSO", menuName = "Scriptable Object/Vegetable", order = 1)]

public class VegetableSO : ScriptableObject
{
    public string nameVegetable;
    public Sprite imgVegetable;
    public Sprite imgPackage;
    public Sprite imgSign;
    public float respawnTime;

}
