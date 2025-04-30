using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Tilemap _tilemap;

    public Vector2 _minPosition;
    public Vector2 _maxPosition;

    private float _camHalfHeight;
    private float _camHalfWidth;

    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;

        if(GameMode.Current == GameModeType.Main && _tilemap != null)
        {
            BoundsInt bounds = _tilemap.cellBounds;

            _camHalfHeight = Camera.main.orthographicSize;
            _camHalfWidth = _camHalfHeight * Camera.main.aspect;

            _minPosition = new Vector2(bounds.min.x, bounds.min.y) + new Vector2(_camHalfWidth, _camHalfHeight);
            _maxPosition = new Vector2(bounds.max.x, bounds.max.y) - new Vector2(_camHalfWidth, _camHalfHeight);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void LateUpdate()
    {
        if (_target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                _target = player.transform;
            }
        }

        if (_target == null) return;

        switch (GameMode.Current)
        {
            case GameModeType.Main:
                Vector3 newPos = new Vector3(_target.position.x, _target.position.y, transform.position.z);
                newPos.x = Mathf.Clamp(newPos.x, _minPosition.x, _maxPosition.x);
                newPos.y = Mathf.Clamp(newPos.y, _minPosition.y, _maxPosition.y);
                transform.position = newPos;
                break;

            case GameModeType.Flappy:
                Vector3 flappyPos = transform.position;
                flappyPos.x = _target.position.x;
                transform.position = flappyPos;
                break;
            default:
                break;
        }

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                _target = player.transform;
            }
        }
    }
}
