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
        demoCanvas?.SetActive(true);
        demoVideoPlayer.Play();
        pressAnyButton.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            pressAnyButton.gameObject.SetActive(false);

            SceneManager.LoadScene("SampleScene");
        }
    }
}
