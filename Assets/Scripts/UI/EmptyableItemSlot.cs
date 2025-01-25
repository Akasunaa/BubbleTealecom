using UI.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class EmptyableItemSlot : ItemSlot, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        private DragWindow _parentDrag;
        private Window _parentWindow;
        private Transform _itemHolder;

        private Vector3 _prevItemScale;

        private void Start()
        {
            Init();
        }

        protected override void Init()
        {
            base.Init();
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
            Init();
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
                    ItemUtils.DropItem(eventData, item);
                }
            }
            _rectTransform = GetComponent<RectTransform>();
        }

        public override void Receive(GameObject item)
        {
            _prevItemScale = item.transform.localScale;
            base.Receive(item);
        }
    }
}