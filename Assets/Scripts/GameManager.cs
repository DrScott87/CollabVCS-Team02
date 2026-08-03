using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages core game state: score, lives, and game-over logic.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public int startingLives = 3;

    private int score;
    private int lives;
    private bool isGameOver;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        lives = startingLives;
        score = 0;
        isGameOver = false;
        Debug.Log("Game Started. Lives: " + lives);
    }

    public void AddScore(int points)
    {
        if (isGameOver) return;
        score += points;
        Debug.Log("Score: " + score);
    }

    public void LoseLife()
    {
        if (isGameOver) return;
        lives--;
        Debug.Log("Lives remaining: " + lives);
        if (lives <= 0) GameOver();
    }

    private void GameOver()
    {
        isGameOver = true;
        Debug.Log("GAME OVER! Final Score: " + score);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}