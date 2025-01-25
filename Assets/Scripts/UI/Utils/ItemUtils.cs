using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Utils
{
    public class ItemUtils
    {
        public static bool TrySlotItem(PointerEventData eventData, GameObject gameObject)
        {
            foreach (var hovered in eventData.hovered)
            {
                var slot = hovered.GetComponent<ItemSlot>();
                if (slot && slot.IsEmpty())
                {
                    slot.Receive(gameObject);
                    return true;
                }
            }

            return false;
        }
    }
}