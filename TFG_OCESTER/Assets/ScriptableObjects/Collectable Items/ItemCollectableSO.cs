using UnityEngine;

[CreateAssetMenu(fileName = "ItemCollectableSO", menuName = "Scriptable Object/Item Collectable", order = 1)]
public class ItemCollectableSO : ScriptableObject
{
    public string nameItem;
    public Sprite imgItem;
    public float respawnTime = 1.0f;
    public ToolsSO collectTool;
    public ItemTypeSO.ItemType itemType;
}
