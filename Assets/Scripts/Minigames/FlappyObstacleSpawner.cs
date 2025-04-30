using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;

    [SerializeField] private float minSpawnDelay = 1.5f;
    [SerializeField] private float maxSpawnDelay = 2.5f;

    [SerializeField] private float minY = -1f;
    [SerializeField] private float maxY = 2f;

    [SerializeField] private float minHoleSize = 0.1f;
    [SerializeField] private float maxHoleSize = 1f;

    [SerializeField] private float spawnOffsetX = 2f;

    private float _timer = 0f;
    private float _nextSpawnDelay;

    private void Start()
    {
        _nextSpawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= _nextSpawnDelay)
        {
            _timer = 0f;
            _nextSpawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);

            float spawnY = Random.Range(minY, maxY);
            float holeSize = Random.Range(minHoleSize, maxHoleSize);

            Vector3 camRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
            Vector3 spawnPos = new Vector3(camRightEdge.x + spawnOffsetX, spawnY, 0);

            GameObject obj = FlappyObstaclePool.Instance.Get();
            obj.transform.position = spawnPos;

            FlappyObstacle fo = obj.GetComponent<FlappyObstacle>();
            if(fo != null)
            {
                fo.SetHoleSize(holeSize);
            }

        }
    }
}
