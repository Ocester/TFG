using System;
using System.Collections.Generic;
using UnityEngine;


public class SpeakNpc : MonoBehaviour
{
    [SerializeField] private NpcSO npc;
    [SerializeField]private bool insideArea = false;
    [SerializeField]private bool canBeSpoken= false;
    public List<QuestSO> quests;
    [SerializeField]private bool clickOnCorrectNpc;
    [SerializeField]private String clickedObject;
    private QuestSO _quest, _currentQuest;
    private float _maxDistance = 1.5f;
    private Ray _ray;
    private RaycastHit2D _hit;
    private GameObject _player;
    private Vector2 _playerPosition;
    
    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        EventController.CheckNextQuest += CheckNextQuest;
        EventController.OnFinishLevel += FinishLevel;
        _quest = QuestController.Instance.GetCurrentQuest();
        _currentQuest = GetCurrentQuest(_quest);
        CanBeSpokenNpc();
    }
    private void FinishLevel()
    {
        gameObject.SetActive(false);
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
    private bool CheckCorrectNpc()
    {
        clickOnCorrectNpc = false;
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);
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
        if (npc.nameNpc == _quest.startingNPC.nameNpc)
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
            canBeSpoken = false;
            EventController.CheckNextQuest -= CheckNextQuest;
            EventController.OnFinishLevel -= FinishLevel;
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
                _currentQuest = checkQuest;
            }
        }
        if (!canBeSpoken)
        {
            ShouldDeactivateNpc(checkQuest);
        }
    }

    private void OnMouseOver()
    {
        if (ActionController.Instance.GetTool().action != npc.interactionTool.action)
        {
            Cursor.SetCursor(ActionController.Instance.GetTool().imgActionDisabled.texture, Vector2.zero, CursorMode.Auto);
            return;
        }

        if (!canBeSpoken)
        {
            Cursor.SetCursor(ActionController.Instance.GetTool().imgActionDisabled.texture, Vector2.zero, CursorMode.Auto);
            return;
        }
        
        IsInsideArea();

        if (insideArea && canBeSpoken && CheckCorrectNpc() && !_currentQuest.started)
        {
            ActionController.Instance.SetStartQuest();
        }
        if (insideArea && canBeSpoken && CheckCorrectNpc() && _currentQuest.started && _currentQuest.itemsColected)
        {
            ActionController.Instance.SetCompleteQuest();
        }
        if (insideArea && DialogController.Instance._dialogFinished)
        {
            ActionController.Instance.SetAction(true);
        }
    }
    private void IsInsideArea()
    {
        _playerPosition = new Vector2(_player.transform.position.x, _player.transform.position.y);
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
        float dist = Vector2.Distance(_playerPosition, _hit.point);

        if (_hit.collider != null)
        {
            if (dist <= _maxDistance)
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
    }
    private void OnMouseExit()
    {
        if (ActionController.Instance.GetTool().action != npc.interactionTool.action)
        {
            Cursor.SetCursor(ActionController.Instance.GetTool().imgAction.texture, Vector2.zero, CursorMode.Auto);
        }
        if (!canBeSpoken)
        {
            Cursor.SetCursor(ActionController.Instance.GetTool().imgAction.texture, Vector2.zero, CursorMode.Auto);
        }
        insideArea = false;
    }
    
}
