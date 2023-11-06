using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTypeSO : ScriptableObject
{
    public enum ToolTypeAction
    {
        Cut,
        Dig,
        Grab,
        Point,
        Speak
    }
}
