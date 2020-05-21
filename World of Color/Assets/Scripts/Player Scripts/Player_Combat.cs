using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Combat : MonoBehaviour
{
    public enum AttackDir
    {
        down,
        up,
        left,
        right
    }

    public float attackCoolDown = 1f;
    public float attackRange = 2f;
    public ContactFilter2D enemyLayer;

    [SerializeField]
    float currentCoolDown;
    public bool isAttacking = false;
    public AttackDir attackdir = AttackDir.down;
    public PolygonCollider2D[] attackArea;

    public Animator anim;

    public AudioSource attackSound;

    private void FixedUpdate()
    {
        if(isAttacking == true && currentCoolDown <= 0.1f)
        {
            attackSound.Play();

            Collider2D[] enemiesToDamage = null;
            //Debug.Log("Attacked in direction " + attackdir);
            if (attackdir == AttackDir.up)
            {
                //indexing into the array via enum
                Physics2D.OverlapCollider(attackArea[(int)attackdir], enemyLayer, enemiesToDamage);
            }
            else if(attackdir == AttackDir.down)
            {
                Physics2D.OverlapCollider(attackArea[(int)attackdir], enemyLayer, enemiesToDamage);
            }
            else if(attackdir == AttackDir.left)
            {
                Physics2D.OverlapCollider(attackArea[(int)attackdir], enemyLayer, enemiesToDamage);
            }
            else if(attackdir == AttackDir.right)
            {
                Physics2D.OverlapCollider(attackArea[(int)attackdir], enemyLayer, enemiesToDamage);
            }

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(0);
            }

            currentCoolDown = attackCoolDown;
        }
        currentCoolDown -= Time.deltaTime;
    }

    public void Attack1(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() >= 0.1f)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }
}
