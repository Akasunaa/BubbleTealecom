using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Collections;
using UnityEngine;

[Serializable]
public struct IngredientTransformation
{
    public Transformation transformation;
    public List<IngredientState> otherRequiredIngredientStates; // other ingredients present in the machine
    public IngredientState newIngredientState; // one transformation always produce one ingredient
}

[Serializable]
[CreateAssetMenu(fileName = "IngredientState", menuName = "ScriptableObjects/IngredientState", order = 1)]
public class IngredientState : ScriptableObject
{
    public Sprite sprite;
    [SerializeField] public List<IngredientTransformation> possibleTransformations;
    
    [CanBeNull]
    public IngredientState Transform(Transformation transformation, List<IngredientState> otherIngredients = null)
    {
        foreach (var possibleTransformation in possibleTransformations)
        {
            if (possibleTransformation.transformation == transformation && CompareIngredientList(possibleTransformation.otherRequiredIngredientStates, otherIngredients))
            {
                return possibleTransformation.newIngredientState;
            }
        }

        return null;
    }
    
    bool CompareIngredientList(List<IngredientState> one, List<IngredientState> two)
    {
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

