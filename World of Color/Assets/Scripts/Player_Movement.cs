using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    public Transform transform;
    public float movementSpeed = 5f;
    Vector2 moveDir;

    public void FixedUpdate()
    {
        if (moveDir.x > 0.1)
        {
            //move right
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
        }
        else if (moveDir.x < -0.1)
        {
            //move left
            transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
        }
        else if (moveDir.y > 0.1)
        {
            //move up
            transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
        }
        else if (moveDir.y < -0.1)
        {
            //move down
            transform.Translate(Vector2.down * movementSpeed * Time.deltaTime);
        }
    }


    public void Move(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }
}
