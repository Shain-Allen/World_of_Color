using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // in case we add additional movement options
    public enum EnemyState
    {
        Chase,
        Patrol
    }

    public EnemyState enemyState = EnemyState.Chase;
    public float distToChasePlayer = float.MaxValue;

    //movement stats
    public float speed = 0.1f;
    public Vector2 direction = Vector2.up;

    //calculating movement goals/targets
    public float buffer = 1.0f;
    public Vector2 target = Vector2.zero;
    public Vector2 destination = Vector2.zero;
    public bool isDoneMoving = true;

    private Rigidbody2D myRb;
    private Animator myAnim;

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();

        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //behavior depends on distance from player
        if(Vector2.Distance(Player.transform.position, transform.position) <= distToChasePlayer)
        {
            enemyState = EnemyState.Chase;
        }
        else
        {
            enemyState = EnemyState.Patrol;
        }

        //don't calculate a new target/path if we're still moving on the previous path
        if (isDoneMoving)
        {
            isDoneMoving = false;

            //different movement patterns based on state
            switch (enemyState)
            {
                case EnemyState.Chase:

                    //only move if we haven't reached the target
                    if (!isOnTarget())
                    {
                        target = FindClosestAdjacent(Player.transform.position);
                        ChoosePath(target);
                    }
                    //stp moving and attack when we reach the target
                    else
                    {
                        isDoneMoving = true;
                        GetComponent<EnemyAttack>().setAttackParameters(direction);

                    }
                    break;
                    
                case EnemyState.Patrol:
                    //travel on a fixed path?
                    break;
            }
        }
    }

    //find the closest adjacent tile to the player (up/down/left/right) to use as the target (don't travel directly on top of the player
    Vector2 FindClosestAdjacent(Vector2 basePos)
    {
        float distToTarget = float.MaxValue;

        int closest = 0;

        Vector2[] possibleTargets = { new Vector2(basePos.x + buffer, basePos.y), new Vector2(basePos.x - buffer, basePos.y), new Vector2(basePos.x, basePos.y + buffer), new Vector2(basePos.x, basePos.y - buffer) };

        for (int i = 0; i < possibleTargets.Length; i++)
        {
            if(distToTarget > Vector2.Distance(transform.position, possibleTargets[i]))
            {
                distToTarget = Vector2.Distance(transform.position, possibleTargets[i]);
                closest = i;
            }
        }

        return possibleTargets[closest];
    }

    //choose the best possible option for getting to a certain target
    void ChoosePath(Vector2 target)
    {
        //enemy can choose to continue going straight, turn left/right, or turn back around (currently only 4 directions)
        Vector2[] possibleMovements = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        //for keeping track of the optimal path
        float distToTarget = float.MaxValue;
        int bestPath = 0;   //default is to continue in the current direction

        //choose whether to turn, double back, or continue
        for(int i = 0; i < possibleMovements.Length; i++)
        {
            if(distToTarget > Vector2.Distance((Vector2)transform.position + possibleMovements[i], target))
            {
                bestPath = i;
                distToTarget = Vector2.Distance((Vector2)transform.position + possibleMovements[i], target);
            }
        }

        direction = possibleMovements[bestPath];
        //switchAnimations(direction);

        destination = (Vector2)transform.position + direction;

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        //keep moving until we reach our current destination (make sure we're not going past our target)
        while (Vector2.Distance(transform.position, destination) > 0.0001f && !isOnTarget())
        {
            Vector2 newPos = Vector2.MoveTowards(transform.position, destination, speed);
            myRb.MovePosition(newPos);
            yield return new WaitForFixedUpdate();
        }

        isDoneMoving = true;
        StopAllCoroutines();
    }

    bool isOnTarget()
    {
        //direction is facing player
        direction = (Vector2)Player.transform.position - target;
        direction.Normalize();

        if(Vector2.Distance((Vector2)transform.position + (direction * buffer), Player.transform.position) > 0.5f)
        {
            return false;
        }
        return true;
    }

    //switch animation based on direction
    void switchAnimations(Vector2 direction)
    {
        if(direction == Vector2.up)
        {
            //myAnim.SetInteger("state", 1);
        }
        if (direction == Vector2.down)
        {
            //myAnim.SetInteger("state", 1);
        }
        if (direction == Vector2.left)
        {
            //myAnim.SetInteger("state", 1);
        }
        if (direction == Vector2.right)
        {
            //myAnim.SetInteger("state", 1);
        }
    }
}
