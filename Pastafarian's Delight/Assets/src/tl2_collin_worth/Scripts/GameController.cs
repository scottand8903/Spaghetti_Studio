// UI script 
// mm - main menu element
// pm - pause menun element


using UnityEngine;
using TMPro;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif



public class GameController: MonoBehaviour
{
    // Public Variables
    public Button mmStartGame;
    public Button pmToMainMenu;
    public Button pmResume;
    public Button mmQuit;
    public GameObject pausePanel;
    public GameObject MainMenuPanel;
    public GameObject HUD;
    public GameObject[] healthSprites;

    public bool GameRunning;

    // Private Variables
    private int MAX_HEALTH = 5;
    private int currentHealth;
    
    // Pasta dishes
    //private PastaDish currentDish;

    // Other scripts
    // private Puzzle puzzle;

    void Start()
    {

        //PastaDish currentDish = new PastaDish;

        // Initialize Game
        GameRunning = false;
        MainMenuPanel.SetActive(true);
        pausePanel.SetActive(false);
        HUD.SetActive(false);
        
        currentHealth = MAX_HEALTH;


        if (mmQuit != null && mmStartGame != null){
            mmQuit.onClick.AddListener(() => OnButtonClick(1));
            mmStartGame.onClick.AddListener(() => OnButtonClick(3));
        }
        else{
            Debug.Log("mmQuit button doesnt exist");
        }
        if (pmToMainMenu != null && pmResume != null){
            pmToMainMenu.onClick.AddListener(() => OnButtonClick(2));
            pmResume.onClick.AddListener(() => OnButtonClick(4));
        }
        else{
            Debug.Log("pmToMainMenu button doesnt exist");
        }

        // PUZZLE MANAGER STUFF
        // PastaDish[] dishes = new PastaDish[1]; // Example with one dish
        // puzzle = new Puzzle(dishes);

        // Get PastaDish from the puzzle instance
        // PastaDish currentDish = Puzzle.Instance?.GetPastaDish(); // scott changed
        
        // puzzle = puzzle.GetPuzzle();

        if(Puzzle.Instance != null)
        {
            Debug.Log("PRINTING FROM GAMECONTROLLER!!!:");
            Puzzle.Instance.PrintCurrentDish();
            Debug.Log($"Ingredient 2 : {Puzzle.Instance.currentDish.ingredients[1].name}");
        }
        else
        {
            Debug.LogError("(GameController)Puzzle instance is not initialized!");
        }


        // if (puzzle.currentDish != null)
        // {
        //     Debug.Log($"puzzle.currentDish is null");
        //     // Debug.Log($"Printing from GameController: {puzzle.currentDish.dishName}");
        //     // foreach (Ingredient ingredient in puzzle.currentDish.ingredients)
        //     // {
        //     //     Debug.Log($"Ingredient: {puzzle.currentDish.ingredient.name}");
        //     //     for (int i = 0; i < puzzle.currentDish.ingredient.riddles.Length; i++)
        //     //     {
        //     //         Debug.Log($"Riddle {i + 1}: {puzzle.currentDish.ingredient.riddles[i]}");
        //     //     }
        //     // }
        // }
        // else
        // {
        //     Debug.LogError("No pasta dish selected in Puzzle!");
        // }

        // Debug.Log("PRINTING FROM GAMECONTROLLER!!!:");
        // Puzzle.Instance.PrintCurrentDish();
 
    
    }

    void OnButtonClick(int button)
    {
        switch(button){

            case 1: 
                    QuitGame();
            break;
            
            case 2: // MainMenu
                    pausePanel.SetActive(false);
                    MainMenuPanel.SetActive(true);
                    ResetGame();
            break;

            case 3: // Play Game
                    MainMenuPanel.SetActive(false);
                    HUD.SetActive(true);
                    GameRunning = true;
            break;

            case 4:
                    Resume();
            break;
        }
    }

    void Update(){
        
        // Pause menu 
        if (Input.GetKeyDown(KeyCode.Escape)){
                if (!GameRunning){
                    Resume();
                }else{
                    Pause();
                }
        }
    }
    
    public int GetHealth(){
        return currentHealth;
    }

    public void updateHealth(int healthChange){
        if((currentHealth + healthChange) <= MAX_HEALTH){
            currentHealth += healthChange;
            
        }else{
            currentHealth = MAX_HEALTH;
        }

        updateHealthSprites();

        if(currentHealth < 1){
            QuitGame(); // change to endGame later
        }
    }

    void updateHealthSprites(){

        for (int i = 0; i < healthSprites.Length; i++){
            healthSprites[i].SetActive(i < currentHealth);
        }
    }

    void Resume(){
        pausePanel.SetActive(false);
        Time.timeScale = 1f; 
        GameRunning = true;
    }

    void Pause(){
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        GameRunning = false;
    }

    public void QuitGame(){
        Application.Quit(); // For Built version of game
        EditorApplication.isPlaying = false; // For editor version of game
    }
    
    public void ResetGame(){
        // Nothing to reset yet
    }
}