using System;
using System.Collections.Generic;
using LevelData.EnumHolders;
using Unity.VisualScripting;
using UnityEngine;

namespace LevelData
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelObject _level; // TODO load dynamically

        [SerializeField] private SpriteRenderer _outsideSprite;
        [SerializeField] private Transform _categoriesParent;
        [SerializeField] private List<MachinesEnumHolder> _machines;

        [Header("Spawners")]
        [SerializeField] private Transform _fruitsSpawner;
        [SerializeField] private Transform _teasSpawner;
        [SerializeField] private Transform _bubblesSpawner;
        [SerializeField] private Transform _toppingsSpawner;

        private void Start()
        {
            Load(LevelDataHolder.CurrentLevel ? LevelDataHolder.CurrentLevel : _level);
        }

        public void Load(LevelObject level)
        {
            _outsideSprite.sprite = level._outsideSprite;

            // disable empty category button
            foreach (var holder in _categoriesParent.GetComponentsInChildren<CategoriesEnumHolder>())
            {
                switch (holder.value)
                {
                    case CategoriesEnum.Fruits:
                        holder.gameObject.SetActive(level._fruits.Count > 0);
                        break;
                    
                    case CategoriesEnum.Teas:
                        holder.gameObject.SetActive(level._teas.Count > 0);
                        break;
                    
                    case CategoriesEnum.Bubbles:
                        holder.gameObject.SetActive(level._bubbles.Count > 0);
                        break;
                    
                    case CategoriesEnum.Toppings:
                        holder.gameObject.SetActive(level._toppings.Count > 0);
                        break;
                }
            }

            // disable unused machines
            foreach (var holder in _machines)
            {
                holder.gameObject.SetActive(level._machines.Contains(holder.value));
            }
            
            // disable unused spawners
            foreach (var holder in _fruitsSpawner.GetComponentsInChildren<FruitsEnumHolder>())
            {
                holder.gameObject.SetActive(level._fruits.Contains(holder.value));
            }
            foreach (var holder in _teasSpawner.GetComponentsInChildren<TeasEnumHolder>())
            {
                holder.gameObject.SetActive(level._teas.Contains(holder.value));
            }
            foreach (var holder in _bubblesSpawner.GetComponentsInChildren<BubblesEnumHolder>())
            {
                holder.gameObject.SetActive(level._bubbles.Contains(holder.value));
            }
            foreach (var holder in _toppingsSpawner.GetComponentsInChildren<ToppingsEnumHolder>())
            {
                holder.gameObject.SetActive(level._toppings.Contains(holder.value));
            }

            // setup clients
            ClientManager.Instance.SetupClients(new List<ClientData>(level._clients));
        }
    }
}