using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.VisualScripting;
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
        if (transformation == Transformation.None)
        {
            return this;
        }
        
        foreach (var possibleTransformation in possibleTransformations)
        {
            if (possibleTransformation.transformation == transformation && Recipe.CompareIngredientList(possibleTransformation.otherRequiredIngredientStates, otherIngredients))
            {
                return possibleTransformation.newIngredientState;
            }
        }

        return null;
    }
}

