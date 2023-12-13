using UnityEngine;
using UnityEngine.UI;

public class ToolSelection : MonoBehaviour
{
    private Color _selectedColor = new Color32(233, 222, 110, 255);
    private Image _btnImg;
    private Button _btn;
    public ToolsSO tool;
    private void Start()
    {
        _btnImg = GetComponent<Image>();
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(ToolSelected);
    }
   
    public void ToolSelected()
    {
        EventController.SelectedToolEvent(tool);
        _btnImg.color = _selectedColor;
    }
}
