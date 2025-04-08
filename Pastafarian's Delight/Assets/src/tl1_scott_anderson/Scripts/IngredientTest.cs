using UnityEngine;

public class IngredientTest : MonoBehaviour
{
    void Start()
    {
        // Create instances of SpicyIngredient and SweetIngredient
        Ingredient spicy = new SpicyIngredient { name = "Chili Pepper" };
        Ingredient sweet = new SweetIngredient { name = "Sugar Cube" };
        Ingredient generic = new Ingredient { name = "Generic Ingredient" };

        // Call OnPickup using base class reference
        spicy.OnPickup(); // Should trigger SpicyIngredient's OnPickup
        sweet.OnPickup(); // Should trigger SweetIngredient's OnPickup
        generic.OnPickup(); // Should trigger Ingredient's OnPickup
    }
}