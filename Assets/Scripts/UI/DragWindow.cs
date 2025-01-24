using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class DragWindow : Window, IDragHandler, IPointerDownHandler
    {
        [SerializeField] private Canvas _canvas;

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

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_stack)
            {
                _stack.MoveWindowToFront(gameObject);
            }
        }
    }
}