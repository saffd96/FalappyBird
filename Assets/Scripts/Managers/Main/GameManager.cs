using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnScoreChanged;

    private static int score;
    public static int HighScore { get; private set; }

    public static bool IsPlayerDead { get; private set; }

    public static bool IsGameStarted { get; private set; }

    private void Awake()
    {
        ResetGame();
        HighScore = PlayerPrefs.GetInt("HighScoreKey");
    }

    private void OnEnable()
    {
        Bird.OnCenterTrigger += AddScore;
        Bird.OnPipeCollision += OnPlayerDeath;
        Bird.OnFirstJump += StartGame;
    }

    private void OnDisable()
    {
        Bird.OnCenterTrigger -= AddScore;
        Bird.OnPipeCollision -= OnPlayerDeath;
        Bird.OnFirstJump -= StartGame;
    }

    private void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score);
    }

    private void OnPlayerDeath()
    {
        IsPlayerDead = true;

        if (score <= HighScore) return;

        HighScore = score;
        PlayerPrefs.SetInt("HighScoreKey", HighScore);
    }

    private void ResetGame()
    {
        score = 0;
        HighScore = 0;
        IsPlayerDead = false;
        IsGameStarted = false;
    }

    private void StartGame()
    {
        IsGameStarted = true;
    }
}
