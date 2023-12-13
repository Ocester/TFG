using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private GameObject tooltip;
    [SerializeField] private ToolsSO tool;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
       tooltip.SetActive(true);
       tooltipText.text = tool.tooltip;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }
}
