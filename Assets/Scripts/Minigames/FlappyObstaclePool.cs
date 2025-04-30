using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyObstaclePool : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private int poolSize = 5;

    private Queue<GameObject> _pool = new Queue<GameObject>();

    public static FlappyObstaclePool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        for(int i = 0; i< poolSize; i++)
        {
            GameObject obj = Instantiate(obstaclePrefab, transform);
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        if(_pool.Count > 0)
        {
            GameObject obj = _pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        GameObject newObj = Instantiate(obstaclePrefab);
        return newObj;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }

}
