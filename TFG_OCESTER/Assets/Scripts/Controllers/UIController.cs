using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private GameObject[] toolBtns;
    private Sprite currentToolSprite;
    private void OnEnable()
    {
        // Se suscribe al evento OnSelectedTool
        EventController.OnSelectedTool += ToggleSelection;
    }

    private void OnDisable()
    {
        EventController.OnSelectedTool -= ToggleSelection;
    }

    void Start()
    {
        // Se buscan todos los botones de la tool bar
        toolBtns = GameObject.FindGameObjectsWithTag("toolBtn");
    }

    private void ToggleSelection(ToolsSO selectedTool)
    {
        foreach (var tool in toolBtns)
        {
            if (tool.GetComponent<ToolSelection>().tool.action.ToString() != selectedTool.action.ToString())
            {
                tool.GetComponent<Image>().color = Color.white;
            }
            else
            {
                currentToolSprite = selectedTool.imgAction;
                Cursor.SetCursor(currentToolSprite.texture, Vector2.zero, CursorMode.Auto);
            }
        }
    }

}
