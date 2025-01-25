using DG.Tweening;
using UI.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class GlassWindow : DragWindow, IPointerUpHandler
    {
        private Vector3 _start;
        private CanvasGroup _group;

        void Start()
        {
            Init();
        }

        protected override void Init()
        {
            base.Init();
            _start = transform.position;
            _group = gameObject.AddComponent<CanvasGroup>();
        }

        protected override void OnPointerDownFunc()
        {
            base.OnPointerDownFunc();
            DOTween.Kill(transform);
            _group.blocksRaycasts = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (ItemUtils.TrySlotItem(eventData, gameObject))
            {
                return;
            }

            Drop(eventData);
        }

        public override void Drop(PointerEventData eventData)
        {
            _group.blocksRaycasts = true;
            float interval = 0.0f;
            if (ItemUtils.TryGiveGlass(eventData, gameObject))
            {
                interval = 1.0f;
            }

            DOTween.Sequence(transform)
                .AppendInterval(interval)
                .Append(transform.DOMove(_start, 0.5f));
        }
    }
}