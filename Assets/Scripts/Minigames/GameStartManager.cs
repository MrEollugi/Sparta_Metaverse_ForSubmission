using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private TextMeshProUGUI instructionText;

    private bool _gameStarted = false;

    private void Start()
    {
        Time.timeScale = 0f;
        _gameStarted = false;

        if(startPanel != null)
        {
            startPanel.SetActive(true);
        }
    }

    private void Update()
    {
        if (_gameStarted) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        _gameStarted=true;
        Time.timeScale = 1f;

        if(startPanel != null)
        {
            startPanel.SetActive(false);
        }
    }

}
