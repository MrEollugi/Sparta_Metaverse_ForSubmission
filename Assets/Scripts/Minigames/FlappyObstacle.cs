using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyObstacle : MonoBehaviour
{
    [SerializeField] private Transform obstacleTop;
    [SerializeField] private Transform obstacleBottom;
    [SerializeField] private BoxCollider2D scoreZone;

    [SerializeField] private float holeSize = 2f;
    private float _despawnX;

    private void Start()
    {
        ApplyHoleSize();
    }

    private void OnEnable()
    {
        float camHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        _despawnX = Camera.main.transform.position.x - camHalfWidth - 2f;
        if(scoreZone != null)
        {
            ScoreZone zone = scoreZone.GetComponent<ScoreZone>();
            zone?.ResetZone();
        }
    }

    private void Update()
    {
        float camHalfWidth =Camera.main.orthographicSize * Camera.main.aspect;
        float currentDespawnX = Camera.main.transform.position.x - camHalfWidth - 2f;

        if(transform.position.x < currentDespawnX)
        {
            Debug.Log("Returned to Pool");
            FlappyObstaclePool.Instance.Return(gameObject);
        }
    }

    public void ApplyHoleSize()
    {
        float halfHole = holeSize / 2f;

        obstacleTop.localPosition = new Vector3(0, halfHole + obstacleTop.localScale.y / 2f, 0);
        obstacleBottom.localPosition = new Vector3(0, -halfHole - obstacleBottom.localScale.y / 2f, 0);

        if(scoreZone != null)
        {
            float thickness = 0.1f;

            float topY = obstacleTop.position.y - (obstacleTop.localScale.y / 2f);
            float bottomY = obstacleBottom.position.y + (obstacleBottom.localScale.y / 2f);
            float height = topY - bottomY;

            float centerY = (topY + bottomY) / 2f;

            scoreZone.offset = transform.InverseTransformPoint(new Vector2(transform.position.x, centerY));
            scoreZone.size = new Vector2(thickness, height);
        }
    }

    public void SetHoleSize(float newSize)
    {
        holeSize = newSize;
        ApplyHoleSize();
    }

}
