// UI script 
// mm - main menu element
// pm - pause menun element


using UnityEngine;
using TMPro;
using UnityEngine.UI;




public class GameController: MonoBehaviour
{
    // Public Variables
    public Button mmToPause;
    public Button mmStartGame;
    public Button pmToMainMenu;
    public GameObject pausePanel;
    public GameObject MainMenuPanel;

    public bool GameRunning;

    // Private Variables


    void Start()
    {
        // Initialize Game
        GameRunning = false;
        MainMenuPanel.SetActive(true);
        pausePanel.SetActive(false);


        if (mmToPause != null)
        {
            mmToPause.onClick.AddListener(() => OnButtonClick(1));
            mmStartGame.onClick.AddListener(() => OnButtonClick(3));
        }
        if (pmToMainMenu != null)
        {
            pmToMainMenu.onClick.AddListener(() => OnButtonClick(2));
        }
        
    }

    void OnButtonClick(int button)
    {
        switch(button){

            case 1:
                    MainMenuPanel.SetActive(false);
                    pausePanel.SetActive(true);
            break;
            
            case 2:
                    pausePanel.SetActive(false);
                    MainMenuPanel.SetActive(true);
            break;

            case 3: // Play Game
                MainMenuPanel.SetActive(false);
                GameRunning = true;
            break;
        }
    }
}