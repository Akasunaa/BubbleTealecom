using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClientSlime : Client
{
    [SerializeField] private IngredientState _ingredientInStomac;
    [SerializeField] private Transform _ingredientInStomacTransform;

    public override void SetupClientSpecific()
    {
        GameObject clientElement = Instantiate(clientRecipeElementPrefab, bubbleFirstElementTransform);
        clientElement.GetComponent<Image>().sprite = GetSpriteFrom(_ingredientInStomac);
        clientElement.GetComponent<Image>().transform.position = _ingredientInStomacTransform.position;
    }

    public override Sprite FindWrongItem(ClientRecipeElements clientRecipeElements, Recipe glassRecipe)
    {
        foreach (var clientRecipeElement in clientRecipeElements.recipeElements)
        {
            if (clientRecipeElement.ingredientStates.Contains(_ingredientInStomac))
            {
                break;
            }
            var ingredientState = clientRecipeElement.ToIngredientState();
            if (!glassRecipe.finalIngredientStates.Contains(ingredientState))
            {
                // Look for base ingredient in the recipe
                var wrongBaseIngredientState = clientRecipeElement.ingredientStates.Find(baseIngredientState =>
                    !glassRecipe.finalIngredientStates.Find(recipeIngredientState => recipeIngredientState == baseIngredientState ||
                        recipeIngredientState.oldIngredientState.Contains(baseIngredientState)) ==
                    baseIngredientState);
                if (wrongBaseIngredientState)
                {
                    return GetSpriteFrom(wrongBaseIngredientState);
                }

                return GetSpriteFrom(clientRecipeElement.transform);
            }
        }

        foreach (var glassIngredientState in glassRecipe.finalIngredientStates)
        {
            if (!recipe.finalIngredientStates.Contains(glassIngredientState))
            {
                return GetSpriteFrom(glassIngredientState);
            }
        }

        Debug.LogError("Should not happen");
        return null;
    }

    public override Recipe ToRecipe(ClientRecipeElements recipeElements)
    {
        return recipeElements.ToRecipe(_ingredientInStomac);
    }
}
