using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RiddleDisplay : MonoBehaviour
{
    public TMP_Text riddleTextBox;

    public static RiddleDisplay Instance;

    private static string eastRiddle;
    private static string westRiddle;
    private static string southRiddle;

    private void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject); // <-- Magic line
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    else
    {
        Destroy(gameObject); // <-- Prevent duplicates
    }
}

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // <-- Clean up the event listener
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateDisplayedRiddle(scene.name);
    }

    private void Start()
    {
        UpdateDisplayedRiddle(SceneManager.GetActiveScene().name); // <-- Also update immediately when first spawned
    }

    private void UpdateDisplayedRiddle(string sceneName)
    {
        if (riddleTextBox == null)
            return;

        if (sceneName == "EastRoom")
        {
            riddleTextBox.text = eastRiddle;
        }
        else if (sceneName == "WestRoom")
        {
            riddleTextBox.text = westRiddle;
        }
        else if (sceneName == "SouthRoom")
        {
            riddleTextBox.text = southRiddle;
        }
        else
        {
            riddleTextBox.text = ""; // Clear text if unknown scene
        }
    }

    public static void DisplayRiddle(string ingredientName)
    {
        PastaDish currentDish = Puzzle.Instance.currentDish;

        foreach (Ingredient ingredient in currentDish.ingredients)
        {
            if (ingredient.name == ingredientName)
            {
                int riddleCount = ingredient.riddles.Length;
                int rIndex = Random.Range(0, riddleCount);
                string riddle = ingredient.riddles[rIndex];

                string sceneName = SceneManager.GetActiveScene().name; // <-- Get scene name here correctly!

                if (sceneName == "EastRoom")
                {
                    eastRiddle = riddle;
                }
                else if (sceneName == "WestRoom")
                {
                    westRiddle = riddle;
                }
                else if (sceneName == "SouthRoom")
                {
                    southRiddle = riddle;
                }

                // Update text immediately if instance exists and player is in this scene
                if (Instance != null)
                {
                    Instance.UpdateDisplayedRiddle(sceneName);
                }

                break;
            }
        }
    }
}