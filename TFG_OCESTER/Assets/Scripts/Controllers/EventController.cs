using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class EventController : MonoBehaviour
{
    public static Action<ToolsSO> OnSelectedTool;
    public static Action<QuestSO> activateIconQuest;
    public static Action<QuestSO> deactivateIconQuest;
    public static Action<QuestSO> activateItem;
    public static Action<QuestSO> completeQuest;
    public static Action<QuestSO> checkNextQuest;
    public static Action<string> dialogTextWrite;
    public static Action<QuestSO> missionUpdateText;
    public static Action missionTextClear;
    public static Action<Sprite> changeDialogPic;
    
    public static EventController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static void SelectedToolEvent(ToolsSO tool)
    {
        OnSelectedTool?.Invoke(tool);
    }
    public static void ActivateItemEvent(QuestSO quest)
    {
        activateItem?.Invoke(quest);
    }
    public static void QuestIconActivateEvent(QuestSO questInWaiting)
    {
        activateIconQuest?.Invoke(questInWaiting);
    }
    public static void QuestIconDeactivateEvent(QuestSO quest)
    {
        deactivateIconQuest?.Invoke(quest);
    }
    public static void CompleteQuestEvent(QuestSO quest)
    {
        completeQuest?.Invoke(quest);
    }
    public static void CheckNextQuestEvent(QuestSO quest)
    {
        checkNextQuest?.Invoke(quest);
    }
    public static void WriteDialogTextEvent(string text)
    {
        dialogTextWrite?.Invoke(text);
    }
    public static void WriteMissionUpdateEvent(QuestSO quest)
    {
        missionUpdateText?.Invoke(quest);
    }
    public static void ClearMissionTextEvent()
    {
        missionTextClear?.Invoke();
    }
    public static void ChangeDialogPicEvent(Sprite charImg)
    {
        changeDialogPic?.Invoke(charImg);
    }

}
