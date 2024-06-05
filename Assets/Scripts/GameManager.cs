using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum GameState
    { 
    Gameplay,
    Paused,
    GameOver,
    }



    public GameState currentState;

    public GameState previousState;

    [Header("Screens")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;

    [Header("Current Stat Displays")]
    public Text currentHealthDisplay;
    public Text enemiesDead;
    public Text collectedExperience;
    public Text currentLevel;

    [Header("Results Screen Displays")]
    public Image chosenCharacterImage;
    public Text chosenCharacterName;
    public Text levelReachedDisplay;
    public Text timeSurvivedDisplay;

    [Header("Stopwatch")]
    public float timeLimit;
    float stopwatchTime;
    public Text stopwatchDisplay;


    public bool isGameOver = false;

    public GameObject playerObject;

    void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Debug.LogWarning("EXTRA" + this + "DELETED");
            Destroy(gameObject);
        }


        DisableScreens();
    }
    

    void Update()
    {
        
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

    public void ChangeState(GameState newState) 
    {
        currentState = newState;
    }


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

    void DisableScreens() 
    {
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
    }

    public void GameOver() 
    {
        timeSurvivedDisplay.text = stopwatchDisplay.text;
        ChangeState(GameState.GameOver);
    }

    void DisplayResults() 
    {
        resultsScreen.SetActive(true);
    }

    public void AssignChosenCharacterUI(CharacterScriptableObject chosenCharacterData) 
    {
        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.name;
    }

    public void AssignLevelReachedUI(int levelReachedData) 
    { 
    levelReachedDisplay.text = levelReachedData.ToString();
    }

    void UpdateStopwatch() 
    {
        stopwatchTime += Time.deltaTime;

        UpdateStopwatchDisplay();

        if (stopwatchTime >= timeLimit)
        {
            playerObject.SendMessage("Kill");
        }

    }

    void UpdateStopwatchDisplay() 
    {
        int minutes = Mathf.FloorToInt(stopwatchTime/60);
        int seconds = Mathf.FloorToInt(stopwatchTime % 60);

        stopwatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
