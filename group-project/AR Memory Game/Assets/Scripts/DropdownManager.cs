using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown userThemeDropdown;
    // Start is called before the first frame update
    void Start()
    {
        // Check if the "UserThemeChoice" key exists in PlayerPrefs
        if (PlayerPrefs.HasKey("UserThemeChoice"))
        {
            // Load the previously saved theme choice from PlayerPrefs
            int savedThemeChoice = PlayerPrefs.GetInt("UserThemeChoice", 0);

            // Set the dropdown value to the saved choice
            userThemeDropdown.value = savedThemeChoice;
        }
        else
        {
            // Set a default theme choice if no saved choice exists
            userThemeDropdown.value = 0; // Set the default dropdown value
            SaveThemeChoice(0); // Save the default theme choice
        }
    }

    public void OnUserThemeDropdownValueChanged()
    {
        string selectedTheme = userThemeDropdown.options[userThemeDropdown.value].text;
        SaveThemeChoice(userThemeDropdown.value);
    }

    private void SaveThemeChoice(int choice)
    {
        PlayerPrefs.SetInt("UserThemeChoice", choice);
    }
}
