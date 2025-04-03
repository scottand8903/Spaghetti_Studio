using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using TMPro;

public class DemoModeManager : MonoBehaviour
{
    public VideoPlayer demoVideoPlayer;
    public GameObject demoCanvas; // Optional: if you're using a canvas or overlay for video
    public TextMeshProUGUI pressAnyButton;

    void Start()
    {
        
        GameController.Instance.SettingsPanel.SetActive(false);
        demoCanvas?.SetActive(true);
        demoVideoPlayer.Play();
        pressAnyButton.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            pressAnyButton.gameObject.SetActive(false);
			GameController.Instance.MainMenuPanel.SetActive(true);
			SceneManager.LoadScene("SampleScene");
        }
    }
}
