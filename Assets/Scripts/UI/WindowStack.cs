using UnityEngine;

namespace UI
{
    public class WindowStack : MonoBehaviour
    {
        public void MoveWindowToFront(GameObject child)
        {
            child.transform.SetAsLastSibling();
        }
    }
}
