using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelData
{
    [Serializable]
    [CreateAssetMenu(fileName = "LevelObject", menuName = "ScriptableObjects/LevelObject", order = 1)]
    public class LevelObject : ScriptableObject
    {
        public List<FruitsEnum> _fruits;
        public List<TeasEnum> _teas;
        public List<BubblesEnum> _bubbles;
        public List<ToppingsEnum> _toppings;
        public List<MachinesEnum> _machines;
        public List<GameObject> _clients;
    }
}