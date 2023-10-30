using UnityEngine;
using System;
using UnityEngine.UI;

public class DigItem : MonoBehaviour
{
    [SerializeField] private ItemCollectableSO item;
    private ActionController selectedAction;
    private Sprite currentCursor;
    
    // Start is called before the first frame update
    void Start()
    {
        selectedAction = GameObject.FindObjectOfType<ActionController>();
    }

    // Update is called once per frame
    private void OnMouseOver()
    {
        if (selectedAction.getTool().action != item.collectTool.action)
        {
            Cursor.SetCursor(selectedAction.getTool().imgActionDisabled.texture, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnMouseExit()
    {
        if (selectedAction.getTool().action != item.collectTool.action)
        {
            Cursor.SetCursor(selectedAction.getTool().imgAction.texture, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (selectedAction.getTool().action != item.collectTool.action)
        {
            selectedAction.SetAction(false);
            return;
        }
        selectedAction.SetAction(true);
        if (other.gameObject.name == "UpHit" || other.gameObject.name == "DownHit" || other.gameObject.name == "RightHit" || other.gameObject.name == "LeftHit")
        {
            Debug.Log("DIG HIT!!!!");
            Invoke("Activate", item.respawnTime);
            gameObject.SetActive(false);
            
            // hay que programar la inclusión en la UI del vegetal
            
        };
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (selectedAction.getTool().action != item.collectTool.action) {
            return;
        }
        selectedAction.SetAction(true);
    }

    private void Activate()
    {
        gameObject.SetActive(true);
    }

}
