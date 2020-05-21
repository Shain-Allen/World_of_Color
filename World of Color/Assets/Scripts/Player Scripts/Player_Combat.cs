﻿using System;
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
    public PolygonCollider2D[] attackArea = new PolygonCollider2D[4];

    public Animator anim;

    public AudioSource attackSound;

    private void FixedUpdate()
    {
        if(isAttacking == true && currentCoolDown <= 0.1f)
        {
            attackSound.Play();

            Collider2D[] enemiesToDamage = new Collider2D[50];
            int numOfEnemies = 0;
            //Debug.Log("Attacked in direction " + attackdir);
            if (attackdir == AttackDir.up)
            {
                //indexing into the array via enum
                numOfEnemies = Physics2D.OverlapCollider(attackArea[(int)attackdir], enemyLayer, enemiesToDamage);
            }
            else if(attackdir == AttackDir.down)
            {
                numOfEnemies = Physics2D.OverlapCollider(attackArea[(int)attackdir], enemyLayer, enemiesToDamage);
            }
            else if(attackdir == AttackDir.left)
            {
                numOfEnemies = Physics2D.OverlapCollider(attackArea[(int)attackdir], enemyLayer, enemiesToDamage);
            }
            else if(attackdir == AttackDir.right)
            {
                numOfEnemies = Physics2D.OverlapCollider(attackArea[(int)attackdir], enemyLayer, enemiesToDamage);
            }

            for (int i = 0; i < numOfEnemies; i++)
            {
                if (enemiesToDamage[i].GetComponent<EnemyHealth>() != null)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(0);
                }
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
