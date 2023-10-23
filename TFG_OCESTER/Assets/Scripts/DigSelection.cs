using UnityEngine;
using System;
using UnityEngine.UI;

public class DigSelection : MonoBehaviour
{
    public event Action<string> OnDigSelectedTool;
    private Color selectedColor = new Color32(233, 222, 110, 255);
    private Image btnImg;
    private void Start()
    {
        btnImg = GetComponent<Image>();
    }
    public void DigSelected()
    {
        OnDigSelectedTool?.Invoke("dig");
        btnImg.color = selectedColor;
    }
}
