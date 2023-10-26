using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolsSO", menuName = "Scriptable Object/Tools", order = 1)]
public class ToolsSO : ScriptableObject
{
    public string grab = "grab";
    public string dig = "dig";
    public string cut = "cut";
    public string pointer = "pointer";
    public Sprite imgGrab;
    public Sprite imgDig;
    public Sprite imgCut;
    public Sprite imgPointer;
    
}
