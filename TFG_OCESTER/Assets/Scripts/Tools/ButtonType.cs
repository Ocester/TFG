using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonType : MonoBehaviour
{
    public ToolType toolType;
    public enum ToolType
    {
        Point,
        Grab,
        Dig,
        Cut
    }
}
