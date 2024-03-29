using UnityEngine;

public class DigItem : MonoBehaviour
{
    [SerializeField] private ItemCollectableSO item;
    [SerializeField]private bool canBeDigged;
    private Vector3 initialItemSize;

    private void Start()
    {
        EventController.ActivateItem += CheckNextQuest;
        EventController.OnFinishLevel += FinishLevel;
        EventController.OnChangeAccessibility+=ChangeIconSize;
        initialItemSize = transform.localScale;
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
                gameObject.transform.localScale = new Vector3(initialItemSize.x * 2, initialItemSize.y * 2, 0);
                break;
        }
    }

    private void OnDisable()
    {
        EventController.ActivateItem -= CheckNextQuest;
        EventController.OnFinishLevel -= FinishLevel;
        EventController.OnChangeAccessibility-=ChangeIconSize;
    }

    private void FinishLevel()
    {
        gameObject.SetActive(false);
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
        ChangeIconSize(UIController.Instance.currentUIElementsSize);
    }
    private void OnMouseOver()
    {
        // se revisa si el tool seleccionado es el indicado para poder recoger el item
        // en caso negativo se cambia el icono del pointer a gris para indicar el error.
        if (ActionController.Instance.GetTool().action != item.collectTool.action || !canBeDigged)
        {
            Cursor.SetCursor(ActionController.Instance.GetTool().imgActionDisabled.texture, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnMouseExit()
    {
        if (ActionController.Instance.GetTool().action != item.collectTool.action || !canBeDigged)
        {
            Cursor.SetCursor(ActionController.Instance.GetTool().imgAction.texture, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ActionController.Instance.GetTool().action != item.collectTool.action || !canBeDigged)
        {
            ActionController.Instance.SetAction(false);
            return;
        }
        
        ActionController.Instance.SetAction(true);
        
        // Se revisa si recibe el collider del tool
        if (other.gameObject.name == "UpHit" || other.gameObject.name == "DownHit" || other.gameObject.name == "RightHit" || other.gameObject.name == "LeftHit")
        {
            EventController.DigObjectSoundEvent(MusicController.ActionSound.DigSound);
            Invoke("Activate", item.respawnTime);
            QuestController.Instance.GetItem(item);
            ActionController.Instance.SetAction(false);
            gameObject.SetActive(false);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (ActionController.Instance.GetTool().action != item.collectTool.action || !canBeDigged) {
            return;
        }
        ActionController.Instance.SetAction(true);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (ActionController.Instance.GetTool().action != item.collectTool.action || !canBeDigged)
        {
            ActionController.Instance.SetAction(false);
        }
    }
    private void Activate()
    {
        gameObject.SetActive(true);
    }

}
