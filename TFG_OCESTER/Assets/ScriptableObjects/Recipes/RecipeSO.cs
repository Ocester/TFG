using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeSO", menuName = "Scriptable Object/New Recipe", order = 1)]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public int recipeId;
   
    [System.Serializable]
    public class RecipeElement
    {
        public ItemCollectableSO requiredItem;
        public int quantity;
        public int collectedQuantity=0;
    }
    public List<RecipeElement> elements = new List<RecipeElement>();
    
    private void OnEnable()
    {
        // Se llama al método que reinicia la collectedQuantity a 0 cada vez que el ScriptableObject se activa
        ResetCollectedQuantitySO();
    }

    // Método para reiniciar la collectedQuantity a 0
    private void ResetCollectedQuantitySO()
    {
        foreach (var recipeElement in elements)
        {
            recipeElement.collectedQuantity = 0;
        }
    }
    
}
