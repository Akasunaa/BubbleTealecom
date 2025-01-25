using UI.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace UI
{
    public class ItemSlot : MonoBehaviour
    {
        protected RectTransform _rectTransform;
        protected GameObject _item;

        public GameObject GetItem() { return _item; }

        private void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public virtual void Receive(GameObject item)
        {
            _item = item;
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

        public virtual bool IsEmpty()
        {
            return _item == null;
        }
    }
}