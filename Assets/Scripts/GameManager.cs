using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isGameActive;
    public bool isGamePaused = false;
    public GameObject settingsPanel, levelCompletePanel, PauseButton, startingPanel, door, door2;
    public AudioSource audioSource;
    public AudioClip levelCompleteSound; 
    public int remainingCollectibles;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        isGameActive = false;
        Time.timeScale = 0; 
        PauseButton.SetActive(false);
        startingPanel.SetActive(true);
        settingsPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
    }

    public void CollectibleTaken()
    {
        remainingCollectibles--;
        if (remainingCollectibles <= 0)
        {
            Destroy(door);
            Destroy(door2);
        }
    }

    public void ToggleSettingsPanel()
    {
        isGamePaused = !isGamePaused;
        settingsPanel.SetActive(isGamePaused);
        Time.timeScale = isGamePaused ? 0 : 1;
    }

    public void StartGame()
    {
        isGameActive = true;
        Time.timeScale = 1; 
        startingPanel.SetActive(false);
        PauseButton.SetActive(true);
    }

    public void LevelComplete()
    {
        levelCompletePanel.SetActive(true);
        if (audioSource && levelCompleteSound) 
        {
            audioSource.PlayOneShot(levelCompleteSound);
        }
        FinishGame();
    }


    void FinishGame()
    {
        isGameActive = false;
        PauseButton.SetActive(false);
        Time.timeScale = 0;
    }
    
    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("This is the last level. Loading first level.");
            SceneManager.LoadScene(0);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ResetGame()
    {
        isGameActive = false;
        Time.timeScale = 0;
        startingPanel.SetActive(true);
        PauseButton.SetActive(false);
    }
}
