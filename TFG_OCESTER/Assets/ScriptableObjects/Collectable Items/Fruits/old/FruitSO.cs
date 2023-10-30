using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FruitSO", menuName = "Scriptable Object/Fruit", order = 1)]
public class FruitSO : ScriptableObject
{
    public string nameFruit;
    public Sprite imgFruit;
    public float respawnTime = 1.0f;
    public ToolsSO collectTool;
}
