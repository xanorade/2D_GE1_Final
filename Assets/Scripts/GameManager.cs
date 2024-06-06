using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager instance;

    // Enum defining game states
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver,
    }

    // Current game state
    public GameState currentState;

    // Previous game state
    public GameState previousState;

    [Header("Screens")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;

    [Header("Current Stat Displays")]
    public TMP_Text currentHealthDisplay;
    public TMP_Text enemiesDead;
    public TMP_Text collectedExperience;
    public TMP_Text currentLevel;

    [Header("Results Screen Displays")]
    public Image chosenCharacterImage;
    public TMP_Text chosenCharacterName;
    public TMP_Text levelReachedDisplay;
    public TMP_Text timeSurvivedDisplay;

    [Header("Stopwatch")]
    public float timeLimit;
    float stopwatchTime;
    public TMP_Text stopwatchDisplay;

    public bool isGameOver = false;

    public GameObject playerObject;

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("EXTRA " + this + " DELETED");
            Destroy(gameObject);
        }

        DisableScreens();
    }

    void Update()
    {
        // Manage game state based on the current state
        switch (currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                UpdateStopwatch();
                break;
            case GameState.Paused:
                CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                if (!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f;
                    Debug.Log("GAME IS OVER");
                    DisplayResults();
                }
                break;
            default:
                Debug.LogWarning("STATE DOESN'T EXIST");
                break;
        }
    }

    // Change the current game state
    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    // Pause the game
    public void PauseGame()
    {
        if (currentState != GameState.Paused)
        {
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            Debug.Log("Game is paused");
        }
    }

    // Resume the game
    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            Debug.Log("Game is resumed");
        }
    }

    // Check for pause and resume inputs
    void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Disable pause and result screens at the start
    void DisableScreens()
    {
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
    }

    // Trigger game over state
    public void GameOver()
    {
        timeSurvivedDisplay.text = stopwatchDisplay.text;
        ChangeState(GameState.GameOver);
    }

    // Display game results
    void DisplayResults()
    {
        resultsScreen.SetActive(true);
    }

    // Assign chosen character UI data
    public void AssignChosenCharacterUI(CharacterScriptableObject chosenCharacterData)
    {
        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.name;
    }

    // Assign level reached UI data
    public void AssignLevelReachedUI(int levelReachedData)
    {
        levelReachedDisplay.text = levelReachedData.ToString();
    }

    // Update the stopwatch
    void UpdateStopwatch()
    {
        stopwatchTime += Time.deltaTime;

        UpdateStopwatchDisplay();

        if (stopwatchTime >= timeLimit)
        {
            playerObject.SendMessage("Kill");
        }
    }

    // Update the stopwatch display
    void UpdateStopwatchDisplay()
    {
        int minutes = Mathf.FloorToInt(stopwatchTime / 60);
        int seconds = Mathf.FloorToInt(stopwatchTime % 60);

        stopwatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
