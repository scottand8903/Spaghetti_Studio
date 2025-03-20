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
    public Button mmSettings;
    public Button sBackButton;
    public GameObject pausePanel;
    public GameObject MainMenuPanel;
    public GameObject SettingsPanel;
    public GameObject HUD;
    public GameObject[] healthSprites;
    public TextMeshProUGUI dishNameTXT;
    public TextMeshProUGUI ingredient1TXT;
    public TextMeshProUGUI ingredient2TXT;
    public TextMeshProUGUI ingredient3TXT;


    public bool GameRunning;

    // Private Variables




    public static GameController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }
    

    void Start()
    {

        // Initialize Game
        Debug.Log("GameController Start called");

        if (MainMenuPanel == null) Debug.LogError("MainMenuPanel is NOT assigned!");
        if (pausePanel == null) Debug.LogError("pausePanel is NOT assigned!");
        if (HUD == null) Debug.LogError("HUD is NOT assigned!");
        if (SettingsPanel== null) Debug.LogError("SettingsPanel is NOT assigned!");

        GameRunning = false;
        Pause();
        MainMenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        pausePanel.SetActive(false);
        HUD.SetActive(false);
        


        if (mmQuit != null && mmStartGame != null && mmSettings != null){
            mmQuit.onClick.AddListener(() => OnButtonClick(1));
            mmStartGame.onClick.AddListener(() => OnButtonClick(3));
            mmSettings.onClick.AddListener(() => OnButtonClick(5));
        }
        else{
            Debug.Log("mm button doesnt exist");
        }
        if (pmToMainMenu != null && pmResume != null){
            pmToMainMenu.onClick.AddListener(() => OnButtonClick(2));
            pmResume.onClick.AddListener(() => OnButtonClick(4));
        }
        else{
            Debug.Log("pm button doesnt exist");
        }
        if(sBackButton != null){
            sBackButton.onClick.AddListener(() => OnButtonClick(6));
        }else{
            Debug.Log("s button doesnt exist");
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


        // Player Health test
        if(PlayerController.Instance != null)
        {
            Debug.Log("currentHealth: " + PlayerController.Instance.GetHealth());
            PlayerController.Instance.updateHealth(-1);
            Debug.Log("currentHealth: " + PlayerController.Instance.GetHealth());
        }
        else
        {
            Debug.LogError("(GameController)PlayerController instance is not initialized!");
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
                    Resume();
            break;

            case 4:
                    Resume();
            break;

            case 5:
                    MainMenuPanel.SetActive(false);
                    SettingsPanel.SetActive(true);
            break;

            case 6:
                    SettingsPanel.SetActive(false);
                    MainMenuPanel.SetActive(true);
            break;

            default:
                    Debug.Log("Switch error");
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
    
    

    public void updateHealthSprites(){

        for (int i = 0; i < healthSprites.Length; i++){
            healthSprites[i].SetActive(i < PlayerController.Instance.GetHealth());
        }
    }

    public void Resume(){
        pausePanel.SetActive(false);
        HUD.SetActive(true);
        Time.timeScale = 1f; 
        GameRunning = true;
    }

    public void Pause(){
        pausePanel.SetActive(true);
        HUD.SetActive(false);
        Time.timeScale = 0f;
        GameRunning = false;
    }

    public void QuitGame(){
        Application.Quit(); // For Built version of game
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // For editor version
        #endif
    }
    
    public void ResetGame(){
        // Nothing to reset yet
    }
}