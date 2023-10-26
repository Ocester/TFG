using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private GameObject[] toolBtns;
    private CutSelection cutSelected;
    private DigSelection digSelected;
    private GrabSelection grabSelected;
    private PointerSelection pointerSelected;
    private string currentPointer;
   
    void Start()
    {
        cutSelected = GameObject.FindObjectOfType<CutSelection>();
        digSelected = GameObject.FindObjectOfType<DigSelection>();
        grabSelected = GameObject.FindObjectOfType<GrabSelection>();
        pointerSelected = GameObject.FindObjectOfType<PointerSelection>();
        
        //se suscribe como observer de las distintas Action
        cutSelected.OnCutSelectedTool += ToggleSelection;
        digSelected.OnDigSelectedTool += ToggleSelection;
        grabSelected.OnGrabSelectedTool += ToggleSelection;
        pointerSelected.OnPointerSelectedTool += ToggleSelection;

        toolBtns = GameObject.FindGameObjectsWithTag("toolBtn");
    }

    private void ToggleSelection(string obj) {
        
        foreach (var tool in toolBtns)
        {
            if (tool.name != obj+"_btn")
            {
                tool.GetComponent<Image>().color = Color.white;
            }
            else
            {
                Cursor.SetCursor(tool.GetComponent<Image>().sprite.texture, Vector2.zero, CursorMode.Auto);
            }
        }
    }
   
}
