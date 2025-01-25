using UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace World
{
    public class WorldButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Window _uiWindow;

        public void OnPointerClick(PointerEventData eventData)
        {
            _uiWindow.gameObject.SetActive(true);
            _uiWindow.Focus();
        }
    }
}
