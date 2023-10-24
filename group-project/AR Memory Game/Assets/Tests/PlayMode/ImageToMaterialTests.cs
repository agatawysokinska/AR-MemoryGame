using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ImageToMaterialTests
{
    [UnityTest]
    public IEnumerator PerformTransformationDirectoryNotFoundTest()
    {
        // Arrange
        var gameObject = new GameObject();
        var imageToMaterial = gameObject.AddComponent<ImageToMaterial>();
        imageToMaterial.ResourcesFolderPath = "NotExistingFolder/NotExistingResourcesFolder";
        LogAssert.ignoreFailingMessages = true;
        
        // Act
        var ret = imageToMaterial.PerformTransformation();
        
        // Assert
        Assert.IsFalse(ret);
            
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator PerformTransformationSuccessTest()
    {
        // Arrange
        var gameObject = new GameObject();
        var imageToMaterial = gameObject.AddComponent<ImageToMaterial>();
        LogAssert.ignoreFailingMessages = true;
        
        // Act
        var ret = imageToMaterial.PerformTransformation();
        
        // Assert
        Assert.IsTrue(ret);
            
        yield return null;
    }
}
