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

        public Recipe GetRecipe()
        {
            // todo
            return new Recipe();
        }

        public void Clear()
        {
            // todo
        }
    }
}