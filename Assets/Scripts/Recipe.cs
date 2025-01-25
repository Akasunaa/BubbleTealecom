using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe", order = 1)]
public class Recipe : ScriptableObject
{
    public List<IngredientState> final_ingredient_states;
}
