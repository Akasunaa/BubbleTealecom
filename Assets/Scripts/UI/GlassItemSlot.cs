using UnityEngine;

namespace UI
{
    public class GlassItemSlot : ItemSlot
    {
        [SerializeField] private Transform _slotList;
        private Recipe _recipe = new Recipe();

        public override void Receive(GameObject item)
        {
            item.transform.localScale = Vector3.one;
            item.transform.SetParent(_slotList);

            var ingredient = item.GetComponent<Ingredient>();
            if (ingredient)
            {
                _recipe.finalIngredientStates.Add(ingredient.ingredient_state);
            }
            else
            {
                Debug.LogError("Missing Ingredient on received object");
            }
        }

        public override bool IsEmpty()
        {
            return _recipe.finalIngredientStates.Count == 0;
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
        }
    }
}