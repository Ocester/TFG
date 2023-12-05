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
    private UIController uiController;
    private TextMeshProUGUI textUI;
    private string [] dialogText;
    private int lineIndex;
    private bool dialogStarted;
    private bool dialogFinished;
    //private bool isPointerDialogUp;
    
    void Start()
    {
        EventController.dialogTextWrite += WriteText;
        EventController.pointObjectWrite += WritePointerText;
        EventController.changeDialogPic += ChangeCharacterPic;
        uiController = FindObjectOfType<UIController>();
        textUI = gameObject.GetComponent<TextMeshProUGUI>();
        textUI.enabled = false;
        dialogStarted = false;
        dialogFinished = true;
        //isPointerDialogUp = false;
    }

    private void OnDisable()
    {
        EventController.dialogTextWrite -= WriteText;
        EventController.pointObjectWrite -= WritePointerText;
        EventController.changeDialogPic -= ChangeCharacterPic;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialogStarted && !dialogFinished )
        {
            if (!dialogStarted)
            {
                StartCoroutine(WriteLine());
            }
            else if (textUI.text == dialogText[lineIndex])
            {
                nextLine();
            }
        }
        
        /*if (Input.GetMouseButtonDown(0) && dialogStarted && dialogFinished)
        {
            EndDialog();
        }
       
        if (Input.GetMouseButtonDown(0) && isPointerDialogUp)
        {
            EndPointerDialog();
        }
        */
        
    }

    private void EndDialog()
    {
        dialogObjet.GetComponent<SpriteRenderer>().enabled=false;
        arrowSprite.SetActive(false);
        textUI.enabled = false;
        dialogStarted = false;
    }

    private void EndPointerDialog()
    {
        dialogObjet.GetComponent<SpriteRenderer>().enabled=false;
        arrowSprite.SetActive(false);
        textUI.enabled = false;
        //isPointerDialogUp = false;
    }


    private void ChangeCharacterPic(Sprite charImg)
    {
        charSpeakImg.GetComponent<SpriteRenderer>().sprite = charImg;
    }
    private void WritePointerText(string txt)
    {
        //isPointerDialogUp = true;
        dialogObjet.GetComponent<SpriteRenderer>().enabled = true;
        textUI.enabled = true;
        textUI.text = txt;
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
            uiController.ActivateTools();
        }
    }
    IEnumerator WriteLine( )
    {   
        Time.timeScale = 0f;
        textUI.text = string.Empty;
        foreach (char c in dialogText[lineIndex])
        {
            textUI.text += c;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        if (lineIndex == dialogText.Length-1)
        {
            arrowSprite.SetActive(false);
        }
        else if (lineIndex < dialogText.Length-1)
        {
            arrowSprite.SetActive(true);
        }
        Time.timeScale = 1f;
    }
    
    private void WriteText(QuestSO quest)
    {
        EventController.DialogSound(MusicController.ActionSound.dialogSound);
        uiController.DeactivateTools();
        ChangeCharacterPic(quest.startingNPC.imgNpc);
        arrowSprite.SetActive(false);
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
