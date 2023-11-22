using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionDetails : MonoBehaviour
{
    private TextMeshProUGUI textUI;

    private void OnEnable()
    {
        EventController.missionUpdateText += WriteMissionText;
        EventController.missionTextClear += ClearMissionText;
        textUI = gameObject.GetComponent<TextMeshProUGUI>();
        textUI.enabled = false;
    }

    private void OnDisable()
    {
        EventController.missionUpdateText -= WriteMissionText;
        EventController.missionTextClear -= ClearMissionText;
    }

    private void ClearMissionText()
    {
        textUI.text = "";
    }

    private void WriteMissionText(QuestSO quest)
    {
        textUI.text = "";
        textUI.text = quest.questName + "\n";
        foreach (var element in quest.recipe.elements)
        {
            textUI.text += element.requiredItem.nameItem+ ": \n";
            textUI.text += element.collectedQuantity;
            textUI.text += " / ";
            textUI.text += element.quantity + "\n";
            
        }
        textUI.enabled = true;
    }
}
