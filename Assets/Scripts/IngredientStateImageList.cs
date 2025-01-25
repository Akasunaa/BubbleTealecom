using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class IngredientStateImage
{
    public IngredientState ingredientState;
    public Sprite image;
}

[Serializable]
[CreateAssetMenu(fileName = "IngredientStateImageList", menuName = "ScriptableObjects/IngredientStateImageList", order = 1)]
public class IngredientStateImageList: ScriptableObject
{
    public List<IngredientStateImage> ingredientStatesImages;
}
