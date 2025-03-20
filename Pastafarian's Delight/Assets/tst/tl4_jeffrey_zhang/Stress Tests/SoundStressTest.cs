/*using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class SoundStressTest
{
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("TestScene");
    }

    [UnityTest]
    public IEnumerator Test()
    {
        var soundScript = GameObject.FindFirstObjectByType<PlayAudio>();
        Assert.NotNull(soundScript, "NewMonoBehaviourScript not found in scene");

        float increment = 0.01f; // Initial speed increment
        float maxLimit = 10000f; // Prevents infinite loop

        while (soundScript.Sounditerationspeed < maxLimit)
        {
            soundScript.Sounditerationspeed += increment;
            float fps = 1.0f / Time.deltaTime;
            Debug.Log($"Sounditerationspeed: {soundScript.Sounditerationspeed}, FPS: {fps}");

            soundScript.PlayButton(); // Call the renamed function

            yield return new WaitForSeconds(0.01f); // Prevents immediate freeze
        }

        Assert.Fail("Test failed to crash Unity. Adjust parameters if needed.");
    }
}
*/