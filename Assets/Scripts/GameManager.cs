using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// Manages game state: score, lives, countdown timer, and game-over logic.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public int startingLives = 3;

    [Header("Timer Settings")]
    public float roundDuration = 120f;
    public UnityEvent onTimerExpired;

    private int score;
    private int lives;
    private bool isGameOver;
    private float timeRemaining;
    private bool timerRunning;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        lives = startingLives; score = 0; isGameOver = false;
        timeRemaining = roundDuration; timerRunning = true;
        Debug.Log($"Game Started. Lives: {lives} | Time: {roundDuration}s");
    }

    void Update()
    {
        if (timerRunning && !isGameOver)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f; timerRunning = false;
                onTimerExpired?.Invoke();
                GameOver();
            }
        }
    }

    public void AddScore(int points)
    {
        if (isGameOver) return;
        score += points;
        Debug.Log("Score: " + score);
    }

    public void AddTime(float seconds)
    {
        timeRemaining += seconds;
        Debug.Log($"Time bonus! +{seconds}s. Remaining: {timeRemaining:F1}s");
    }

    public float GetTimeRemaining() => timeRemaining;

    public void LoseLife()
    {
        if (isGameOver) return;
        lives--;
        Debug.Log("Lives remaining: " + lives);
        if (lives <= 0) GameOver();
    }

    private void GameOver()
    {
        isGameOver = true; timerRunning = false;
        Debug.Log($"GAME OVER! Final Score: {score} | Time Left: {timeRemaining:F1}s");
    }

    public void RestartGame() =>
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}