using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;

    [SerializeField] private GameObject gameOverPanel;



    private int _score = 0;
    private bool _isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;
        UpdateScoreUI();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        scoreText = GameObject.FindWithTag("ScoreText")?.GetComponent<TextMeshProUGUI>();
        highScoreText = GameObject.FindWithTag("HighScoreText")?.GetComponent<TextMeshProUGUI>();
        currentScoreText = GameObject.FindWithTag("CurrentScoreText")?.GetComponent<TextMeshProUGUI>();

        gameOverPanel = GameObject.FindWithTag("GameOverPanel");
        Time.timeScale = 1f;
        _score = 0;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        UpdateHighScoreUI();
    }

    public void AddScore(int value)
    {
        if (_isGameOver) return;

        _score += value;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {_score}";
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        SaveHighScore();
        UpdateHighScoreUI();
        UpdateCurrentScoreUI();
    }

    public void Retry()
    {
        _isGameOver = false;
        _score = 0;

        Time.timeScale = 1f;

        GameMode.Current = GameModeType.Flappy;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMain()
    {
        _isGameOver = false;
        _score = 0;

        GameMode.Current = GameModeType.Main;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    private void SaveHighScore()
    {
        int best = PlayerPrefs.GetInt("HighScore", 0);
        if(_score > best)
        {
            PlayerPrefs.SetInt("HighScore", _score);
            PlayerPrefs.Save();
        }
    }

    private void UpdateHighScoreUI()
    {
        GameObject highScoreObj = GameObject.FindWithTag("HighScoreText");
        if(highScoreObj != null)
        {
            TextMeshProUGUI highScoreText = highScoreObj.GetComponent<TextMeshProUGUI>();
            int high = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = $"High Score: {high}";
        }
    }

    private void UpdateCurrentScoreUI()
    {
        if(currentScoreText != null)
        {
            currentScoreText.text = $"Score: {_score}";
        }
    }
    
}
