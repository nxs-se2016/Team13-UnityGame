using UnityEngine;

public class GameManager : MonoBehaviour
{
    // References to UI panels
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    // Reference to the FPS_Controller script
    public FPS_Controller fpsController;

    // Game state variables
    public static bool isPaused = false;
    public static bool isGameStarted = false;

    void Start()
    {
        ShowMainMenu();

        // Set default mouse sensitivity (optional)
        SetMouseSensitivity(1f); // Set to your default lookSpeed value
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGameStarted)
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    #region MainMenu Methods
    public void ShowMainMenu()
    {
        isPaused = true;
        isGameStarted = false;
        Time.timeScale = 0f;

        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        isPaused = false;
        isGameStarted = true;
        Time.timeScale = 1f;

        mainMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenSettingsFromMainMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    #region PauseMenu Methods
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        pauseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ReturnToMainMenu()
    {
        ShowMainMenu();
    }
    #endregion

    #region SettingsMenu Methods
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        if (fpsController != null)
        {
            float baseLookSpeed = 100f; // Default lookSpeed
            float newLookSpeed = baseLookSpeed * sensitivity;
            fpsController.SetLookSpeed(newLookSpeed);
            // Debug.Log("Mouse Sensitivity set to: " + newLookSpeed);
        }
        else
        {
            Debug.LogError("GameManager: FPS_Controller is not assigned.");
        }
    }
    #endregion
}
