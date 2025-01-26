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
    [NonSerialized] [HideInInspector] public List<IngredientState> oldIngredientState = new List<IngredientState>();

    [Header("Ingredient Visual Data")]
    public Color color;

    [CanBeNull]
    public IngredientState Transform(Transformation transformation, List<IngredientState> otherIngredients = null)
    {
        if (transformation == Transformation.None)
        {
            oldIngredientState.Clear();
            return this;
        }
        
        foreach (var possibleTransformation in possibleTransformations)
        {
            if (possibleTransformation.transformation == transformation && possibleTransformation.transformation == Transformation.MolecularAssembly)
            {
                IngredientState newIngredientState = possibleTransformation.newIngredientState;
                // IngredientState newIngredientState = CreateInstance<IngredientState>();
                // newIngredientState = possibleTransformation.newIngredientState;
                newIngredientState.oldIngredientState.Clear();
                newIngredientState.oldIngredientState.Add(this);
                if (otherIngredients != null)
                {
                    foreach (var otherIngredientState in otherIngredients)
                    {
                        newIngredientState.oldIngredientState.Add(otherIngredientState);
                    }
                }
                return newIngredientState;
            }
            if (possibleTransformation.transformation == transformation)
            {
                IngredientState newIngredientState = possibleTransformation.newIngredientState; // CreateInstance<IngredientState>();
                newIngredientState.oldIngredientState.Clear();
                newIngredientState = possibleTransformation.newIngredientState;
                newIngredientState.oldIngredientState.Add(this);
                return newIngredientState;
            }
        }

        return null;
    }
}

