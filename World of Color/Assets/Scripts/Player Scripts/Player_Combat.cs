using System;
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

    public float attackCoolDown = 1f;
    public float attackRange = 2f;
    public ContactFilter2D enemyLayer;

    [SerializeField]
    float currentCoolDown;
    public bool isAttacking = false;
    public AttackDir attackdir = AttackDir.forward;
    public PolygonCollider2D[] attackArea = new PolygonCollider2D[4];

    public Animator anim;

    //sounds
    public PlayerSounds mySounds;
    public AudioSource audioSource;

    private void FixedUpdate()
    {
        if(isAttacking == true && currentCoolDown <= 0.1f)
        {
            anim.SetBool("IsAttacking", true);
            audioSource.PlayOneShot(mySounds.player_attack);

            Collider2D[] enemiesToDamage = new Collider2D[50];
            int numOfEnemies = 0;
            //Debug.Log("Attacked in direction " + attackdir);
            if (attackdir == AttackDir.backward)
            {
                //indexing into the array via enum
                numOfEnemies = Physics2D.OverlapCollider(attackArea[(int)attackdir], enemyLayer, enemiesToDamage);
            }
            else if(attackdir == AttackDir.forward)
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

            //since the colliders are on child objects you need to get the parent which has the health script on it
            for (int i = 0; i < numOfEnemies; i++)
            {
                if (enemiesToDamage[i].gameObject.GetComponent<EnemyHealth>() != null)
                {
                    enemiesToDamage[i].gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
                }
            }

            currentCoolDown = attackCoolDown;
        }
        currentCoolDown -= Time.deltaTime;
    }

    //again InputAction.CallbackContext is to get the value from the input component
    public void Attack1(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() >= 0.1f)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
            anim.SetBool("IsAttacking", false);
        }
    }
}
