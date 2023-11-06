using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakNpc : MonoBehaviour
{
    [SerializeField] private NpcSO npc;
    [SerializeField]private ActionController selectedAction;
    [SerializeField]private QuestController questController;
    [SerializeField]private bool insideArea = false;
    [SerializeField]private bool canBeSpoken= false;
    public List<QuestSO> quests;
    [SerializeField]private bool clickOnCorrectNpc;
    [SerializeField]private String clickedObject;
    private QuestSO quest, currentQuest;
    
    private void Start()
    {
        EventController.checkNextQuest += CheckNextQuest;
        selectedAction = GameObject.FindObjectOfType<ActionController>();
        questController = GameObject.FindObjectOfType<QuestController>();
        quest = questController.GetCurrentQuest();
        currentQuest = GetCurrentQuest(quest);
        CanBeSpokenNpc();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            
            if (insideArea && canBeSpoken && checkCorrectNpc() && !currentQuest.started)
            {
                //Debug.Log("hablo con " + npc.nameNpc);
                EventController.QuestIconDeactivateEvent(currentQuest);
                questController.StartQuest();
                
            }
            if (insideArea && canBeSpoken && checkCorrectNpc() && currentQuest.started && currentQuest.itemsColected)
            {
                EventController.CompleteQuestEvent(currentQuest);
            }
        }
    }
    private QuestSO GetCurrentQuest( QuestSO checkQuest)
    {
        foreach (var element in quests)
        {
            if (element == checkQuest)
            {
                return element;
            }
        }
        return null;
    }

    // se verifica que clicamos encima del NPC y no fuera
    private bool checkCorrectNpc()
    {
        clickOnCorrectNpc = false;
        Vector2 clickPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(clickPositon, Vector2.zero);
        if (hit.collider && hit.collider.CompareTag("npc"))
        {
            clickedObject = hit.collider.gameObject.GetComponent<SpeakNpc>().npc.nameNpc;
            if (clickedObject == npc.nameNpc)
            {
                clickOnCorrectNpc = true;
            }
        }
        return clickOnCorrectNpc;
    }
    private void CanBeSpokenNpc()
    {
        canBeSpoken = false;
       // se controla que estamos en el NPC asignado a la quest
        if (npc.nameNpc == quest.startingNPC.nameNpc)
        {
            canBeSpoken = true;
        }
    }
    private void ShouldDeactivateNpc(QuestSO checkQuest)
    {
        var canBeDisabled = true;
        // se revisa si en la lista de quests del NPC si hay alguna no terminada, entonces no se eliminará.
        foreach (var element in quests)
        {
            if (!element.finished)
            {
                canBeDisabled = false;
            }
        }
        if (canBeDisabled)
        {
            Debug.Log(npc.nameNpc+" NPC DESACTIVADO");
            canBeSpoken = false;
            EventController.checkNextQuest -= CheckNextQuest;
        }
    }

    private void CheckNextQuest(QuestSO checkQuest)
    {
        canBeSpoken = false;
        // Comparamos la quest actual contra todas las quest que activa este NPC
        foreach (var element in quests)
        {
            // si la quest actual está entre las quests del NPC el NPC se puede hablar
            if (element == checkQuest)
            {
                canBeSpoken = true;
                currentQuest = checkQuest;
            }
        }
        if (!canBeSpoken)
        {
            ShouldDeactivateNpc(checkQuest);
        }
    }

    private void OnMouseOver()
    {
        if (selectedAction.getTool().action != npc.interactionTool.action)
        {
            Cursor.SetCursor(selectedAction.getTool().imgActionDisabled.texture, Vector2.zero, CursorMode.Auto);
        }

        if (!canBeSpoken)
        {
            Cursor.SetCursor(selectedAction.getTool().imgActionDisabled.texture, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnMouseExit()
    {
        if (selectedAction.getTool().action != npc.interactionTool.action)
        {
            Cursor.SetCursor(selectedAction.getTool().imgAction.texture, Vector2.zero, CursorMode.Auto);
        }
        if (!canBeSpoken)
        {
            Cursor.SetCursor(selectedAction.getTool().imgAction.texture, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (selectedAction.getTool().action != npc.interactionTool.action)
        {
            selectedAction.SetAction(false);
            return;
        }
        selectedAction.SetAction(true);
        if (other.gameObject.CompareTag("grabArea"))
        {
            insideArea = true;
            //Debug.Log(" Area para hacer clic!!!!");
        };
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (selectedAction.getTool().action != npc.interactionTool.action)
        {
            selectedAction.SetAction(false);
            return;
        }
        selectedAction.SetAction(true);
        if (other.gameObject.CompareTag("grabArea"))
        {
            insideArea = true;
            //Debug.Log(" Stay Area para hacer clic!!!!");
        };
        
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        //fuera de zona
        insideArea = false;
    }
    
    
}
