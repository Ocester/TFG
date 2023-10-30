using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TreeSO", menuName = "Scriptable Object/Tree", order = 1)]
public class TreeSO : ScriptableObject
{
    public string nameTree;
    public Sprite imgTree;
    public ToolsSO collectTool;
    
}
