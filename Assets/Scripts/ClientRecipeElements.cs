using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[Serializable]
public class ClientRecipeElement
{
    public List<IngredientState> ingredientStates;
    public Transformation transform;

    public IngredientState ToIngredientState()
    {
        if (ingredientStates.Count > 1)
        {
            List<IngredientState> otherIngredientStates = ingredientStates.GetRange(1, ingredientStates.Count - 1);
            return ingredientStates[0].Transform(transform, otherIngredientStates);
        }

        return ingredientStates[0].Transform(transform);
    }
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
            recipe.finalIngredientStates.Add(clientRecipeElement.ToIngredientState());
        }
        return recipe;
    }
    
    public Recipe ToRecipe(IngredientState toRemove)
    {
        Recipe recipe = new Recipe();
        foreach (var clientRecipeElement in recipeElements)
        {
            if (!clientRecipeElement.ingredientStates.Contains(toRemove))
            {
                recipe.finalIngredientStates.Add(clientRecipeElement.ToIngredientState());
            }
        }
        return recipe;
    }
}
