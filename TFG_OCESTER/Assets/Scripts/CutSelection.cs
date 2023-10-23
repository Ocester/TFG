using UnityEngine;
using System;
using UnityEngine.UI;


public class CutSelection : MonoBehaviour
{
    public event Action<string> OnCutSelectedTool;
    private Color selectedColor = new Color32(233, 222, 110, 255);
    private Image btnImg;
   
    private void Start()
    {
        btnImg = GetComponent<Image>();
    }

    public void CutSelected()
    {
        OnCutSelectedTool?.Invoke("cut");
        btnImg.color = selectedColor;

    }
    
}
