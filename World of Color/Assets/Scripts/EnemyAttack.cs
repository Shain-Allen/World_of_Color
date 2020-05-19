using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
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
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(canAttack && currCooldown >= maxCooldown)
        {
            //play attack animation
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
