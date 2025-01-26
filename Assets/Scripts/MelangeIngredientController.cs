using UnityEngine;
using UnityEngine.UI;

public class MelangeIngredientController : MonoBehaviour
{
    [SerializeField] private Image _originDots1;
    [SerializeField] private Image _originDots2;

    public void CreateMelange(IngredientState ingredient1, IngredientState ingredient2)
    {
        if (ingredient1.color!=null)
        {
            _originDots1.color = ingredient1.color;
        }
        if (ingredient2.color != null)
        {
            _originDots2.color = ingredient2.color;
        }
        GetComponent<Ingredient>().ingredientState.oldIngredientState.Clear();
        GetComponent<Ingredient>().ingredientState.oldIngredientState.Add(ingredient1);
        GetComponent<Ingredient>().ingredientState.oldIngredientState.Add(ingredient2);
    }
}
