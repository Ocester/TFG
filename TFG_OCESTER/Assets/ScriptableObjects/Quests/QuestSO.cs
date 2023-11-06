using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestSO", menuName = "Quest/ New Quest")]
public class QuestSO : ScriptableObject
{
    public string questName;
    public int questId;
    public NpcSO startingNPC;
    [TextArea] public string questText;
    [TextArea] public string npcText;
    [TextArea] public string finishedQuestText;
    [TextArea] public string allIngredientsQuestText;
    public RecipeSO recipe;
    //public QuestSO nextQuest;
    public bool started;
    public bool finished;
    public bool itemsColected;
    
    private void OnEnable()
    {
        started = false;
        finished = false;
        itemsColected = false;
    }
}
