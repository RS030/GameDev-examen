using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject gameOverScreen;
    public GameObject winScreen;

    private int lives = 3;
    private int fruitCount = 0;
    private int fruitNeededToWin = 30;

    public bool isGameActive = false;

    private void Start()
    {
        Time.timeScale = 1f;

        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    public void StartGame()
    {
        lives = 3;
        fruitCount = 0;
        isGameActive = true;

        startScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        winScreen.SetActive(false);

        FindFirstObjectByType<SpawnManager>().StartSpawning();

        Debug.Log("Game started");
    }

    public void AddFruit()
    {
        if (!isGameActive)
        {
            return;
        }

        fruitCount++;

        Debug.Log("Fruit: " + fruitCount + "/" + fruitNeededToWin);

        if (fruitCount >= fruitNeededToWin)
        {
            WinGame();
        }
    }

    public void LoseLife()
    {
        if (!isGameActive)
        {
            return;
        }

        lives--;

        Debug.Log("Lives: " + lives);

        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameActive = false;
        gameOverScreen.SetActive(true);

        Debug.Log("GAME OVER");
    }

    private void WinGame()
    {
        isGameActive = false;
        winScreen.SetActive(true);

        Debug.Log("YOU WIN");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}