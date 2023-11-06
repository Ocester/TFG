using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField] private GameObject dialogObjet;
    [SerializeField] private GameObject charSpeakImg;
    private TextMeshProUGUI textUI;
    
    void Start()
    {
        EventController.dialogTextWrite += WriteText;
        EventController.changeDialogPic += ChangeCharacterPic;
        textUI = gameObject.GetComponent<TextMeshProUGUI>();
        textUI.enabled = false;
    }

    private void ChangeCharacterPic(Sprite charImg)
    {
        Debug.Log("character pic -> " + charSpeakImg.GetComponent<SpriteRenderer>().sprite);
        charSpeakImg.GetComponent<SpriteRenderer>().sprite = charImg;
    }

    private void WriteText(string text)
    {
        dialogObjet.GetComponent<SpriteRenderer>().enabled=true;
        textUI.text = text;
        textUI.enabled = true;
    }
    
}
