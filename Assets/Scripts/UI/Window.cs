using UnityEngine;

namespace UI
{
    public class Window : MonoBehaviour
    {
        protected Canvas _canvas;
        protected WindowStack _stack;

        void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            _canvas = GetComponentInParent<Canvas>();
            _stack = GetComponentInParent<WindowStack>();
        }
    }
}
