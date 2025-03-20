using System.Collections;
using System.IO;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class JSON_StressTest
{
   
    private string testFilePath = Application.persistentDataPath + "/test.json";

    
    private Ingredient LoadJson(string jsonContent)
    {
        File.WriteAllText(testFilePath, jsonContent);
        string loadedJson = File.ReadAllText(testFilePath);
        return JsonUtility.FromJson<Ingredient>(loadedJson);
    }

    
    [UnityTest, Timeout(300000)] // 5 minute timeout
    public IEnumerator JSON_StressTestWithEnumeratorPasses()
    {
        int testAmount = 100000;
        string largeJson = "{ \"ingredients\": [";
        for (int i = 0; i < testAmount; i++) // Generate 100,000 ingredient objects
        {
            largeJson += $"{{\"id\":{i},\"name\":\"Ingredient{i}\",\"riddles\":[\"Riddle1\",\"Riddle2\"]}},";
        }
        largeJson = largeJson.TrimEnd(',') + "]}"; // Close JSON properly

        File.WriteAllText(testFilePath, largeJson);
        string loadedJson = File.ReadAllText(testFilePath);

        // JsonUtility does not support arrays, using wrapper class.
        IngredientList wrapper = JsonUtility.FromJson<IngredientList>(loadedJson);

        Assert.NotNull(wrapper, "Parsed object should not be null.");
        TestContext.WriteLine("Assert.NotNull passed: Wrapper is not null.");

        Assert.NotNull(wrapper.ingredients, "Ingredients list should not be null.");
        TestContext.WriteLine("Assert.NotNull passed: Ingredients list is not null.");

        Assert.AreEqual(testAmount, wrapper.ingredients.Length, "Should have parsed " + testAmount + " ingredients.");
        TestContext.WriteLine($"Assert.AreEqual passed: Ingredients array length is {testAmount}.");

        Debug.Log($"{testAmount} parsed ingredients.");
        Debug.Log($"{wrapper.ingredients.Length} total ingredients.");

        yield return null;
    }


    // Boundary Test 1: Empty JSON
    [Test]
    public void BoundaryTest_EmptyJsonFile()
    {
        string emptyJson = "{}";
        var result = LoadJson(emptyJson);
        
        Assert.IsNotNull(result, "Parser should return an object, even if empty.");
        TestContext.WriteLine("Assert.IsNotNull passed.");

        Assert.AreEqual(0, result.id, "ID should default to 0.");
        TestContext.WriteLine("Assert.AreEqual passed: ID is 0.");

        Assert.IsNull(result.name, "Name should be null for empty JSON.");
        TestContext.WriteLine("Assert.IsNull passed for name.");

        Assert.IsNull(result.riddles, "Riddles array should be null for empty JSON.");
        TestContext.WriteLine("Assert.IsNull passed for riddles.");
    }

    // Boundary Test 2: Minimum Valid JSON
    [Test]
    public void BoundaryTest_MinimumValidJson()
    {
        string minValidJson = "{ \"id\": 1, \"name\": \"Salt\", \"riddles\": [\"White but not snow\"] }";
        var result = LoadJson(minValidJson);
        
        Assert.IsNotNull(result, "Parser should return a valid object.");
        TestContext.WriteLine("Assert.IsNotNull passed: result is not null.");

        Assert.AreEqual(1, result.id, "ID should match expected value.");
        TestContext.WriteLine("Assert.AreEqual passed: ID is 1.");

        Assert.AreEqual("Salt", result.name, "Name should match expected value.");
        TestContext.WriteLine("Assert.AreEqual passed: Name is 'Salt'.");

        Assert.IsNotNull(result.riddles, "Riddles array should not be null.");
        TestContext.WriteLine("Assert.IsNotNull passed: Riddles array is not null.");

        Assert.AreEqual(1, result.riddles.Length, "Riddles array should contain one item.");
        TestContext.WriteLine("Assert.AreEqual passed: Riddles array length is 1.");

        Assert.AreEqual("White but not snow", result.riddles[0], "Riddle content should match.");
        TestContext.WriteLine("Assert.AreEqual passed: Riddle content is 'White but not snow'.");
    }
}
