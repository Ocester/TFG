using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrabItem : MonoBehaviour
{
    
    [SerializeField] private ItemCollectableSO item;
    private ActionController selectedAction;
    private bool insideArea = false;
    
    // Start is called before the first frame update
    void Start()
    {
        selectedAction = GameObject.FindObjectOfType<ActionController>();
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
        if (selectedAction.getTool().action != item.collectTool.action)
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
        if (other.gameObject.CompareTag("grabArea"))
        {
            insideArea = true;
            Debug.Log(" Area para hacer clic!!!!");
        };
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (selectedAction.getTool().action != item.collectTool.action) {
            return;
        }
        selectedAction.SetAction(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //fuera de zona
        insideArea = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&& insideArea)
        {
            // ******* hay que Ver que esté apuntando a un objeto, no solo con click ******** //
            gameObject.SetActive(false);
            Invoke("Activate", item.respawnTime);
        }
    }
    private void Activate()
    {
        gameObject.SetActive(true);
        
    }
}
