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
    public void SetIngredient(Ingredient ingredient, GameObject ingredientObj, Vector3 spawnPointPosition)
    {
        ingredientData = ingredient;

        spriteRenderer = ingredientObj.GetComponent<SpriteRenderer>();

        Transform labelTransform = ingredientObj.transform.Find("IngredientLabel/Canvas/IngredientName");
        if (labelTransform == null)
        {
            // Create the label hierarchy dynamically
            GameObject label = new GameObject("IngredientLabel");
            label.transform.SetParent(ingredientObj.transform);

            GameObject canvas = new GameObject("Canvas");
            canvas.transform.SetParent(label.transform);
            Canvas canvasComponent = canvas.AddComponent<Canvas>();
            canvasComponent.renderMode = RenderMode.WorldSpace;

            // Scale and position the canvas
            canvasComponent.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            canvasComponent.transform.localPosition = spawnPointPosition + new Vector3(0, -1f, -0.1f);
            // new Vector3(0, spriteRenderer.bounds.size.y / 2 + 0.2f, -0.1f);

            // Match the sorting layer of the ingredient
            canvasComponent.sortingLayerName = spriteRenderer.sortingLayerName;
            canvasComponent.sortingOrder = spriteRenderer.sortingOrder + 1;

            GameObject textObject = new GameObject("IngredientName");
            textObject.transform.SetParent(canvas.transform);
            textMeshPro = textObject.AddComponent<TextMeshProUGUI>();

            
            textMeshPro.fontSize = 0.3f;
            textMeshPro.color = Color.white;
            textMeshPro.alignment = TextAlignmentOptions.Center;
        }
        else
        {
            textMeshPro = labelTransform.GetComponent<TextMeshProUGUI>();
        }

        if (ingredientData != null && !string.IsNullOrEmpty(ingredientData.sprite))
        {
            Sprite loadedSprite = Resources.Load<Sprite>("Ingredient/" + ingredientData.sprite);
            if (loadedSprite != null)
            {
                spriteRenderer.sprite = loadedSprite;
            }

            Item pickup = ingredientObj.GetComponent<Item>();
            if (pickup != null)
            {
                pickup.itemSprite = loadedSprite;
            }
        }

        ingredientObj.name = ingredient.name;

        if (textMeshPro != null)
        {
            textMeshPro.text = ingredient.name;
            RectTransform textTransform = textMeshPro.GetComponent<RectTransform>();
            if (textTransform != null)
            {
                textTransform.localPosition = Vector3.zero;
            }
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
