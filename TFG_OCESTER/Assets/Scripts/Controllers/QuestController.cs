using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public List<QuestSO> quests;
    [SerializeField] private int currentQuestIndex = 0;
    [SerializeField]private QuestSO currentQuest;
    [SerializeField] private GameObject missionChart;
    public static QuestController instance;
    public bool finalizarQuest = false; // eliminar en entrega final
    private bool questFinished = false;
   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        currentQuest = GetCurrentQuest();
        EventController.QuestIconActivateEvent(currentQuest);
        EventController.completeQuest += CompleteCurrentQuest;
        
    }
    
    public QuestSO GetCurrentQuest()
    {
        if (currentQuestIndex < quests.Count)
        {
            return quests[currentQuestIndex];
        }
        FinishLevel();
        return null;
    }

    private void FinishLevel()
    {
        Debug.Log("FIN DE NIVEL");
        EventController.FinishLevelEvent();
        Time.timeScale = 0f;
    }

    public void StartQuest()
    {
        currentQuest.started = true;
        
        if(!missionChart.activeSelf){missionChart.SetActive(true);}
        
        EventController.ChangeDialogPicEvent(currentQuest.startingNPC.imgNpc);
        // TEXTO DE LA QUEST.
       EventController.WriteDialogTextEvent(currentQuest);
        
        // AÑADIR A LA UI LA RECETA: Nº DE ITEMS Y SU CANTIDAD RECOLECTADA (AL INICIO 0)
        EventController.WriteMissionUpdateEvent(currentQuest);
        EventController.ActivateItemEvent(currentQuest);
    }

    public void CompleteCurrentQuest(QuestSO quest)
    {
        if (currentQuest )
        {
            currentQuest.finished = true;
            EventController.ChangeDialogPicEvent(currentQuest.startingNPC.imgNpc);
            EventController.ClearMissionTextEvent();
            EventController.WriteDialogTextEvent(currentQuest);
            EventController.QuestIconDeactivateEvent(currentQuest);
            currentQuestIndex++;
            currentQuest = GetCurrentQuest();
            EventController.CheckNextQuestEvent(currentQuest);
            EventController.QuestIconActivateEvent(currentQuest);
            if(missionChart.activeSelf){missionChart.SetActive(false);}
        }
    }

    public void GetItem(ItemCollectableSO item)
    {
        foreach (var element in currentQuest.recipe.elements)
        {
            if (item.nameItem == element.requiredItem.nameItem)
            {
                element.collectedQuantity++;
                EventController.WriteMissionUpdateEvent(currentQuest);
            }
        }
        if (IsRecipeCompleted())
        {
            currentQuest.itemsColected = true;
            // Se activa el icono del NPC para entregar los ingredientes
            EventController.QuestIconActivateEvent(currentQuest);
            // Se ecribe el texto de ingredientes completados
            //Debug.Log(currentQuest.allIngredientsQuestText);
            EventController.WriteDialogTextEvent(currentQuest);
        }
    }
    private bool IsRecipeCompleted()
    {
        questFinished = true;
        foreach (var element in currentQuest.recipe.elements)
        {
            if (element.collectedQuantity < element.quantity )
            {
                questFinished = false;
                break;
            }
        }
        return questFinished;
    }


}
