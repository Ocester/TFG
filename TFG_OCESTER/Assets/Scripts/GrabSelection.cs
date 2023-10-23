using UnityEngine;
using System;
using UnityEngine.UI;

public class GrabSelection : MonoBehaviour
{
    public event Action<string> OnGrabSelectedTool;
    private Color selectedColor = new Color32(233, 222, 110, 255);
    private Image btnImg;
    private void Start()
    {
        btnImg = GetComponent<Image>();
    }
    public void GrabSelected()
    {
        OnGrabSelectedTool?.Invoke("grab");
        btnImg.color = selectedColor;
        
    }
}
