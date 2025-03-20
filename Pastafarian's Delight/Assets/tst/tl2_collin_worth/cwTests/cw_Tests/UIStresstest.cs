using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class GameControllerTests : MonoBehaviour
{
    private GameController gameController;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject hud;

    private GameObject gameControllerObject;

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    } 

/*
   [UnityTest]
    public IEnumerator StressTest_RapidPauseResume()
    {
        for (int i = 0; i < 1; i++)
        {
            Debug.Log($"Iteration: {i}");
            gameController.Pause();
            yield return null; // Let Unity process

            Assert.IsFalse(gameController.Instance.GameRunning, "Game should be paused");
            Assert.IsTrue(gameController.Instance.pausePanel.activeSelf, "Pause panel should be active");

            gameController.Resume();
            yield return null; // Let Unity process

            Assert.IsTrue(gameController.Instance.GameRunning, "Game should be running");
            Assert.IsFalse(gameController.Instance.pausePanel.activeSelf, "Pause panel should be inactive");
        }

        yield return null; // Ensure Unity test completes
    } 
*/
    [UnityTest]
    public IEnumerator StressTest_RapidPauseResume()
    {
        int iterations = 10000;
        float waitTime = 0.05f; // Start with a reasonable delay
        float minWaitTime = 0.00000001f; // Set a minimum limit
        float decreaseAmount = 0.005f; // Decrease by this amount per iteration

        for (int i = 0; i < iterations; i++)
        {
            var gameController = GameObject.FindObjectOfType<GameController>();

            Debug.Log($"Iteration: {i}, Wait Time: {waitTime}");

            // Rapidly pause and resume the game
            gameController.Pause();
            yield return new WaitForSecondsRealtime(waitTime);

            Assert.IsFalse(gameController.GameRunning, "Game should be paused");
            Assert.IsTrue(gameController.pausePanel.activeSelf, "Pause panel should be active");

            gameController.Resume();
            Assert.IsTrue(gameController.GameRunning, "Game should be running");
            Assert.IsFalse(gameController.pausePanel.activeSelf, "Pause panel should be inactive");

            //yield return null;

            // Decrease wait time by a fixed amount, ensuring it doesn't go below minWaitTime
            waitTime = Mathf.Max(waitTime - decreaseAmount, minWaitTime);
        }

        Debug.Log("Stress test completed!");
    }
}
