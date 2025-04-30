using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        retryButton.onClick.AddListener(() => GameManager.Instance.Retry());
        exitButton.onClick.AddListener(() => GameManager.Instance.ReturnToMain());
    }
}
