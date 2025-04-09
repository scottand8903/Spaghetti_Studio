using TMPro;
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

    public TextMeshProUGUI textMeshPro;

    // Stores the ingredient data associated with this display.
    private Ingredient ingredientData;

    /// <summary>
    /// Sets the ingredient data and updates the display accordingly.
    /// </summary>
    /// <param name="ingredient">The ingredient data to display.</param>
    /// <param name="ingredientObj">The ingredient object, needed to assign the sprite variable.</param>
    public void SetIngredient(Ingredient ingredient, GameObject ingredientObj)
    {
        // Store the ingredient data.
        ingredientData = ingredient;

        // Get the SpriteRenderer component attached to this GameObject.
        spriteRenderer = ingredientObj.GetComponent<SpriteRenderer>();

        // textMeshPro = GetComponentInChildren<TextMeshPro>();
        Transform labelTransform = ingredientObj.transform.Find("IngredientLabel/Canvas/IngredientName");
        if (labelTransform != null)
        {
            textMeshPro = labelTransform.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("IngredientLabel not found in children.");
        }

        // If the ingredient data is valid and has a sprite, load and set the sprite.
        if (ingredientData != null && !string.IsNullOrEmpty(ingredientData.sprite))
        {
            // Load the sprite from the Resources folder.
            Sprite loadedSprite = Resources.Load<Sprite>("Ingredient/" + ingredientData.sprite);

            // If the sprite is successfully loaded, assign it to the SpriteRenderer.
            if (loadedSprite != null)
            {
                Debug.Log($"Loaded sprite for ingredient '{ingredientData.name}': {loadedSprite.name}");
                spriteRenderer.sprite = loadedSprite;
            }

            // Set the ingredient sprite on the pickup component
            Item pickup = ingredientObj.GetComponent<Item>();
            if (pickup != null)
            {
                pickup.itemSprite = loadedSprite;
            }
        }

        // Update the GameObject's name to match the ingredient's name.
        ingredientObj.name = ingredient.name;

        if(textMeshPro != null)
        {
            textMeshPro.text = ingredient.name;
            textMeshPro.fontSize = 36;
            textMeshPro.color = Color.white;

            RectTransform textTransform = textMeshPro.GetComponent<RectTransform>();
            // RectTransform textTransform = textMeshPro.GetComponent<RectTransform>();
            if (textTransform != null)
            {
                textTransform.localPosition = new Vector3(0, -spriteRenderer.bounds.size.y / 2 - 0.2f, 0);
            }
        }
        else
        {
            Debug.LogError("TextMeshPro component not found in children.");
        }
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
