using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Collections;
using UnityEngine;

[Serializable]
public struct IngredientTransformation
{
    public Transformation transformation;
    public IngredientState newIngredientState;
}

[Serializable]
[CreateAssetMenu(fileName = "IngredientState", menuName = "ScriptableObjects/IngredientState", order = 1)]
public class IngredientState : ScriptableObject
{
    public Sprite sprite;
    [SerializeField] public List<IngredientTransformation> possible_transformations;
    
    [CanBeNull]
    public IngredientState Transform(Transformation transformation)
    {
        foreach (var possible_transformation in possible_transformations)
        {
            if (possible_transformation.transformation == transformation)
            {
                return possible_transformation.newIngredientState;
            }
        }

        return null;
    }
}