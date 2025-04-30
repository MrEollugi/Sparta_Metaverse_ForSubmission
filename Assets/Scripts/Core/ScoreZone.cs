using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    private bool _scored = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_scored) return;

        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(1);
            _scored = true;
        }
    }

    public void ResetZone()
    {
        _scored  =false;
    }
}
