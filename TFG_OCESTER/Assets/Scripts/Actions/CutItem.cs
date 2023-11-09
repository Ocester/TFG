using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CutItem : MonoBehaviour
{
    [SerializeField] private ItemCollectableSO item;
    private ActionController selectedAction;
    private QuestController questController;
    //[SerializeField]private bool canBeCut = false;
    
    // Start is called before the first frame update
    void Start()
    {
        EventController.OnFinishLevel += FinishLevel;
        selectedAction = GameObject.FindObjectOfType<ActionController>();
    }
    private void FinishLevel()
    {
        gameObject.SetActive(false);
    }

    private void OnMouseOver()
    {
        if (selectedAction.getTool().action != item.collectTool.action)
        {
            Cursor.SetCursor(selectedAction.getTool().imgActionDisabled.texture, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnMouseExit()
    {
        
        if (selectedAction.getTool().action != item.collectTool.action )   
        {
            Cursor.SetCursor(selectedAction.getTool().imgAction.texture, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (selectedAction.getTool().action != item.collectTool.action)
        {
            selectedAction.SetAction(false);
            return;
        }
        selectedAction.SetAction(true);
        if (other.gameObject.name == "UpHit" || other.gameObject.name == "DownHit" || other.gameObject.name == "RightHit" || other.gameObject.name == "LeftHit")
        {
            Debug.Log("CUT HIT!!");
           
            //questController.GetItem(item);
            gameObject.SetActive(false);
            Invoke("Activate", item.respawnTime);
        };
    }

    private void OnTriggerStay2D(Collider2D other)
    {
         
        if (selectedAction.getTool().action != item.collectTool.action)
        {
            return;
        }
        selectedAction.SetAction(true);
    }

    private void Activate()
    {
        gameObject.SetActive(true);
    }
    
    
}
