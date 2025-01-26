using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{
    public IngredientState ingredientState;

    public void Transform(Transformation t, List<Ingredient> otherIngredients = null)
    {
        IngredientState newIngredientState;
        if (otherIngredients != null)
        {
            newIngredientState = ingredientState.Transform(t, otherIngredients.Select(i => i.ingredientState).ToList());
        }
        else
        {
            newIngredientState = ingredientState.Transform(t);
        }

        if (newIngredientState == null)
        {
            Debug.LogError("NO TRANSFORMATION");
        }
        else
        {
            UpdateValue(newIngredientState);
        }
    }

    private void UpdateValue(IngredientState ingredientState)
    {
        this.ingredientState = ingredientState;
        GetComponent<Image>().sprite = ingredientState.sprite;
    }
}
