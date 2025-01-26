using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelData
{
    [Serializable]
    [CreateAssetMenu(fileName = "ClientData", menuName = "ScriptableObjects/ClientData", order = 1)]
    public class ClientData : ScriptableObject
    {
        public Sprite _sprite;
        public ClientRecipeElements _recipe;
        public IngredientStateImageList _ingredientsLanguage;
        public TransformationImageList _transformationLanguage;
        public float _timer;
    }
}