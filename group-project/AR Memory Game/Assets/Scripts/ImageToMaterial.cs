using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ImageToMaterial : MonoBehaviour
{
    public string ResourcesFolderPath { get; set; }  = "Assets/Resources";

    // Start is called before the first frame update
    void Start()
    {
        //PerformTransformation();
    }

#if UNITY_EDITOR
    public bool PerformTransformation()
    {
        string sourceImagesFolderName = Path.Combine(ResourcesFolderPath, "Images");
        string materialFolderName = Path.Combine(ResourcesFolderPath, "Materials");
        
        try
        {
            string[] subFolders = Directory.GetDirectories(sourceImagesFolderName);
            foreach (string subFolder in subFolders)
            {
                string imageFolderName = Path.GetFileName(subFolder);
                string specifiedImagesPath = Path.Combine("Images", imageFolderName);
                // Load the image as a texture
                Texture2D[] textures = Resources.LoadAll<Texture2D>(specifiedImagesPath);

                // Create a new material for each texture and save it to the specified folder
                foreach (Texture2D texture in textures)
                {
                    
                    // Create a new material and assign the texture to its main texture property
                    Material material = new Material(Shader.Find("Standard"));
                    material.SetTexture("_MainTex", texture);

                    // Save the material to an asset in the specified folder
                    string materialFolderPath = Path.Combine(materialFolderName, imageFolderName);
                    string materialPath = Path.Combine(materialFolderPath, texture.name + ".mat");

                    // Ensure that the parent directory exists before saving the material
                    Directory.CreateDirectory(materialFolderPath);
                    
                    UnityEditor.AssetDatabase.CreateAsset(material, materialPath);
                    UnityEditor.AssetDatabase.SaveAssets();
                }

            }
        }
        catch (DirectoryNotFoundException e)
        {
            Debug.LogError("Resources folder not found: " + e.Message);
            return false;
        }
        
        return true;
    }
#endif
}
