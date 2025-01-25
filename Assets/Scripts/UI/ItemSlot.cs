using UI.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace UI
{
    public class ItemSlot : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        private RectTransform _rectTransform;
        private DragWindow _parentDrag;
        private Window _parentWindow;
        private Transform _itemHolder;

        private GameObject _item;
        private Vector3 _prevItemScale;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _parentDrag = GetComponentInParent<DragWindow>();
            _parentWindow = GetComponentInParent<Window>();
            _itemHolder = _parentWindow.GetItemHolder();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_item)
            {
                if (_parentDrag)
                {
                    _parentDrag.OnDrag(eventData);
                }
            }
            else
            {
                var drag = _item.GetComponent<DragWindow>();
                if (drag)
                {
                    drag.OnDrag(eventData);
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_parentWindow)
            {
                _parentWindow.OnPointerDown(eventData);
            }

            if (_item)
            {
                _item.transform.SetParent(_itemHolder);
                _item.transform.position = _rectTransform.position;
                _item.transform.localScale = _prevItemScale;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_item)
            {
                var item = _item;
                _item = null;
                if (!ItemUtils.TrySlotItem(eventData, item))
                {
                    Destroy(item);
                }
            }
        }

        public void Receive(GameObject item)
        {
            _item = item;
            _prevItemScale = item.transform.localScale;
            var rectTransform = _item.transform as RectTransform;
            rectTransform.SetParent(transform);
            rectTransform.anchoredPosition = Vector2.zero;

            var mySizeDelta = _rectTransform.sizeDelta;
            var sizeDelta = rectTransform.sizeDelta;
            float xRatio = mySizeDelta.x / sizeDelta.x;
            float yRatio = mySizeDelta.y / sizeDelta.y;
            float ratio = Mathf.Min(xRatio, yRatio);

            rectTransform.localScale = ratio * Vector3.one;
        }

        public bool IsEmpty()
        {
            return _item == null;
        }
    }
}