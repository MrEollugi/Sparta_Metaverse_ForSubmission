using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGamePortal : MonoBehaviour
{
    [SerializeField] private string _miniGameSceneName = "FlappyBirdScene";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameMode.Current = GameModeType.Flappy;
            SceneManager.LoadScene(_miniGameSceneName);
        }
    }
}
