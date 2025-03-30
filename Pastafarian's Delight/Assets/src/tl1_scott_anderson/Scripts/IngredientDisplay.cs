using UnityEngine;
using UnityEngine.UI;

public class IngredientDisplay : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    private Ingredient ingredientData;
    // public Sprite ingredientSprite; // just for testing

    // void Start()
    // {
    //     spriteRenderer = GetComponent<SpriteRenderer>();

    //     if(ingredientData != null && !string.IsNullOrEmpty(ingredientData.sprite))
    //     {
    //         Sprite loadedSprite = Resources.Load<Sprite>("Ingredient/" + ingredientData.sprite);

    //         if(loadedSprite != null)
    //         {
    //             spriteRenderer.sprite = loadedSprite;
    //         }
    //     }

    //     // if(spriteRenderer != null && ingredientSprite != null)
    //     // {
    //     //     spriteRenderer.sprite = ingredientSprite;
    //     // }
    // }

    public void SetIngredient(Ingredient ingredient)
    {

        ingredientData = ingredient;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(ingredientData != null && !string.IsNullOrEmpty(ingredientData.sprite))
        {
            Sprite loadedSprite = Resources.Load<Sprite>("Ingredient/" + ingredientData.sprite);

            if(loadedSprite != null)
            {
                spriteRenderer.sprite = loadedSprite;
            }
        }

        // comment out for testing
        // spriteRenderer.sprite = ingredient.sprite;

        // spriteRenderer.sprite = ingredientSprite;
        gameObject.name = ingredient.name;
    }

    public Ingredient GetIngredient()
    {
        return ingredientData;
    }
    
}
