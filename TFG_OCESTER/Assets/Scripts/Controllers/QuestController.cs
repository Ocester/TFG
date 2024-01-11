using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public List<QuestSO> quests;
    [SerializeField] private int currentQuestIndex;
    [SerializeField]private QuestSO currentQuest;
    [SerializeField] private GameObject missionChart;
    [SerializeField] private GameObject missionDetails;
    [SerializeField] private GameObject greenCheck;
    [SerializeField] private GameObject itemImg;
    
    private bool _questFinished;
    public bool allQuestsFinished;
    public static QuestController Instance;
    private RectTransform originalMissionRectTransform;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        _questFinished = false;
        currentQuestIndex = 0;
        currentQuest = GetCurrentQuest();
        EventController.QuestIconActivateEvent(currentQuest);
        EventController.CompleteQuest += CompleteCurrentQuest;
        allQuestsFinished = false;
        greenCheck.SetActive(false);
        itemImg.SetActive(false);
        
    }
    
    public QuestSO GetCurrentQuest()
    {
        if (currentQuestIndex < quests.Count)
        {
            return quests[currentQuestIndex];
        }
        return null;
    }
    public void StartQuest()
    {
        currentQuest.started = true;

        //si es la última quest tiene un tratamiento especial, solo muestra texto de la quest no hay que recolectar nada
        if (currentQuestIndex == quests.Count-1)
        {
            allQuestsFinished = true;
            EventController.ChangeDialogPicEvent(currentQuest.startingNPC.imgNpc);
            EventController.WriteDialogTextEvent(currentQuest);
            return;
        }
        if (!missionChart.activeSelf)
        {
            missionChart.SetActive(true);
            originalMissionRectTransform = missionDetails.GetComponent<RectTransform>();
            float newHeight = currentQuest.recipe.elements.Count * 60f;
            missionDetails.GetComponent<RectTransform>().sizeDelta=new Vector2(originalMissionRectTransform.sizeDelta.x, newHeight);
            missionDetails.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
        
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
            if (greenCheck.activeSelf) { greenCheck.SetActive(false); }
        }
    }

    public void GetItem(ItemCollectableSO item)
    {
        foreach (var element in currentQuest.recipe.elements)
        {
            if (item.nameItem != element.requiredItem.nameItem) continue;
            element.collectedQuantity++;
            EventController.WriteMissionUpdateEvent(currentQuest);
            itemImg.GetComponent<SpriteRenderer>().sprite = element.requiredItem.imgItem;
            itemImg.GetComponent<Transform>().localScale = new Vector3(5f, 5f,1f);
            Debug.Log(element.requiredItem.nameItem);
        }
        
        itemImg.SetActive(true);
        Invoke("DeactivateItemImg",2f);
    
        if (IsRecipeCompleted())
        {
            currentQuest.itemsColected = true;
            // Se activa el icono del NPC para entregar los ingredientes
            EventController.QuestIconActivateEvent(currentQuest);
            // Se ecribe el texto de ingredientes completados
            EventController.WriteDialogTextEvent(currentQuest);
            greenCheck.SetActive(true);
        }
    }

    private void DeactivateItemImg()
    {
        itemImg.SetActive(false);
    }

    private bool IsRecipeCompleted()
    {
        _questFinished = true;
        foreach (var element in currentQuest.recipe.elements)
        {
            if (element.collectedQuantity < element.quantity )
            {
                _questFinished = false;
                break;
            }
        }
        return _questFinished;
    }
}
