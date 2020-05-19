using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 1;

    public Vector2 attackDirection = Vector2.up;
    public bool canAttack = false;

    public float currCooldown = 0.0f;
    public float maxCooldown = 1.0f;

    public GameObject Player;

    public Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(canAttack)
        {
            //myAnim.SetInteger("state", 1);
            Player.GetComponent<Player_Health>().health--;
            canAttack = false;
            currCooldown = 0;
        }
        else
        {
            currCooldown += Time.deltaTime;
            if(currCooldown > maxCooldown)
            {
                currCooldown = 0;
            }
        }
    }

    public void setAttackParameters(Vector2 direction)
    {
        canAttack = true;
        attackDirection = direction;
    }
}
