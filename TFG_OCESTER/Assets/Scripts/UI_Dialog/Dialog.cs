using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField] private GameObject dialogObjet;
    [SerializeField] private GameObject charSpeakImg;
    [SerializeField] private GameObject arrowSprite;
    private TextMeshProUGUI textUI;
    private string [] dialogText;
    private int lineIndex;
    private bool dialogStarted;
    private bool dialogFinished;
    
    void Start()
    {
        EventController.dialogTextWrite += WriteText;
        EventController.pointObjectWrite += WritePointerText;
        EventController.changeDialogPic += ChangeCharacterPic;
        textUI = gameObject.GetComponent<TextMeshProUGUI>();
        textUI.enabled = false;
        dialogStarted = false;
        dialogFinished = true;
    }

    private void OnDisable()
    {
        EventController.dialogTextWrite -= WriteText;
        EventController.pointObjectWrite -= WritePointerText;
        EventController.changeDialogPic -= ChangeCharacterPic;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialogStarted && !dialogFinished)
        {
            if (!dialogStarted)
            {
                WriteLine();
            }
            else if (textUI.text == dialogText[lineIndex])
            {
                nextLine();
            }
        }
    }

    private void ChangeCharacterPic(Sprite charImg)
    {
        Debug.Log("character pic -> " + charSpeakImg.GetComponent<SpriteRenderer>().sprite);
        charSpeakImg.GetComponent<SpriteRenderer>().sprite = charImg;
    }
    private void WritePointerText(string txt)
    {
        dialogObjet.GetComponent<SpriteRenderer>().enabled=true;
        textUI.text = txt;
        textUI.enabled = true;
    }
    
    private void nextLine()
    {
        lineIndex++;
        if (lineIndex < dialogText.Length)
        {
            StartCoroutine(WriteLine());
        }
        else
        {
            dialogFinished = true;
        }
    }

    IEnumerator HideDialogText()
    {
        yield return new WaitForSecondsRealtime(2f);
        dialogObjet.GetComponent<SpriteRenderer>().enabled=false;
        textUI.enabled = false;
    }
    

    IEnumerator WriteLine( )
    {
        arrowSprite.GetComponent<SpriteRenderer>().enabled = false;
        
        textUI.text = string.Empty;
        foreach (char c in dialogText[lineIndex])
        {
            textUI.text += c;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        if (lineIndex == dialogText.Length-1)
        {
            StartCoroutine(HideDialogText());
        }
        else
        {
            arrowSprite.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    
    private void WriteText(QuestSO quest)
    {
        dialogStarted = true;
        dialogFinished = false;
        lineIndex = 0;
        if (quest.finished)
        {
            dialogText = quest.finishedQuestText;
        }
        else if (quest.itemsColected)
        {
            dialogText = quest.allIngredientsQuestText;
        }
        else
        {
            dialogText = quest.npcText;
        }
        dialogObjet.GetComponent<SpriteRenderer>().enabled=true;
        
        textUI.enabled = true;
        StartCoroutine(WriteLine());
    }
}
