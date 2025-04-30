using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [Header ("Components")]
    private Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody => _rigidbody;
    [SerializeField] private SpriteRenderer _characterRenderer;
    [SerializeField] private Transform _weaponPivot;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 4f;

    protected Vector2 _movementDirection = Vector2.zero;
    public Vector2 MovementDirection => _movementDirection;

    protected Vector2 _lookDirection = Vector2.right;
    public Vector2 LookDirection => _lookDirection;

    private Vector2 _knockbackForce = Vector2.zero;
    private float _knockbackTimeRemaining = 0f;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleActions();
        RotateTowards(_lookDirection);
    }

    protected virtual void FixedUpdate()
    {
        MoveCharacter(_movementDirection);

        if(_knockbackTimeRemaining > 0f)
        {
            _knockbackTimeRemaining -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleActions()
    {

    }

    protected virtual void MoveCharacter(Vector2 direction)
    {
        if(direction.sqrMagnitude > 1f)
        {
            direction = direction.normalized;
        }

        Vector2 finalMove = direction.normalized * _moveSpeed;
        
        if(_knockbackTimeRemaining > 0f)
        {
            finalMove *= 0.2f;
            finalMove += _knockbackForce;
        }

        _rigidbody.velocity = finalMove;
    }

    protected virtual void RotateTowards(Vector2 direction)
    {
        if(direction != Vector2.zero)
        {
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bool isLeft = Mathf.Abs(rotZ) > 90f;

            if(_characterRenderer != null)
            {
                _characterRenderer.flipX = isLeft;
            }

            if(_weaponPivot != null)
            {
                _weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
            }
        }
    }

    public virtual void ApplyKnockback(Transform attacker, float power, float duration)
    {
        _knockbackForce = -(attacker.position - transform.position).normalized * power;
        _knockbackTimeRemaining = duration;
    }

    public void SetLookDirection(Vector2 direction)
    {
        _lookDirection = direction.normalized;
        RotateTowards(_lookDirection);
    }

}
