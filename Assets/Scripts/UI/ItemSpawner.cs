using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ItemSpawner : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private Transform _itemHolder;

        private RectTransform _rectTransform;
        private GameObject _spawnedItem;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_spawnedItem)
            {
                _spawnedItem = Instantiate(_itemPrefab, _itemHolder);
                _spawnedItem.transform.position = _rectTransform.position;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_spawnedItem)
            {
                Destroy(_spawnedItem);
                _spawnedItem = null;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            var drag = _spawnedItem.GetComponent<DragWindow>();
            if (drag)
            {
                drag.OnDrag(eventData);
            }
        }
    }
}