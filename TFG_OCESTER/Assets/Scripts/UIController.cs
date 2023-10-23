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
    void Start()
    {
        cutSelected = GameObject.FindObjectOfType<CutSelection>();
        digSelected = GameObject.FindObjectOfType<DigSelection>();
        grabSelected = GameObject.FindObjectOfType<GrabSelection>();
        
        //se suscribe como observer de las distinas Action
        cutSelected.OnCutSelectedTool += ToggleSelection;
        digSelected.OnDigSelectedTool += ToggleSelection;
        grabSelected.OnGrabSelectedTool += ToggleSelection;

        toolBtns = GameObject.FindGameObjectsWithTag("toolBtn");
    }

    private void ToggleSelection(string obj) {
        
        foreach (var tool in toolBtns)
        {
            if (tool.name != obj+"_btn")
            {
                tool.GetComponent<Image>().color = Color.white;
            }
        }
    }
   
}
