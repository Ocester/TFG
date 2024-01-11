using UnityEngine;
using TMPro;

public class MissionDetails : MonoBehaviour
{
    private TextMeshProUGUI _textUI;

    private void OnEnable()
    {
        EventController.MissionUpdateText += WriteMissionText;
        EventController.MissionTextClear += ClearMissionText;
        _textUI = gameObject.GetComponent<TextMeshProUGUI>();
        _textUI.enabled = false;
    }

    private void OnDisable()
    {
        EventController.MissionUpdateText -= WriteMissionText;
        EventController.MissionTextClear -= ClearMissionText;
    }

    private void ClearMissionText()
    {
        _textUI.text = "";
    }

    private void WriteMissionText(QuestSO quest)
    {
        _textUI.text = "";
        _textUI.text = quest.questName + "\n\n";
        foreach (var element in quest.recipe.elements)
        {
            _textUI.text += element.requiredItem.nameItem+ ": \n";
            _textUI.text += element.collectedQuantity;
            _textUI.text += " / ";
            _textUI.text += element.quantity + "\n\n";
            
        }
        _textUI.enabled = true;
    }
}
