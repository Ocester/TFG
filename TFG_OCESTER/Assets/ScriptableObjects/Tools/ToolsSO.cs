using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolsSO", menuName = "Scriptable Object/Tools", order = 1)]
public class ToolsSO : ScriptableObject
{
    //public string action;
    public ToolTypeSO.ToolTypeAction action;
    public Sprite imgAction;
    public Sprite imgActionDisabled;
    [TextArea]public string tooltip;

}
