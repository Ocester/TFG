using UnityEngine;
using System;
using UnityEngine.UI;

public class DigItem : MonoBehaviour
{
    [SerializeField] private ItemCollectableSO item;
    private ActionController selectedAction;
    private QuestController questController;
    [SerializeField]private bool canBeDigged;

    void Start()
    {
        EventController.activateItem += CheckNextQuest;
        selectedAction = GameObject.FindObjectOfType<ActionController>();
        questController = GameObject.FindObjectOfType<QuestController>();
    }
    
    private void IsCurrentQuestItem(QuestSO checkQuest) {
        if (!checkQuest)
        {
            return;
        }
        canBeDigged = false;
        foreach (var element in checkQuest.recipe.elements)
        {
            
            if (item.nameItem == element.requiredItem.nameItem)
            {
                canBeDigged = true;
            }
        }
    }
    private void CheckNextQuest(QuestSO checkQuest)
    {
        IsCurrentQuestItem(checkQuest);
    }
    private void OnMouseOver()
    {
        // se revisa si el tool seleccionado es el indicado para poder recoger el item
        // en caso negativo se cambia el icono del pointer a gris para indicar el error.
        if (selectedAction.getTool().action != item.collectTool.action || !canBeDigged)
        {
            Cursor.SetCursor(selectedAction.getTool().imgActionDisabled.texture, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnMouseExit()
    {
        if (selectedAction.getTool().action != item.collectTool.action || !canBeDigged)
        {
            Cursor.SetCursor(selectedAction.getTool().imgAction.texture, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (selectedAction.getTool().action != item.collectTool.action || !canBeDigged)
        {
            selectedAction.SetAction(false);
            return;
        }
        
        selectedAction.SetAction(true);
        
        // Se revisa si recibe el collider del tool
        if (other.gameObject.name == "UpHit" || other.gameObject.name == "DownHit" || other.gameObject.name == "RightHit" || other.gameObject.name == "LeftHit")
        {
            Debug.Log("DIG HIT!!!!");
            Invoke("Activate", item.respawnTime);
            questController.GetItem(item);
            gameObject.SetActive(false);
            //Destroy(gameObject);
        };
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (selectedAction.getTool().action != item.collectTool.action || !canBeDigged) {
            return;
        }
        selectedAction.SetAction(true);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (selectedAction.getTool().action != item.collectTool.action || !canBeDigged)
        {
            selectedAction.SetAction(true);
        }
    }
    private void Activate()
    {
        gameObject.SetActive(true);
    }

}
