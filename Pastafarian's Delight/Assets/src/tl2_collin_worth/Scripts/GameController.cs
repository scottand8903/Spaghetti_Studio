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
    public TextMeshProUGUI dishNameTXT;
    public TextMeshProUGUI ingredient1TXT;
    public TextMeshProUGUI ingredient2TXT;
    public TextMeshProUGUI ingredient3TXT;


    public bool GameRunning;

    // Private Variables
    private int MAX_HEALTH = 5;
    private int currentHealth;

    void Start()
    {

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
        if(Puzzle.Instance != null)
        {
            
            // Display dish and ingredients
            dishNameTXT.text = Puzzle.Instance.currentDish.dishName;
            ingredient1TXT.text = Puzzle.Instance.currentDish.ingredients[0].name;
            ingredient2TXT.text = Puzzle.Instance.currentDish.ingredients[1].name;
            ingredient3TXT.text = Puzzle.Instance.currentDish.ingredients[2].name;

        }
        else
        {
            Debug.LogError("(GameController)Puzzle instance is not initialized!");
        }

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