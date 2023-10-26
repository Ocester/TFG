using UnityEngine;
using System;
using UnityEngine.UI;

public class PointerSelection : MonoBehaviour
{
    public event Action<string> OnPointerSelectedTool;
    private Color selectedColor = new Color32(233, 222, 110, 255);
    private Image btnImg;
    [SerializeField] private ToolsSO tools;
    
   
    private void Start()
    {
        btnImg = GetComponent<Image>();
    }

    public void PointerSelected()
    {
        
        OnPointerSelectedTool?.Invoke(tools.pointer);
        btnImg.color = selectedColor;
    }
}
