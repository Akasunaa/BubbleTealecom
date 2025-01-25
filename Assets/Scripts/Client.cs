using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    private Recipe recipe;
    public ClientRecipeElements recipeDisplayElements;
    public float timerMax;
    private float _timer;
    private bool _timerEnded = false;
    public Image timerFillImage;
    public GameObject bubbleRecipe;
    public GameObject bubbleRecipeAnswer;
    public GameObject clientRecipeElementPrefab;
    public IngredientStateImageList ingredientStateImageList;
    public TransformationImageList transformationImageList;
    public Transform bubbleFirstElementTransform;
    public Transform bubbleAnswerElementTransform;
    public Sprite delimiterSprite;
    public Gradient timerGradient;

    private void Start()
    {
        _timer = timerMax;
        recipe = recipeDisplayElements.ToRecipe();
        DisplayRecipe(recipeDisplayElements);
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _timer = 0;
            if (!_timerEnded)
            {
                _timerEnded = true;
                EndTimer();
            }
        }
        timerFillImage.color =  timerGradient.Evaluate(1.0f - _timer / timerMax);
        timerFillImage.fillAmount = _timer / timerMax;
    }

    public void GiveGlass(Recipe otherRecipe)
    {
        _timerEnded = true;
        bool isGoodRecipe = Recipe.CompareIngredientList(recipe.finalIngredientStates, otherRecipe.finalIngredientStates);
        if (isGoodRecipe)
        {
            ClientManager.Instance.ClientHappy();
        }
        else
        {
            bubbleRecipe.SetActive(false);
            var firstWrongIngredient = recipe.finalIngredientStates.Find(ingredientState => !otherRecipe.finalIngredientStates.Contains(ingredientState));
            if (!firstWrongIngredient)
            {
                Debug.LogError("There should be a wrong ingredient somewhere");
            }
            GameObject clientElement = Instantiate(clientRecipeElementPrefab, bubbleRecipeAnswer.transform);
            clientElement.GetComponent<Image>().sprite = GetSpriteFrom(firstWrongIngredient);
            clientElement.GetComponent<Image>().transform.position = bubbleAnswerElementTransform.position;
            bubbleRecipeAnswer.SetActive(true);
            Invoke(nameof(UnHappyClient), 3.0f);
        }
    }

    private void UnHappyClient()
    {
        ClientManager.Instance.ClientUnHappy();
    }

    private void EndTimer()
    {
        ClientManager.Instance.ClientTimerEnded();
    }

    private void DisplayRecipe(ClientRecipeElements clientsRecipeElements)
    {
        float translateX = 0.0f;
        for (var i = 0; i < clientsRecipeElements.recipeElements.Count; i++)
        {
            ClientRecipeElement clientRecipeElement = clientsRecipeElements.recipeElements[i];
            
            foreach (var ingredientState in clientRecipeElement.ingredientStates)
            {
                GameObject clientElement = Instantiate(clientRecipeElementPrefab, bubbleRecipe.transform);
                clientElement.GetComponent<Image>().sprite = GetSpriteFrom(ingredientState);
                clientElement.GetComponent<Image>().transform.position = bubbleFirstElementTransform.position;
                clientElement.GetComponent<Image>().rectTransform.Translate(translateX, 0.0f, 0.0f);
                translateX -= clientElement.GetComponent<Image>().transform.lossyScale.x * clientElement.GetComponent<Image>().rectTransform.sizeDelta.x + 0.02f;
            }

            if (clientRecipeElement.transform != Transformation.None)
            {
                GameObject clientElementTransform = Instantiate(clientRecipeElementPrefab, bubbleRecipe.transform);
                clientElementTransform.GetComponent<Image>().sprite = GetSpriteFrom(clientRecipeElement.transform);
                clientElementTransform.GetComponent<Image>().transform.position = bubbleFirstElementTransform.position;
                clientElementTransform.GetComponent<Image>().rectTransform.Translate(translateX, 0.0f, 0.0f);
                translateX -= clientElementTransform.GetComponent<Image>().transform.lossyScale.x * clientElementTransform.GetComponent<Image>().rectTransform.sizeDelta.x + 0.02f;
            }

            if (i < clientsRecipeElements.recipeElements.Count - 1)
            {
                GameObject clientElementDelimiter = Instantiate(clientRecipeElementPrefab, bubbleRecipe.transform);
                clientElementDelimiter.GetComponent<Image>().sprite = delimiterSprite;
                clientElementDelimiter.GetComponent<Image>().transform.position = bubbleFirstElementTransform.position;
                clientElementDelimiter.GetComponent<Image>().rectTransform.Translate(translateX, 0.0f, 0.0f);
                translateX -= clientElementDelimiter.GetComponent<Image>().transform.lossyScale.x * clientElementDelimiter.GetComponent<Image>().rectTransform.sizeDelta.x + 0.02f;
            }
        }
    }

    private Sprite GetSpriteFrom(IngredientState ingredientState)
    {
        foreach (var ingredientStateImage in ingredientStateImageList.ingredientStatesImages)
        {
            if (ingredientStateImage.ingredientState == ingredientState)
            {
                return ingredientStateImage.image;
            }
        }

        Debug.LogError("IngredientState image missing");
        return null;
    }
    
    private Sprite GetSpriteFrom(Transformation transformation)
    {
        foreach (var transformationImage in transformationImageList.transformationImages)
        {
            if (transformationImage.transformation == transformation)
            {
                return transformationImage.image;
            }
        }

        Debug.LogError("Transformation image missing");
        return null;
    }
}
