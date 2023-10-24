using System.Collections;
using System.IO;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class LayCards : NetworkBehaviour
{
    [SerializeField] GameObject cardParent;
    [SerializeField] GameObject cardChild;
    //[SerializeField] GameObject boardPrefab ;
    //[SerializeField][Range(1,5)] NetworkVariable<int> rows = new NetworkVariable<int>(2);
    int rows = 2;
    int columns = 5;
    //[SerializeField][Range(2, 7)] NetworkVariable<int> columns = new NetworkVariable<int>(5);
    float cardLength = 7;
    Material cardImageMaterial;
    Material[] selectedMatsDuplicate;

    private bool isServerInitialized = false; // Flag to track server initialization
    private void Start()
    {
        //StartCoroutine(LayOutCards());
        
        if (!isServerInitialized && NetworkManager.Singleton.IsServer )
        {
            isServerInitialized = true;
            StartCoroutine(LayOutCards());
        }

    }
    private void ShuffleArray(Material[] array)
    {
        // Iterate through the array from the last element to the first
        for (int i = array.Length - 1; i > 0; i--)
        {
            // Generate a random index within the range of the remaining unshuffled elements
            int randomIndex = Random.Range(0, i + 1);

            // Swap the current element with the randomly selected element
            Material temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    public string ThemePicker()
    {
        string themeFolderName;
        int userChoice = PlayerPrefs.GetInt("UserThemeChoice", 0); // Get the saved theme choice

        switch (userChoice)
        {
            case 0:
                themeFolderName = "city";
                break;

            case 1:
                themeFolderName = "countryside";
                break;

            case 2:
                themeFolderName = "Desert";
                break;

            case 3:
                themeFolderName = "Fantasy";
                break;

            case 4:
                themeFolderName = "House pets";
                break;

            case 5:
                themeFolderName = "Rainforest";
                break;

            case 6:
                themeFolderName = "Sea";
                break;

            case 7:
                themeFolderName = "Space";
                break;

            case 8:
                themeFolderName = "Wild";
                break;

            case 9:
                themeFolderName = "Woods";
                break;

            default:
                themeFolderName = "city"; // Default theme in case of invalid input
                break;
        }
        return themeFolderName;
    }


    private void ChooseCardMaterials(string themeFolderName)
    {
        // Get the selected theme folder name
        string materialFolderName = "Materials";
        string logoFolderName = "Logo";
        string themeFolderPath = Path.Combine(materialFolderName, themeFolderName);

        Resources.UnloadUnusedAssets();
        

        Material[] allCardMaterials = Resources.LoadAll<Material>(themeFolderPath);
        allCardMaterials = allCardMaterials.GroupBy(p => p.name).Select(g => g.First()).ToArray(); // Removes duplicates, dont know why it happens

        // Calculate the number of materials to use (maximum 2 times each)
        int maxMaterials = columns * rows / 2;
        Material[] selectedMaterials = new Material[maxMaterials];
        ShuffleArray(allCardMaterials);

        // Copy the selected subset of materials
        for (int i = 0; i < maxMaterials; i++)
        {
            selectedMaterials[i] = allCardMaterials[i];
        }

        // save duplicate of selectedMaterials array and double it
        selectedMatsDuplicate = new Material[selectedMaterials.Length * 2];
        for (int i = 0; i < selectedMaterials.Length; i++)
        {
            selectedMatsDuplicate[i] = selectedMaterials[i];
            selectedMatsDuplicate[i + selectedMaterials.Length] = selectedMaterials[i];
        }
        // shuffle cardTexturesDuplicate
        ShuffleArray(selectedMatsDuplicate);

    }
    private IEnumerator LayOutCards()
    {
        yield return new WaitUntil(() => NetworkManager.Singleton.IsListening);

        // coordinates needed to center the placement of cards
        int rowsNumber = rows;
        int columnsNumber = columns;
        float xCenter = (cardLength * (rowsNumber - 1)) / 2;
        float zCenter = (cardLength * (columnsNumber - 1)) / 2;

        string themeFolderName = ThemePicker();
        ChooseCardMaterials(themeFolderName);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Debug.Log("CARD IN ROW: " + row);
                Debug.Log("CARD IN COLUMN: " + col);
                //Delete a material from the array
                cardImageMaterial = selectedMatsDuplicate[0];
                selectedMatsDuplicate = selectedMatsDuplicate.Skip(1).ToArray();
                InstantiateServerRpc(xCenter, zCenter, row, col);

            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void InstantiateServerRpc(float xCenter, float zCenter, int row, int col)
    {
        GameObject card = Instantiate(cardParent, new Vector3((row * cardLength) - xCenter, 0.0f, (col * cardLength) - zCenter), Quaternion.identity);
        DontDestroyOnLoad(card);
        NetworkObject networkCard = card.GetComponent<NetworkObject>();
        networkCard.Spawn();

        GameObject card_child = Instantiate(cardChild);
        DontDestroyOnLoad(card_child);
        NetworkObject networkCardChild = card_child.GetComponent<NetworkObject>();
        networkCardChild.Spawn();

        MeshRenderer renderer = card_child.GetComponent<MeshRenderer>();
        Material[] cardMaterials = renderer.materials;

        cardMaterials[1] = cardImageMaterial;
        renderer.materials = cardMaterials;
        card_child.transform.SetParent(card.transform);

    }


}
