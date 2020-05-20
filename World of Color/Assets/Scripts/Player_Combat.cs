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
    public LayerMask enemyLayer;

    [SerializeField]
    float currentCoolDown;
    public bool isAttacking = false;
    public AttackDir attackdir = AttackDir.down;

    private void FixedUpdate()
    {
        if(isAttacking == true && currentCoolDown <= 0.1f)
        {
            Collider2D[] enemiesToDamage = null;
            //Debug.Log("Attacked in direction " + attackdir);
            if (attackdir == AttackDir.up)
            {
                enemiesToDamage = Physics2D.OverlapCircleAll(Vector2.up * attackRange, attackRange, enemyLayer);
            }
            else if(attackdir == AttackDir.down)
            {
                enemiesToDamage = Physics2D.OverlapCircleAll(Vector2.down * attackRange, attackRange, enemyLayer);
            }
            else if(attackdir == AttackDir.left)
            {
                enemiesToDamage = Physics2D.OverlapCircleAll(Vector2.left * attackRange, attackRange, enemyLayer);
            }
            else if(attackdir == AttackDir.right)
            {
                enemiesToDamage = Physics2D.OverlapCircleAll(Vector2.right * attackRange, attackRange, enemyLayer);
            }

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                
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
