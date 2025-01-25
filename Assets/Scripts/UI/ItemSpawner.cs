using System;
using UI.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ItemSpawner : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private GameObject _itemPrefab;

        private RectTransform _rectTransform;
        private Window _parentWindow;
        private Transform _itemHolder;

        private GameObject _spawnedItem;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _parentWindow = GetComponentInParent<Window>();
            _itemHolder = _parentWindow.GetItemHolder();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_parentWindow)
            {
                _parentWindow.OnPointerDown(eventData);
            }
            if (!_spawnedItem)
            {
                _spawnedItem = Instantiate(_itemPrefab, _itemHolder);
                _spawnedItem.AddComponent<CanvasGroup>().blocksRaycasts = false;
                _spawnedItem.transform.position = _rectTransform.position;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_spawnedItem)
            {
                if (!ItemUtils.TrySlotItem(eventData, _spawnedItem))
                {
                    ItemUtils.DropItem(eventData, _spawnedItem);
                }

                _spawnedItem = null;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_spawnedItem)
            {
                return;
            }

            var drag = _spawnedItem.GetComponent<DragWindow>();
            if (drag)
            {
                drag.OnDrag(eventData);
            }
        }
    }
}