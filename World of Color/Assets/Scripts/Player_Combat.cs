using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Combat : MonoBehaviour
{
    public enum AttackDir
    {
        forward,
        backward,
        left,
        right
    }

    public AttackDir attackdir = AttackDir.forward;

    public void Attack1(InputAction.CallbackContext context)
    {

    }
}
