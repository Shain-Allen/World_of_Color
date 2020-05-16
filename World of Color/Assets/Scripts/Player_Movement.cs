using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    PlayerControlls input;

    //move
    Vector2 movementInput;

    private void Awake()
    {
        input = new PlayerControlls();
        input.Player.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
