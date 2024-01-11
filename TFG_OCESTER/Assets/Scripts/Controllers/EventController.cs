using UnityEngine;
using System;

public class EventController : MonoBehaviour
{
    public static Action<ToolsSO> OnSelectedTool;
    public static Action<QuestSO> ActivateIconQuest;
    public static Action<QuestSO> DeactivateIconQuest;
    public static Action<QuestSO> ActivateItem;
    public static Action<QuestSO> CompleteQuest;
    public static Action<QuestSO> CheckNextQuest;
    public static Action<QuestSO> DialogTextWrite;
    public static Action<string> PointObjectWrite;
    public static Action<QuestSO> MissionUpdateText;
    public static Action MissionTextClear;
    public static Action<Sprite> ChangeDialogPic;
    public static Action OnFinishLevel;
    public static Action<MusicController.ActionSound> PointObjectSound;
    public static Action<MusicController.ActionSound> GrabSound;
    public static Action<MusicController.ActionSound> DialogSound;
    public static Action<MusicController.ActionSound> DigSound;
    public static Action<MusicController.ActionSound> CutSound;
    public static Action<MusicController.ActionSound> FinishLevelSound;
    public static Action<UIController.UIElementsSize> OnChangeAccessibility;
    public static Action<UIController.UIColorblindMode> OnChangeColorblindMode;
    
    public static EventController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
        ActivateItem?.Invoke(quest);
    }
    public static void QuestIconActivateEvent(QuestSO questInWaiting)
    {
        ActivateIconQuest?.Invoke(questInWaiting);
    }
    public static void QuestIconDeactivateEvent(QuestSO quest)
    {
        DeactivateIconQuest?.Invoke(quest);
    }
    public static void CompleteQuestEvent(QuestSO quest)
    {
        CompleteQuest?.Invoke(quest);
    }
    public static void CheckNextQuestEvent(QuestSO quest)
    {
        CheckNextQuest?.Invoke(quest);
    }
    public static void WriteDialogTextEvent(QuestSO questText)
    {
        DialogTextWrite?.Invoke(questText);
    }
    public static void PointObjectWriteEvent(string text)
    {
        PointObjectWrite?.Invoke(text);
    }
    public static void WriteMissionUpdateEvent(QuestSO quest)
    {
        MissionUpdateText?.Invoke(quest);
    }
    public static void ClearMissionTextEvent()
    {
        MissionTextClear?.Invoke();
    }
    public static void ChangeDialogPicEvent(Sprite charImg)
    {
        ChangeDialogPic?.Invoke(charImg);
    }
    public static void FinishLevelEvent()
    {
        OnFinishLevel?.Invoke();
    }
    public static void PointObjectSoundEvent(MusicController.ActionSound soundType)
    {
        PointObjectSound?.Invoke(soundType);
    }
    public static void DialogSoundEvent(MusicController.ActionSound soundType)
    {
        DialogSound?.Invoke(soundType);
    }
    public static void DigObjectSoundEvent(MusicController.ActionSound soundType)
    {
        DigSound?.Invoke(soundType);
    }
    public static void GrabObjectSoundEvent(MusicController.ActionSound soundType)
    {
        GrabSound?.Invoke(soundType);
    }
    public static void CutObjectSoundEvent(MusicController.ActionSound soundType)
    {
        CutSound?.Invoke(soundType);
    }
    public static void FinishLevelSoundEvent(MusicController.ActionSound soundType)
    {
        FinishLevelSound?.Invoke(soundType);
    }
    public static void SelectedChangeAccessibilityEvent(UIController.UIElementsSize size)
    {
        OnChangeAccessibility?.Invoke(size);
    }
    public static void SelectedColorblindMode(UIController.UIColorblindMode newColorblindMode)
    {
        OnChangeColorblindMode?.Invoke(newColorblindMode);
    }
}
