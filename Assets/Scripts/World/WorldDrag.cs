using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace World
{
    public class WorldDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        private Vector3 _start;
        private Vector3 _offset;

        private void Start()
        {
            _start = transform.position;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            DOTween.Kill(transform);
            _offset = transform.position - Camera.main.ScreenToWorldPoint(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = _offset + Camera.main.ScreenToWorldPoint(eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // todo check bin or client
            transform.DOMove(_start, 0.5f);
        }
    }
}