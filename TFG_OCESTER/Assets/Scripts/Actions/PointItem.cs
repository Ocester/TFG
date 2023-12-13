using UnityEngine;

public class PointItem : MonoBehaviour
{
    [SerializeField] private ToolsSO pointerTool;
    private string _itemPointed;

    private void Start()
    {
        EventController.OnFinishLevel += FinishLevel;
    }

    private void OnDisable()
    {
        EventController.OnFinishLevel -= FinishLevel;
    }

    private void FinishLevel()
    {
        gameObject.SetActive(false);
    }
    private void OnMouseOver()
    {
        if (ActionController.Instance.GetTool().action != pointerTool.action)
        {
            return;
        }
        ActionController.Instance.SetAction(true);
    }
    private void OnMouseExit()
    {
        ActionController.Instance.SetAction(false);
    }
}
