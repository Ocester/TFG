using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour
{
    public Animator iconAnim;
    [SerializeField] private List<QuestSO> levelQuests; //quests que puede ofrecer el NPC
    private QuestController questController;
    private int currentQuestIndex=0; // index de las quests que puede ofrecer el NPC
    private QuestSO quest, iconQuest;
    private Vector3 iconOriginalScale;
    private Vector3 iconOriginalPosition;
    private GameObject mainCamera;
    private Camera _camera;
    private float originalCameraOrthographicSize;
    private float scale;
  
    void Start()
    {
        EventController.activateIconQuest += ActivateIcon;
        EventController.deactivateIconQuest += DeactivateIcon;
        EventController.completeQuest += EndIconAnim;
        //iconQuest = CheckCurrentQuest(quest);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        originalCameraOrthographicSize = mainCamera.GetComponent<FollowCamera>().originalZoom;
        _camera = mainCamera.GetComponent<Camera>();
        iconOriginalScale = gameObject.transform.localScale;
        iconOriginalPosition = gameObject.transform.position;
        //Debug.Log("Icon original scale: "+iconOriginalScale);
        
        iconAnim.enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        levelQuests = GetComponentInParent<SpeakNpc>().quests;
        questController = FindObjectOfType<QuestController>();
        quest = questController.GetCurrentQuest();
        ActivateIcon(quest);
    }

    private void Update()
    {
        /*if (gameObject.GetComponent<SpriteRenderer>().enabled)
        {
            if (_camera.orthographicSize > originalCameraOrthographicSize)
            {
                // Escalado del objeto para mantener su tamaño constante y modificamos la transform.y
                scale = _camera.orthographicSize - originalCameraOrthographicSize;
                gameObject.transform.localScale = new Vector3(iconOriginalScale.x + scale, iconOriginalScale.y + scale,
                    iconOriginalScale.z + scale);
                
                gameObject.transform.position = new Vector3(iconOriginalPosition.x, iconOriginalPosition.y + (scale*0.4f),
                    iconOriginalPosition.z);
            }
            else
            {
                gameObject.transform.localScale = iconOriginalScale;
                gameObject.transform.position = iconOriginalPosition;
            }
        }*/
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
        //Debug.Log("entro ActivateIcon");
        gameObject.transform.localScale = iconOriginalScale; // esta línea revisar que da problemas.
        iconQuest = CheckCurrentQuest(checkQuest);
        if (iconQuest == null)
        {
            return;
        }
        if (iconQuest.questId == checkQuest.questId)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            iconAnim.enabled = true;
            //Debug.Log("Icon anim true");
        }
    }
    public void EndIconAnim(QuestSO questToDisable)
    {
        if (iconQuest == null)
        {
            return;
        }
        // si no es la quest actual la que controla este icono no lo elimina.
        if (iconQuest.questId != questToDisable.questId)
        {
            return;
        }
        ShouldDeactivateIcon(questToDisable);
        
        if(iconQuest.questName == questToDisable.questName)
        {
            ActivateIcon((levelQuests[currentQuestIndex]));
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
