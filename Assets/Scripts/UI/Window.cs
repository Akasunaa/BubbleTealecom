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

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_stack)
            {
                _stack.MoveWindowToFront(gameObject);
            }
        }
    }
}
