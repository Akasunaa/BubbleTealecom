using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class DragWindow : Window, IDragHandler, IDroppable
    {
        private RectTransform _rectTransform;

        void Start()
        {
            Init();
        }

        protected override void Init()
        {
            base.Init();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void ClampDrag()
        {
            var refRes = (_canvas.transform as RectTransform).sizeDelta;
            var anchoredPosition = _rectTransform.anchoredPosition;
            var x = anchoredPosition.x;
            var y = anchoredPosition.y;

            // left
            if (x - _rectTransform.sizeDelta.x * _rectTransform.localScale.x * _rectTransform.pivot.x < -refRes.x / 2.0f)
            {
                x = -(refRes.x / 2f) + _rectTransform.sizeDelta.x * _rectTransform.localScale.x * _rectTransform.pivot.x;
            }
            // right
            if (_rectTransform.anchoredPosition.x + (_rectTransform.sizeDelta.x * _rectTransform.localScale.x * (1f - _rectTransform.pivot.x)) >
                refRes.x / 2f)
            {
                x = refRes.x / 2f - _rectTransform.sizeDelta.x * _rectTransform.localScale.x * (1f - _rectTransform.pivot.x);
            }
            // up
            if (_rectTransform.anchoredPosition.y + _rectTransform.sizeDelta.y * _rectTransform.localScale.y * (1f - _rectTransform.pivot.y) >
                refRes.y / 2f)
            {
                y = refRes.y / 2f - _rectTransform.sizeDelta.y * _rectTransform.localScale.y * (1f - _rectTransform.pivot.y);
            }
            // down
            if (_rectTransform.anchoredPosition.y - _rectTransform.sizeDelta.y * _rectTransform.localScale.y * _rectTransform.pivot.y < -refRes.y / 2f)
            {
                y = -(refRes.y / 2f) + _rectTransform.sizeDelta.y * _rectTransform.localScale.y * _rectTransform.pivot.y;
            }

            _rectTransform.anchoredPosition = new Vector2(x, y);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
            
            ClampDrag();
        }

        public virtual void Drop(PointerEventData eventData)
        {
            Destroy(gameObject);
        }
    }
}