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
        EventController.OnFinishLevel += FinishLevel;
        selectedAction = GameObject.FindObjectOfType<ActionController>();
    }

    private void OnDisable()
    {
        EventController.OnFinishLevel -= FinishLevel;
    }

    private void FinishLevel()
    {
        gameObject.SetActive(false);
    }
    private void OnMouseOver()
    {
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
