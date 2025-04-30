using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private Transform[] scrollGroups;
    [SerializeField] private SpriteRenderer referenceBackground;

    private float scrollThreshold = 12f;
    private float scrollWidth = 8f;
    private float camHalfWidth;
    private Transform _cam;

    private void Start()
    {
        _cam = Camera.main.transform;
        camHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;

        if(referenceBackground != null)
        {
            scrollWidth = referenceBackground.bounds.size.x;
            scrollThreshold = scrollWidth + camHalfWidth + 3f;
        }
    }

    private void Update()
    {
        foreach (var group in scrollGroups)
        {
            if(_cam.position.x - group.position.x > scrollThreshold)
            {
                float rightMost = GetRightmostX(group);
                group.position = new Vector3(rightMost + scrollWidth, group.position.y, group.position.z);
            }
        }
    }

    private float GetRightmostX(Transform exclude = null)
    {
        float maxX = float.MinValue;
        foreach(var group in scrollGroups)
        {
            if (group == exclude) continue;
            if(group.position.x > maxX)
            {
                maxX = group.position.x;
            }
        }
        return maxX;
    }
}
