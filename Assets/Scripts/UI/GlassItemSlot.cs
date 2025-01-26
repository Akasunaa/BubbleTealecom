using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GlassItemSlot : ItemSlot
    {
        [SerializeField] private Transform _slotList;
        private Recipe _recipe = new Recipe();

        private bool _isShaken = false;
        [SerializeField] private Image  _image;
        [SerializeField] private Sprite _filledSprite;
        [SerializeField] private Sprite _emptySprite;

        public bool GetIsShaken() {  return _isShaken; }

        public override void Receive(GameObject item)
        {
            item.transform.localScale = Vector3.one;
            item.transform.SetParent(_slotList);

            var ingredient = item.GetComponent<Ingredient>();
            if (ingredient)
            {
                _recipe.finalIngredientStates.Add(ingredient.ingredientState);
            }
            else
            {
                Debug.LogError("Missing Ingredient on received object");
            }
        }

        public override bool IsEmpty()
        {
            return true;
        }

        public Recipe GetRecipe()
        {
            return _recipe;
        }

        public void Clear()
        {
            int n = _slotList.childCount;
            for (int i = n - 1; i >= 0; i--)
            {
                Destroy(_slotList.GetChild(i).gameObject);
            }
            _recipe = new Recipe();
            _image.sprite = _emptySprite;
            _isShaken=false;
        }

        public void ShakeGlass()
        {
            _isShaken = true;
            //we clear the shake :
            int n = _slotList.childCount;
            for (int i = n - 1; i >= 0; i--)
            {
                Destroy(_slotList.GetChild(i).gameObject);
            }
            //we add the liquid sprite :
            _image.sprite = _filledSprite;
        }
    }
}