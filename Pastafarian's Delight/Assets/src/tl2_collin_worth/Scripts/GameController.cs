// UI script 
// mm - main menu element
// pm - pause menun element
// s - settings element
// d - death screen element

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif



public class GameController: MonoBehaviour
{
    // Public Variables
    public GameObject ingredientAnswers;
    public Button mmStartGame;
    public Button pmToMainMenu;
    public Button pmResume;
    public Button mmQuit;
    public Button mmSettings;
    public Button sBackButton;
    public Button sDemoModeButton;
    public Button dQuitButton;
    public Toggle BCModeToggle;
    public GameObject pausePanel;
    public GameObject MainMenuPanel;
    public GameObject SettingsPanel;
    public GameObject DeathScreen;
    public GameObject HUD;
    public GameObject[] healthSprites;
    public TextMeshProUGUI dishNameTXT;
    public TextMeshProUGUI ingredient1TXT;
    public TextMeshProUGUI ingredient2TXT;
    public TextMeshProUGUI ingredient3TXT;

    public string lastDoorUsed;
    public bool GameRunning;
    public bool BCMode;

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
        DeathScreen.SetActive(false);
        ingredientAnswers.SetActive(false);
        

        // Initialize buttons
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
        if(dQuitButton != null){
            dQuitButton.onClick.AddListener(() => OnButtonClick(1));
        }else{
            Debug.Log("d button doesnt exist");
        }
        
        // Initialize health sprites
        BCModeToggle.onValueChanged.AddListener(BCModeChanged);
        dQuitButton.onClick.AddListener(QuitGame);

        //Load Demo mode button
        if(IsSceneValid("DemoMode"))
        {
            sDemoModeButton.onClick.AddListener(() => SceneManager.LoadScene("DemoMode"));
        }
        else
        {
            Debug.Log("Scene DemoMode does not exist");
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

    // Called when a button is clicked displays correct menu
    // 1 = Quit Game
    // 2 = Main Menu
    // 3 = Start Game
    // 4 = Resume
    // 5 = Settings
    void OnButtonClick(int button)
    {
        switch(button){

            case 1: 
                    QuitGame();
            break;
            
            case 2: // MainMenu
                    pausePanel.SetActive(false);
                    MainMenuPanel.SetActive(true);
                    InventorySystem.Instance.wipeInventory();
                    SceneManager.LoadScene("SampleScene");
                    ResetGame();
            break;

            case 3: // Start Game
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

    // Check if the scene is valid
    private bool IsSceneValid(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameFromPath = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneNameFromPath == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    // Toggles BCMode on/off from the toggle switch
    void BCModeChanged(bool isOn)
    {
        if (isOn)
        {
            BCMode = true;
            Debug.Log("setting BCMODE true");
            ingredientAnswers.SetActive(true);
        } 
        else
        {
            BCMode = false;
            Debug.Log("setting BCMODE false");
            ingredientAnswers.SetActive(false);
        }
    }

    // Updates the health sprites based on the current health
    public void updateHealthSprites(){

        for (int i = 0; i < healthSprites.Length; i++){
            healthSprites[i].SetActive(i < HealthSystem.Instance.GetHealth());
        }
    }
    

    public void endGame(){
        HUD.SetActive(false);
        DeathScreen.SetActive(true);
        Time.timeScale = 0f;
        GameRunning = false;

    }
    
    // Resumes the game
    public void Resume(){
        pausePanel.SetActive(false);
        HUD.SetActive(true);
        Time.timeScale = 1f; 
        GameRunning = true;
    }

    // Pauses the game
    public void Pause(){
        pausePanel.SetActive(true);
        HUD.SetActive(false);
        Time.timeScale = 0f;
        GameRunning = false;
    }

    // Quits the game
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