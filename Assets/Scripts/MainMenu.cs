//using UnityEngine;

//public class MainMenu : MonoBehaviour
//{
//    public GameObject mainMenu;     // Reference to the MainMenu UI Panel
//    public GameObject settingsMenu; // Reference to the SettingsMenu UI Panel

//    void Start()
//    {
//        // Ensure MainMenu is active and SettingsMenu is inactive when the game starts
//        if (mainMenu != null)
//        {
//            mainMenu.SetActive(true);
//        }
//        else
//        {
//            Debug.LogError("MainMenu: mainMenu GameObject is not assigned in the Inspector.");
//        }

//        if (settingsMenu != null)
//        {
//            settingsMenu.SetActive(false);
//        }
//        else
//        {
//            Debug.LogError("MainMenu: settingsMenu GameObject is not assigned in the Inspector.");
//        }

//        // Ensure the cursor is visible and unlocked
//        Cursor.lockState = CursorLockMode.None;
//        Cursor.visible = true;

//        // Pause the game when in main menu
//        Time.timeScale = 0f;
//        PauseMenu.isPaused = true;
//        FPS_Controller.isGameStarted = false;
//    }

//    public void PlayGame()
//    {
//        if (mainMenu != null)
//        {
//            mainMenu.SetActive(false);
//        }
//        else
//        {
//            Debug.LogError("MainMenu: mainMenu GameObject is not assigned in the Inspector.");
//        }

//        // Unlock the cursor and allow player control
//        Cursor.lockState = CursorLockMode.Locked;
//        Cursor.visible = false;
//        Time.timeScale = 1f;

//        // Since the game was paused at the main menu, ensure that isPaused is false
//        PauseMenu.isPaused = false;
//        FPS_Controller.isGameStarted = true;
//    }

//    public void OpenSettings()
//    {
//        if (mainMenu != null && settingsMenu != null)
//        {
//            mainMenu.SetActive(false);
//            settingsMenu.SetActive(true);
//        }
//        else
//        {
//            Debug.LogError("MainMenu: mainMenu or settingsMenu GameObject is not assigned in the Inspector.");
//        }
//    }

//    public void QuitGame()
//    {
//        Application.Quit();
//    }
//}


