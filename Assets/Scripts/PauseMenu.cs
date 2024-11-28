//using UnityEngine;

//public class PauseMenu : MonoBehaviour
//{
//    public GameObject pauseMenu; // Reference to the PauseMenu UI Panel
//    public GameObject mainMenu;  // Reference to the MainMenu UI Panel
//    public static bool isPaused;

//    void Start()
//    {
//        pauseMenu.SetActive(false);
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            if (isPaused)
//            {
//                ResumeGame();
//            }
//            else if (FPS_Controller.isGameStarted)
//            {
//                PauseGame();
//            }
//        }
//    }

//    public void PauseGame()
//    {
//        pauseMenu.SetActive(true);
//        Time.timeScale = 0f;
//        isPaused = true;
//        Cursor.lockState = CursorLockMode.None;
//        Cursor.visible = true;
//    }

//    public void ResumeGame()
//    {
//        pauseMenu.SetActive(false);
//        Time.timeScale = 1f;
//        isPaused = false;
//        Cursor.lockState = CursorLockMode.Locked;
//        Cursor.visible = false;
//    }

//    public void GoToMainMenu()
//    {
//        Time.timeScale = 0f; // Pause the game
//        isPaused = true;
//        pauseMenu.SetActive(false);

//        // Show MainMenu panel
//        if (mainMenu != null)
//        {
//            mainMenu.SetActive(true);
//        }
//        else
//        {
//            Debug.LogError("PauseMenu: mainMenu GameObject is not assigned in the Inspector.");
//        }

//        // Set game as not started
//        FPS_Controller.isGameStarted = false;

//        // Show and unlock the cursor
//        Cursor.lockState = CursorLockMode.None;
//        Cursor.visible = true;
//    }

//    public void QuitGame()
//    {
//        Application.Quit();
//    }
//}

