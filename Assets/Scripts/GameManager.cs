
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text txtAccuracy = null;
    public Text txtWordPerMinute = null;
   

    public static bool GameIsPaused = false;
    public GameObject gameOverUI;
    public GameObject promptUI;
    public GameObject youWinUI;
    public string nextLevel = "Level2";
    public int levelToUnlock = 2;

    public void Start()
    {
	promptUI.SetActive(true);
       Time.timeScale = 0f;
       GameIsPaused = true;


        GameplayModule.OnLevelComplete += HandleOnLevelComplete;   
    }

    private void OnDestroy()
    {
        GameplayModule.OnLevelComplete -= HandleOnLevelComplete;
    }

    private void HandleOnLevelComplete()
    {
        YouWin();
    }

    public void EndGame ()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Game Over");
        GameIsPaused = true;
        
    }

    public void YouWin ()
    {
        youWinUI.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("You Win!");
        GameIsPaused = true;
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        txtAccuracy.text = "Accuracy: " + (ResultsModule.Instance.Accuracy() * 100).ToString();
        txtWordPerMinute.text = "WPM: " + (ResultsModule.Instance.WordPerMinute()).ToString();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Resume()
    {
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
    }

    
}

