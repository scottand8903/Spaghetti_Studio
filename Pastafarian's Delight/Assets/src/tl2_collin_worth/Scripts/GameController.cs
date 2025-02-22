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
    public GameObject Inventory;

    public bool GameRunning;

    // Private Variables


    void Start()
    {
        // Initialize Game
        GameRunning = false;
        MainMenuPanel.SetActive(true);
        pausePanel.SetActive(false);
        Inventory.SetActive(false);


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
                    Inventory.SetActive(true);
                    GameRunning = true;
            break;

            case 4:
                    Resume();
            break;
        }
    }

    void Update(){
        
        //Debug.Log("Up")
        // Pause menu 
        if (Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("hitting escape");
                if (!GameRunning){
                    Debug.Log("going to resume");
                    Resume();
                }else{
                    Debug.Log("going to pause");
                    Pause();
                }
        }
    }

    void Resume(){
        pausePanel.SetActive(false);
        Time.timeScale = 1f; 
        GameRunning = true;
    }

    void Pause(){
        Debug.Log("Pausing the game");
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