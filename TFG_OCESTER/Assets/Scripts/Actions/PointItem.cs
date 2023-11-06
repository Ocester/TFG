using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointItem : MonoBehaviour
{
    private ActionController selectedAction;
    [SerializeField] private ToolsSO pointerTool;
    private string itemPointed;
    
    void Start()
    {
        selectedAction = GameObject.FindObjectOfType<ActionController>();
    }
    
    private void OnMouseOver()
    {
        Debug.Log("OnOmuseOver -> "+gameObject.name);
        if (selectedAction.getTool().action != pointerTool.action)
        {
            return;
        }
        selectedAction.SetAction(true);
    }
    
    private void OnMouseExit()
    {
        selectedAction.SetAction(false);
    }
}
