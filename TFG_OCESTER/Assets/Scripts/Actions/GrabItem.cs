using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;

public class GrabItem : MonoBehaviour
{
    
    [SerializeField] private ItemCollectableSO item;
    private ActionController selectedAction;
    private QuestController questController;
    [SerializeField] private bool insideArea = false;
    [SerializeField] private bool canBeGrabbed = false;
    private float maxDistance = 1f;
    private Ray ray;
    private RaycastHit2D hit;
    private GameObject player;
    private Vector2 playerPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        //EventController.completeQuest += CheckNextQuest; // comprobará si el elemento es un item de la quest en curso y lo activa
        EventController.activateItem += CheckNextQuest;
        EventController.OnFinishLevel += FinishLevel;
        selectedAction = GameObject.FindObjectOfType<ActionController>();
        questController = GameObject.FindObjectOfType<QuestController>();
        player = GameObject.FindWithTag("Player");
    }
    private void FinishLevel()
    {
        gameObject.SetActive(false);
    }
    
    
    private void Update()
    {
        /*if (Input.GetMouseButtonDown(0) && insideArea && canBeGrabbed)
        {
            questController.GetItem(item);
            Destroy(gameObject);
        }*/
    }

    public ItemCollectableSO GetItem()
    {
        return item;
    }

    // Revisa si el objeto es un item de la quest acutal, si es así será activado para ser recogido, sino no
    private void IsCurrentQuestItem(QuestSO checkQuest)
    {
        if (!questController.GetCurrentQuest() && item)
        {
            return;
        }
        //Debug.Log("->> IsCurrentQuestItem: "+ questController.GetCurrentQuest().recipe.recipeName);
        canBeGrabbed = false;
        foreach (var element in checkQuest.recipe.elements)
        {
            if (item.nameItem == element.requiredItem.nameItem)
            {
                canBeGrabbed = true;
            }
        }
    }

    private void CheckNextQuest(QuestSO checkQuest)
    {
        IsCurrentQuestItem(checkQuest);
    }

    private void OnMouseOver()
    {
        if (selectedAction.getTool().action != item.collectTool.action)
        {
            Cursor.SetCursor(selectedAction.getTool().imgActionDisabled.texture, Vector2.zero, CursorMode.Auto);
            return;
        }

        if (!canBeGrabbed)
        {
            Cursor.SetCursor(selectedAction.getTool().imgActionDisabled.texture, Vector2.zero, CursorMode.Auto);
            return;
        }
        
        playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
        float dist = Vector2.Distance(playerPosition, hit.point);

        if (hit.collider != null)
        {
            if (dist <= maxDistance)
            {
                // El raycast está dentro del rango máximo
                insideArea = true;
            }
            else
            {
                // El raycast está fuera del rango máximo
                insideArea = false;
            }
        }
        if (insideArea)
        {
            Debug.Log("InsideArea && IsGrabable true: ");
            selectedAction.SetAction(true);
        }
        
    }

    private void OnMouseExit()
    {
        if (selectedAction.getTool().action != item.collectTool.action)
        {
            Cursor.SetCursor(selectedAction.getTool().imgAction.texture, Vector2.zero, CursorMode.Auto);
        }
        if (!canBeGrabbed)
        {
            Cursor.SetCursor(selectedAction.getTool().imgAction.texture, Vector2.zero, CursorMode.Auto);
        }
        insideArea = false;
    }
    
  
}
