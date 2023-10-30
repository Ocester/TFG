using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemTypeSO", menuName = "Scriptable Object/Item Type", order = 1)]
public class ItemTypeSO : ScriptableObject
{
    
    public enum ItemType
    {
        Egg,
        Fruit,
        Vegetable,
        Tree
    }
}
