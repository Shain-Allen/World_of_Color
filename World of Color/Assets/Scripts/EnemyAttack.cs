using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyObject enemyObj;

    //attack
    public Vector2 attackDirection = Vector2.up;
    public bool canAttack = false;  //true if enemy is within attacking range

    //cooldown
    public float currCooldown = 0.0f;
    public float maxCooldown = 1.0f;

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        enemyObj = GetComponent<EnemyObject>();

        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(canAttack && currCooldown >= maxCooldown)
        {
            //choose attack animation based on direction
            switchAnimations(attackDirection, true);

            //decrement enemy health
            Player.GetComponent<Player_Health>().health -= enemyObj.attackDamage;

            //reset cooldown
            currCooldown = 0;
        }
        else
        {
            //go to idle when not attacking
            enemyObj.myAnim.SetInteger("state", 4);
            enemyObj.myAnim.SetBool("attack", false);

            //increment cooldown
            currCooldown += Time.deltaTime;
        }
    }

    public void setAttackParameters(Vector2 direction)
    {
        attackDirection = direction;
    }

    //choose attack animation based on direction
    void switchAnimations(Vector2 direction, bool attack)
    {
        if (direction == Vector2.up)
        {
            enemyObj.myAnim.SetInteger("state", 5); //up attack
        }
        if (direction == Vector2.down)
        {
            enemyObj.myAnim.SetInteger("state", 6); //down attack
        }
        if (direction == Vector2.left)
        {
            enemyObj.myAnim.SetInteger("state", 7); //left attack
        }
        if (direction == Vector2.right)
        {
            enemyObj.myAnim.SetInteger("state", 8); //right attack
        }

        enemyObj.myAnim.SetBool("attack", attack);
    }
}
