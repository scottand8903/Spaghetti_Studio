using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameControllerTests
{
    private GameController gameController;
    private GameObject mainMenuPanel;
    private GameObject pausePanel;
    private GameObject hud;

    private GameObject gameControllerObject;

    [SetUp]
    public void Setup()
    {
        // Create GameObjects for UI elements
        mainMenuPanel = new GameObject("MainMenuPanel");
        pausePanel = new GameObject("PausePanel");
        hud = new GameObject("HUD");

        // Add CanvasRenderer (or any necessary components)
        mainMenuPanel.AddComponent<CanvasRenderer>();
        pausePanel.AddComponent<CanvasRenderer>();
        hud.AddComponent<CanvasRenderer>();

        // Create GameController object and add the component
        gameControllerObject = new GameObject();
        gameController = gameControllerObject.AddComponent<GameController>();

        // Assign UI GameObjects to the GameController
        gameController.MainMenuPanel = mainMenuPanel;
        gameController.pausePanel = pausePanel;
        gameController.HUD = hud;
    }

    [UnityTest]
    public IEnumerator StressTest_RapidPauseResume()
    {
        for (int i = 0; i < 100; i++)
        {
            gameController.Pause();
            Assert.IsFalse(gameController.GameRunning, "Game should be paused");

            gameController.Resume();
            Assert.IsTrue(gameController.GameRunning, "Game should be running");
        }

        yield return null;
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up after test
        GameObject.Destroy(gameControllerObject);
        GameObject.Destroy(mainMenuPanel);
        GameObject.Destroy(pausePanel);
        GameObject.Destroy(hud);
    }
}