using UnityEngine;

namespace UI
{
    public class WindowStack : MonoBehaviour
    {
        [SerializeField] private Transform _itemHolder;

        public Transform GetItemHolder()
        {
            print("1"+_itemHolder);
            return _itemHolder;
        }

        public void MoveWindowToFront(GameObject child)
        {
            child.transform.SetAsLastSibling();
        }
    }
}
