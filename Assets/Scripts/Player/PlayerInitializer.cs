using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInitializer : MonoBehaviour
{
    private static PlayerInitializer _instance;

    private Rigidbody2D _rb;

    [Header("중력 설정값")]
    [SerializeField] private float flappyGravity = 2f;
    [SerializeField] private float mainGravity = 0f;

    [Header("시작 위치")]
    [SerializeField] private Vector3 flappyStartPos = new Vector3(-6f, 0f, 0f);
    [SerializeField] private Vector3 mainStartPos = new Vector3(0f, 0f, 0f);

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        _rb = GetComponent<Rigidbody2D>();
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
        InitializePlayer();
    }

    private void InitializePlayer()
    {
        switch (GameMode.Current)
        {
            case GameModeType.Flappy:
                _rb.gravityScale = flappyGravity;
                GetComponent<PlayerController>().enabled = false;

                var flappy = GetComponent<FlappyBirdController>();
                GetComponent<FlappyBirdController>().enabled = true;
                flappy.ResetState();

                transform.position = flappyStartPos;
                break;
            case GameModeType.Main:
                _rb.gravityScale = mainGravity;
                GetComponent<PlayerController>().enabled = true;
                GetComponent<FlappyBirdController>().enabled = false;
                transform.position = mainStartPos;
                break;
        }
        Debug.Log($"[Init] Mode: {GameMode.Current}, FlappyBirdController: {GetComponent<FlappyBirdController>().enabled}");

    }
}
