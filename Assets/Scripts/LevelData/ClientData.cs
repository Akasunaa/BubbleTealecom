using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelData
{
    [Serializable]
    public enum ClientType
    {
        HumanOrInfected,
        Slime,
    }

    [Serializable]
    [CreateAssetMenu(fileName = "ClientData", menuName = "ScriptableObjects/ClientData", order = 1)]
    public class ClientData : ScriptableObject
    {
        public Sprite _sprite;
        public ClientRecipeElements _recipe;
        public IngredientStateImageList _ingredientsLanguage;
        public TransformationImageList _transformationLanguage;
        public float _timer;
        public SoundManager.Sound _entrySound;
        public SoundManager.Sound _correctAudio;
        public SoundManager.Sound _incorrectAudio;
        public ClientType _type;
    }
}