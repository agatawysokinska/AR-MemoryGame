using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LayCardsTests
{
    [UnityTest]
    public IEnumerator ThemePickerDefaultTest()
    {
        // Arrange
        var gameObject = new GameObject();
        var layCards = gameObject.AddComponent<LayCards>();
        LogAssert.ignoreFailingMessages = true;
        string expectedValue = "city";
        PlayerPrefs.DeleteKey("UserThemeChoice");

        // Act
        var ret = layCards.ThemePicker();
        
        // Assert
        Assert.AreEqual(expectedValue, ret);
        
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator ThemePickerAllThemesTest()
    {
        // Arrange
        var gameObject = new GameObject();
        var layCards = gameObject.AddComponent<LayCards>();
        LogAssert.ignoreFailingMessages = true;
        string[] expectedValues = {"city", "countryside", "Desert", "Fantasy", "House pets", 
            "Rainforest", "Sea", "Space", "Wild", "Woods"};

        for (int i = 0; i < expectedValues.Length; i++)
        {
            PlayerPrefs.SetInt("UserThemeChoice", i);
            
            // Act
            var ret = layCards.ThemePicker();

            // Assert
            Assert.AreEqual(expectedValues[i], ret);
        }

        yield return null;
    }
}
