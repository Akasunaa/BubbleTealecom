using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Recipe
{
    public List<IngredientState> finalIngredientStates = new List<IngredientState>();
    
    public static bool CompareIngredientList(List<IngredientState> one, List<IngredientState> two)
    {
        if (one == null && two == null)
        {
            return true;
        }
        if (one!.Count == 0 && two == null)
        {
            return true;
        }
        if (one.Count != two.Count)
        {
            return false;
        }

        foreach (var ingredientState in one)
        {
            if (two.Contains(ingredientState))
            {
                two.Remove(ingredientState);
            }
        }
    
        return two.Count == 0;
    }
}
