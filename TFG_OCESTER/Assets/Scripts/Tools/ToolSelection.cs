using System;
using UnityEngine;
using UnityEngine.UI;

public class ToolSelection : MonoBehaviour
{
 
    private Color selectedColor = new Color32(233, 222, 110, 255);
    private Image btnImg;
    private Button btn;
    [SerializeField] private ToolsSO tool;
    private void Start()
    {
        btnImg = GetComponent<Image>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ToolSelected);
    }
   
    public void ToolSelected()
    {
        EventManager.SelectedToolEvent(tool);
        btnImg.color = selectedColor;
    }
}
