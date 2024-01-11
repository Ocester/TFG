using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour
{
    public Animator iconAnim;
    [SerializeField] private List<QuestSO> levelQuests; //quests que puede ofrecer el NPC
    private int _currentQuestIndex=0; // index de las quests que puede ofrecer el NPC
    private QuestSO _quest, _iconQuest;
    private float _scale;
    private Vector3 initialItemSize;
  
    void Start()
    {
        EventController.ActivateIconQuest += ActivateIcon;
        EventController.DeactivateIconQuest += DeactivateIcon;
        EventController.CompleteQuest += EndIconAnim;
        EventController.OnChangeAccessibility+=ChangeIconSize;
        iconAnim.enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        levelQuests = GetComponentInParent<SpeakNpc>().quests;
        _quest = QuestController.Instance.GetCurrentQuest();
        initialItemSize = transform.localScale;
        ActivateIcon(_quest);
    }

    private void ChangeIconSize(UIController.UIElementsSize newSize)
    {
        switch (newSize)
        {
            case UIController.UIElementsSize.S:
                gameObject.transform.localScale = initialItemSize;
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case UIController.UIElementsSize.M:
                gameObject.transform.localScale = new Vector3(initialItemSize.x*2, initialItemSize.y*2, 2);
                break;
        }
    }

    private QuestSO CheckCurrentQuest( QuestSO checkQuest)
    {
        foreach (var element in levelQuests)
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
        _iconQuest = CheckCurrentQuest(checkQuest);
        if (_iconQuest == null)
        {
            return;
        }
        if (_iconQuest.questId == checkQuest.questId)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            iconAnim.enabled = true;
        }
    }
    public void EndIconAnim(QuestSO questToDisable)
    {
        if (_iconQuest == null)
        {
            return;
        }
        // si no es la quest actual la que controla este icono no lo elimina.
        if (_iconQuest.questId != questToDisable.questId)
        {
            return;
        }
        ShouldDeactivateIcon(questToDisable);
        
        if(_iconQuest.questName == questToDisable.questName)
        {
            ActivateIcon((levelQuests[_currentQuestIndex]));
        }
    }
    
    private void ShouldDeactivateIcon(QuestSO checkQuest)
    {
        var canBeDisabled = true;
        // se revisa si en la lista de quests del Icono si hay alguna no terminada, entonces no se eliminará.
        foreach (var element in levelQuests)
        {
            if (!element.finished)
            {
                canBeDisabled = false;
            }
        }
        if (canBeDisabled)
        {
            EventController.ActivateIconQuest -= ActivateIcon;
            EventController.DeactivateIconQuest -= DeactivateIcon;
            EventController.CompleteQuest -= EndIconAnim;
            EventController.OnChangeAccessibility-=ChangeIconSize;
            Destroy(gameObject);
        }
    }
    
    public void DeactivateIcon(QuestSO questIcon)
    {
       gameObject.GetComponent<SpriteRenderer>().enabled = false;
       iconAnim.enabled = false;
    }
}
