using UnityEngine.EventSystems;

namespace UI
{
    public interface IDroppable
    {
        public void Drop(PointerEventData eventData);
    }
}