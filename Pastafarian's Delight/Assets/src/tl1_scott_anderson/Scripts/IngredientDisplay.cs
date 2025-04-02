using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the display of an ingredient in the game.
/// This includes setting the sprite and updating the GameObject's name based on the ingredient data.
/// </summary>
public class IngredientDisplay : MonoBehaviour
{
    // The SpriteRenderer component used to display the ingredient's sprite.
    public SpriteRenderer spriteRenderer;

    // Stores the ingredient data associated with this display.
    private Ingredient ingredientData;

    /// <summary>
    /// Sets the ingredient data and updates the display accordingly.
    /// </summary>
    /// <param name="ingredient">The ingredient data to display.</param>
    public void SetIngredient(Ingredient ingredient)
    {
        // Store the ingredient data.
        ingredientData = ingredient;

        // Get the SpriteRenderer component attached to this GameObject.
        spriteRenderer = GetComponent<SpriteRenderer>();

        // If the ingredient data is valid and has a sprite, load and set the sprite.
        if (ingredientData != null && !string.IsNullOrEmpty(ingredientData.sprite))
        {
            // Load the sprite from the Resources folder.
            Sprite loadedSprite = Resources.Load<Sprite>("Ingredient/" + ingredientData.sprite);

            // If the sprite is successfully loaded, assign it to the SpriteRenderer.
            if (loadedSprite != null)
            {
                spriteRenderer.sprite = loadedSprite;
            }
        }

        // Update the GameObject's name to match the ingredient's name.
        gameObject.name = ingredient.name;
    }

    /// <summary>
    /// Gets the ingredient data associated with this display.
    /// </summary>
    /// <returns>The ingredient data.</returns>
    public Ingredient GetIngredient()
    {
        return ingredientData;
    }
}
