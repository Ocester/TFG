using UnityEngine;

public class GrabItem : MonoBehaviour
{
    
    [SerializeField] private ItemCollectableSO item;
    [SerializeField] private bool insideArea = false;
    [SerializeField] private bool canBeGrabbed = false;
    private float _maxDistance = 1.5f;
    private Ray _ray;
    private RaycastHit2D _hit;
    private GameObject _player;
    private Vector2 _playerPosition;

    private void Start()
    {
        EventController.ActivateItem += CheckNextQuest;
        EventController.OnFinishLevel += FinishLevel;
        _player = GameObject.FindWithTag("Player");
    }
    private void FinishLevel()
    {
        gameObject.SetActive(false);
    }

    public ItemCollectableSO GetItem()
    {
        return item;
    }

    // Revisa si el objeto es un item de la quest acutal, si es así será activado para ser recogido, sino no
    private void IsCurrentQuestItem(QuestSO checkQuest)
    {
        if (!QuestController.Instance.GetCurrentQuest() && item)
        {
            return;
        }
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
        if (ActionController.Instance.GetTool().action != item.collectTool.action)
        {
            Cursor.SetCursor(ActionController.Instance.GetTool().imgActionDisabled.texture, Vector2.zero, CursorMode.Auto);
            return;
        }

        if (!canBeGrabbed)
        {
            Cursor.SetCursor(ActionController.Instance.GetTool().imgActionDisabled.texture, Vector2.zero, CursorMode.Auto);
            return;
        }
        
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
        if (insideArea)
        {
            ActionController.Instance.SetAction(true);
        }
        
    }

    private void OnMouseExit()
    {
        if (ActionController.Instance.GetTool().action != item.collectTool.action)
        {
            Cursor.SetCursor(ActionController.Instance.GetTool().imgAction.texture, Vector2.zero, CursorMode.Auto);
        }
        if (!canBeGrabbed)
        {
            Cursor.SetCursor(ActionController.Instance.GetTool().imgAction.texture, Vector2.zero, CursorMode.Auto);
        }
        insideArea = false;
    }
}
