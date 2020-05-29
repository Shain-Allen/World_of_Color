using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
#pragma warning disable
    public Transform transform;
    public Animator anim;
    public float movementSpeed = 5f;
    [HideInInspector]
    public Vector2 moveDir;

    public int currRoom = 1;

    //attack combat direction passthrough
    public Player_Combat combatdir;

    public void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //check where the player wants to move and store the last pressed direction
        if (moveDir.x > 0.1)
        {
            //move right
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
            combatdir.attackdir = Player_Combat.AttackDir.right;
            anim.SetBool("IsMoving", true);
        }
        else if (moveDir.x < -0.1)
        {
            //move left
            transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
            combatdir.attackdir = Player_Combat.AttackDir.left;
            anim.SetBool("IsMoving", true);
        }
        else if (moveDir.y > 0.1)
        {
            //move up
            transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
            combatdir.attackdir = Player_Combat.AttackDir.backward;
            anim.SetBool("IsMoving", true);
        }
        else if (moveDir.y < -0.1)
        {
            //move down
            transform.Translate(Vector2.down * movementSpeed * Time.deltaTime);
            combatdir.attackdir = Player_Combat.AttackDir.forward;
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }

        anim.SetFloat("MoveDir", (float)combatdir.attackdir);
    }

    //this gets pluged into the player input component on the player
    //InputAction.CallbackContext lets the input component feed in the values its getting from the controller
    public void Move(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }
}
