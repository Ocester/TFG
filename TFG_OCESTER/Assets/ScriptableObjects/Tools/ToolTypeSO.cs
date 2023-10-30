using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolTypeSO", menuName = "Scriptable Object/Tool Type", order = 1)]
public class ToolTypeSO : ScriptableObject
{
    public enum ToolTypeAction
    {
        Cut,
        Dig,
        Grab,
        Point
    }
}
