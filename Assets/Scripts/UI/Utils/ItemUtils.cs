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

        public static bool TryGiveGlass(PointerEventData eventData, GameObject glass)
        {
            foreach (var hovered in eventData.hovered)
            {
                var receiver = hovered.GetComponent<IGlassReceiver>();
                if (receiver != null)
                {
                    receiver.ReceiveGlass(glass);
                    return true;
                }
            }

            return false;
        }

        public static void DropItem(PointerEventData eventData, GameObject gameObject)
        {
            var droppable = gameObject.GetComponent<IDroppable>();
            if (droppable != null)
            {
                droppable.Drop(eventData);
            }
        }
    }
}