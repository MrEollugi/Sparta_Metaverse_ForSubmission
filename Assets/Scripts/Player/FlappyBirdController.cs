using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class FlappyBirdController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 9f;
    private Rigidbody2D _rb;
    private bool _isAlive = true;
    private float moveSpeed = 3.5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Debug.Log($"[FBC Update] isAlive: {_isAlive}, Time: {Time.timeScale}");

        if (!_isAlive) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity = Vector2.up * jumpForce;
        }
    }

    private void OnEnable()
    {
        _isAlive = true;
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(moveSpeed, _rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"[Collision] with: {collision.gameObject.name}");

        if (!_isAlive) return;

        if(collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("GAME OVER by collision!");
            _isAlive = false;
            GameManager.Instance.GameOver();
        }

        _isAlive = false;
        GameManager.Instance.GameOver();
    }

    public void ResetState()
    {
        _isAlive = true;
        //_rb.velocity = Vector2.zero;

        BaseController baseController = GetComponent<BaseController>();
        if(baseController != null )
        {
            baseController.SetLookDirection(Vector2.right);
        }

    }
}
