using LevelData;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Menu
{
    public class LevelButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private LevelObject _level;
        [SerializeField] private Image _stateImage;
        [SerializeField] private Sprite _currentTexture;
        [SerializeField] private Sprite _passedTexture;

        private int _myIndex = 0;
        private bool _isCurrent = false;

        private void Start()
        {
            _myIndex = transform.GetSiblingIndex();

            int dayIndexed = LevelDataHolder.CurrentDay - 1;
            if (dayIndexed < _myIndex)
            {
                _stateImage.gameObject.SetActive(false);
            }
            else if (dayIndexed == _myIndex)
            {
                _stateImage.sprite = _currentTexture;
                _isCurrent = true;
            }
            else
            {
                _stateImage.sprite = _passedTexture;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isCurrent && _level != null)
            {
                // TODO display message
                LevelDataHolder.CurrentLevel = _level;
                SceneManager.LoadScene("ShopScene");
            }
        }
    }
}

