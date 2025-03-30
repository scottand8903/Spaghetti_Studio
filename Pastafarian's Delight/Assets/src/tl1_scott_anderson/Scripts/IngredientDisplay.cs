using UnityEngine;
using UnityEngine.UI;

public class IngredientDisplay : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    private Ingredient ingredientData;

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

        gameObject.name = ingredient.name;
    }

    public Ingredient GetIngredient()
    {
        return ingredientData;
    }
    
}
