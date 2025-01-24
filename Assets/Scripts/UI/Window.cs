using UnityEngine;

namespace UI
{
    public class Window : MonoBehaviour
    {
        protected WindowStack _stack;

        void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            _stack = GetComponentInParent<WindowStack>();
        }
    }
}
