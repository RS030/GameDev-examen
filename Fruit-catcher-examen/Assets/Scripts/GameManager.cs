using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject gameOverScreen;
    public GameObject winScreen;

    public TextMeshProUGUI fruitText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverResultText;
    public TextMeshProUGUI startText;

    private int lives = 3;
    private int fruitCount = 0;
    private int fruitNeededToWin = 25;

    public bool isGameActive = false;

    private void Start()
    {
        Time.timeScale = 1f;

        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        winScreen.SetActive(false);

        fruitText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);

        UpdateStartText();
        UpdateUI();
    }

    public void SelectEasy()
    {
        fruitNeededToWin = 20;
        lives = 3;
        UpdateStartText();
    }

    public void SelectNormal()
    {
        fruitNeededToWin = 40;
        lives = 3;
        UpdateStartText();
    }

    public void SelectHard()
    {
        fruitNeededToWin = 80;
        lives = 1;
        UpdateStartText();
    }

    public void StartGame()
    {
        fruitCount = 0;
        isGameActive = true;

        startScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        winScreen.SetActive(false);

        fruitText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);

        UpdateUI();

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

        UpdateUI();

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

        UpdateUI();

        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void UpdateStartText()
    {
        startText.text = "Fruit te vangen: " + fruitNeededToWin;
    }

    private void UpdateUI()
    {
        fruitText.text = "Fruit: " + fruitCount + "/" + fruitNeededToWin;
        livesText.text = GetHearts();
    }

    private string GetHearts()
    {
        string hearts = "";

        for (int i = 0; i < lives; i++)
        {
            hearts += "♥ ";
        }

        return hearts;
    }

    private void GameOver()
    {
        isGameActive = false;
        gameOverScreen.SetActive(true);

        fruitText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);

        gameOverResultText.text = "Fruit gevangen: " + fruitCount + "/" + fruitNeededToWin;

        Debug.Log("GAME OVER");
    }

    private void WinGame()
    {
        isGameActive = false;
        winScreen.SetActive(true);

        fruitText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);

        Debug.Log("YOU WIN");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}