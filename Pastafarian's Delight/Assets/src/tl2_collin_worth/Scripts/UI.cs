// UI script 
// mm - main menu element
// pm - pause menun element


using UnityEngine;
using TMPro;
using UnityEngine.UI;




public class UI : MonoBehaviour
{
    public Button mmToPause;
    public Button pmToMainMenu;
    public GameObject pausePanel;
    public GameObject MainMenuPanel;

    void Start()
    {
        if (mmToPause != null)
        {
            mmToPause.onClick.AddListener(() => OnButtonClick(1));
            //myButton.onClick.AddListener(() => OnButtonClick("Button Clicked!"));        
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
                if (pausePanel != null && MainMenuPanel != null){
                    MainMenuPanel.SetActive(false);
                    pausePanel.SetActive(true);
                }
            break;
            
            case 2:
                if (pausePanel != null && MainMenuPanel != null){
                    pausePanel.SetActive(false);
                    MainMenuPanel.SetActive(true);
                }
            break;
        }
    }
}