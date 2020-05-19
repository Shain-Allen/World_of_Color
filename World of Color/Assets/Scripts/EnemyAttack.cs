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
            //enemyObj.myAnim.SetInteger("state", 1);
            Player.GetComponent<Player_Health>().health -= enemyObj.attackDamage;
            currCooldown = 0;
        }
        else
        {
            currCooldown += Time.deltaTime;
        }
    }

    public void setAttackParameters(Vector2 direction)
    {
        attackDirection = direction;
    }
}
