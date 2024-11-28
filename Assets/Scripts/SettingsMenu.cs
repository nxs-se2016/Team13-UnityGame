//using UnityEngine;

//public class SettingsMenu : MonoBehaviour
//{
//    public GameObject settingsMenu; // Reference to the SettingsMenu UI Panel
//    public GameObject mainMenu;     // Reference to the MainMenu UI Panel

//    void Start()
//    {
//        // Ensure SettingsMenu is inactive when the game starts
//        if (settingsMenu != null)
//        {
//            settingsMenu.SetActive(false);
//        }
//        else
//        {
//            Debug.LogError("SettingsMenu: settingsMenu GameObject is not assigned in the Inspector.");
//        }
//    }

//    public void BackToMainMenu()
//    {
//        if (settingsMenu != null && mainMenu != null)
//        {
//            settingsMenu.SetActive(false);
//            mainMenu.SetActive(true);
//        }
//        else
//        {
//            Debug.LogError("SettingsMenu: settingsMenu or mainMenu GameObject is not assigned in the Inspector.");
//        }
//    }
//}

