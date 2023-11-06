using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour
{
    public Animator iconAnim;
    [SerializeField] private List<QuestSO> quests;
    private QuestController questController;
    private int currentQuestIndex=0; // index de las quests que puede ofrecer el NPC
    private QuestSO quest, currentQuest;
  
    void Start()
    {
        EventController.activateIconQuest += ActivateIcon;
        EventController.deactivateIconQuest += DeactivateIcon;
        EventController.completeQuest += EndIconAnim;
        quests = GetComponentInParent<SpeakNpc>().quests;
        questController = GameObject.FindObjectOfType<QuestController>();
        quest = questController.GetCurrentQuest();
        currentQuest = GetCurrentQuest(quest);
        iconAnim.enabled = false;
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
    
    public void ActivateIcon(QuestSO checkQuest)
    {
        //if (currentQuestIndex > quests.Count-1) { return; } 
        
        currentQuest = GetCurrentQuest(checkQuest);
        if (currentQuest == null)
        {
            return;
        }
        if (currentQuest.questId == checkQuest.questId)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            iconAnim.enabled = true;
        }
    }
    public void EndIconAnim(QuestSO questToDisable)
    {
        if (currentQuest == null)
        {
            return;
        }
        // si no es la quest actual la que controla este icon no lo elimina.
        if (currentQuest.questId != questToDisable.questId)
        {
            return;
        }
        //currentQuestIndex++;
        //if (quests[currentQuestIndex].questId == questToDisable.questId)
        /*if (currentQuestIndex > quests.Count-1)
        {
            Debug.Log("Icon unsubscribe y destroy IconController.cs");
            EventController.activateIconQuest -= ActivateIcon;
            EventController.deactivateIconQuest -= DeactivateIcon;
            EventController.deactivateIconQuest -= EndIconAnim;
            Destroy(gameObject);
        }*/
        
        ShouldDeactivateIcon(questToDisable);
        
        if(currentQuest.questName == questToDisable.questName)
        {
            ActivateIcon((quests[currentQuestIndex]));
        }
    }
    
    private void ShouldDeactivateIcon(QuestSO checkQuest)
    {
        var canBeDisabled = true;
        // se revisa si en la lista de quests del Icono si hay alguna no terminada, entonces no se eliminará.
        foreach (var element in quests)
        {
            if (!element.finished)
            {
                canBeDisabled = false;
            }
        }
        if (canBeDisabled)
        {
            Debug.Log("Icon unsubscribe y destroy IconController.cs");
            EventController.activateIconQuest -= ActivateIcon;
            EventController.deactivateIconQuest -= DeactivateIcon;
            EventController.deactivateIconQuest -= EndIconAnim;
            Destroy(gameObject);
        }
    }
    
    public void DeactivateIcon(QuestSO questIcon)
    {
       gameObject.GetComponent<SpriteRenderer>().enabled = false;
       iconAnim.enabled = false;
    }
}
