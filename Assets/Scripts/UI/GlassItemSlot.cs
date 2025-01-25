using UnityEngine;

namespace UI
{
    public class GlassItemSlot : ItemSlot
    {
        [SerializeField] private Transform _slotList;

        public override void Receive(GameObject item)
        {
            item.transform.localScale = Vector3.one;
            item.transform.SetParent(_slotList);
        }

        public override bool IsEmpty()
        {
            return true;
        }
    }
}