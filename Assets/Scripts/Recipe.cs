using System;
using System.Collections.Generic;
using System.Linq;
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
            var inTwo = two.Find(ig =>
                ig.sprite == ingredientState.sprite &&
                           SimpleCompareList(ig.oldIngredientState, ingredientState.oldIngredientState)
            );
            if (inTwo)
            {
                two.Remove(inTwo);
            }
        }
    
        return two.Count == 0;
    }

    private static bool SimpleCompareList(List<IngredientState> one, List<IngredientState> two)
    {
        foreach (var ingredientState in one)
        {
            if (!two.Find(ig => ig.sprite == ingredientState.sprite))
            {
                return false;
            }
        }
        return one.Count == two.Count;
    }
}
