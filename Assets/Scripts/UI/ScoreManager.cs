using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int CurrentScore {  get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int value)
    {
        CurrentScore += value;
    }

    public void ResetScore()
    {
        CurrentScore = 0;
    }

    public void SaveHighScore()
    {
        int best = PlayerPrefs.GetInt("HighScore", 0);
        if (CurrentScore > best)
        {
            PlayerPrefs.SetInt("HighScore", CurrentScore);
        }
    }
}
