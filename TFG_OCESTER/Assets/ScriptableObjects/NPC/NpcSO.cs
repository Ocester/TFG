using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcSO", menuName = "Scriptable Object/New NPC", order = 1)]
public class NpcSO : ScriptableObject
{
    public string nameNpc;
    public Sprite imgNpc;
    public ToolsSO interactionTool;
}
