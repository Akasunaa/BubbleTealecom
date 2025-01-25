using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[Serializable]
public class ClientRecipeElement
{
    public List<IngredientState> ingredientStates;
    public Transformation transform;
}

[Serializable]
[CreateAssetMenu(fileName = "ClientRecipeElements", menuName = "ScriptableObjects/ClientRecipeElements", order = 1)]
public class ClientRecipeElements : ScriptableObject
{
    public List<ClientRecipeElement> recipeElements;

    public Recipe ToRecipe()
    {
        Recipe recipe = new Recipe();
        foreach (var clientRecipeElement in recipeElements)
        {
            if (clientRecipeElement.ingredientStates.Count > 1)
            {
                List<IngredientState> otherIngredientStates = clientRecipeElement.ingredientStates.GetRange(1, clientRecipeElement.ingredientStates.Count - 1);
                IngredientState ingredientState = clientRecipeElement.ingredientStates[0].Transform(clientRecipeElement.transform, otherIngredientStates);
                recipe.finalIngredientStates.Add(ingredientState);
            }
            else
            {
                IngredientState ingredientState = clientRecipeElement.ingredientStates[0].Transform(clientRecipeElement.transform);
                recipe.finalIngredientStates.Add(ingredientState);
            }
        }
        return recipe;
    }
}
