using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    protected override void HandleActions()
    {
        HandleMovementInput();
        HandleLookDirection();
    }

    private void HandleMovementInput()
    {
        if(DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueActive)
        {
            _movementDirection = Vector2.zero;
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _movementDirection = new Vector2(moveX, moveY);
    }

    private void HandleLookDirection()
    {
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueActive)
            return;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookVector = mouseWorldPosition - transform.position;
        _lookDirection = lookVector.normalized;
    }

}
