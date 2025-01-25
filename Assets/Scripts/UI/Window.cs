using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class Window : MonoBehaviour, IPointerDownHandler
    {
        protected Canvas _canvas;
        private WindowStack _stack;

        void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            _canvas = GetComponentInParent<Canvas>();
            _stack = GetComponentInParent<WindowStack>();
        }

        protected virtual void OnPointerDownFunc() {}

        public Transform GetItemHolder()
        {
            if (_stack)
            {
                return _stack.GetItemHolder();
            }

            return null;
        }

        public void Focus()
        {
            if (_stack)
            {
                _stack.MoveWindowToFront(gameObject);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Focus();
            OnPointerDownFunc();
        }
    }
}
