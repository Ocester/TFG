using UnityEngine;

public class CutItem : MonoBehaviour
{
    [SerializeField] private ItemCollectableSO item;

    private void Start()
    {
        EventController.OnFinishLevel += FinishLevel;
    }
    private void FinishLevel()
    {
        gameObject.SetActive(false);
    }

    private void OnMouseOver()
    {
        if (ActionController.Instance.GetTool().action != item.collectTool.action)
        {
            Cursor.SetCursor(ActionController.Instance.GetTool().imgActionDisabled.texture, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnMouseExit()
    {
        
        if (ActionController.Instance.GetTool().action != item.collectTool.action )   
        {
            Cursor.SetCursor(ActionController.Instance.GetTool().imgAction.texture, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (ActionController.Instance.GetTool().action != item.collectTool.action)
        {
            ActionController.Instance.SetAction(false);
            return;
        }
        ActionController.Instance.SetAction(true);
        if (other.gameObject.name == "UpHit" || other.gameObject.name == "DownHit" || other.gameObject.name == "RightHit" || other.gameObject.name == "LeftHit")
        {
            EventController.CutObjectSoundEvent(MusicController.ActionSound.CutSound);
            ActionController.Instance.SetAction(false);
            gameObject.SetActive(false);
            if (item.respawnTime != 0)
            {
                Invoke("Activate", item.respawnTime);
            }
        };
    }

    private void OnTriggerStay2D(Collider2D other)
    {
         
        if (ActionController.Instance.GetTool().action != item.collectTool.action)
        {
            return;
        }
        ActionController.Instance.SetAction(true);
    }

    private void Activate()
    {
        gameObject.SetActive(true);
    }
}
