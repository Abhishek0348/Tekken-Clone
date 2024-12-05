using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerSelection : MonoBehaviour
{
    public GameObject playerCharacters;  // Parent GameObject holding all characters
    private List<GameObject> allCharacters;  // List of all child character GameObjects
    private int selectedCharacter = 0;  // Index of currently selected character

    private void Start()
    {
        // Initialize list of characters from child GameObjects
        allCharacters = new List<GameObject>();
        for (int i = 0; i < playerCharacters.transform.childCount; i++)
        {
            allCharacters.Add(playerCharacters.transform.GetChild(i).gameObject);
            allCharacters[i].SetActive(false);
        }

        // Load selected character index from PlayerPrefs (if available)
        if (PlayerPrefs.HasKey("CharacterSelected"))
        {
            selectedCharacter = PlayerPrefs.GetInt("CharacterSelected");
        }

        ShowCurrentCharacter();
    }

    // Function to display only the currently selected character
    private void ShowCurrentCharacter()
    {
        for (int i = 0; i < allCharacters.Count; i++)
        {
            allCharacters[i].SetActive(i == selectedCharacter);  // Show selected, hide others
        }
    }

    public void NextCharacter()
    {
        selectedCharacter = (selectedCharacter + 1) % allCharacters.Count;
        ShowCurrentCharacter();
    }

    public void PreviousCharacter()
    {
        selectedCharacter = (selectedCharacter - 1 + allCharacters.Count) % allCharacters.Count;
        ShowCurrentCharacter();
    }

    public void ConfirmCharacter(string sceneName)
    {
        PlayerPrefs.SetInt("CharacterSelected", selectedCharacter);  // Save selected character index
        PlayerPrefs.Save();
        SceneManager.LoadScene(sceneName);  // Load next scene
    }

    

}
