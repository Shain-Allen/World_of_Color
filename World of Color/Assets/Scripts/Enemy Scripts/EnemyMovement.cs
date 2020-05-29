using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMovement : MonoBehaviour
{
    public GenericStats myStats;
    public EnemyAttack myAttack;

    // in case we add additional movement options
    public enum EnemyState
    {
        Chase,
        Patrol,
        Purified
    }
    public EnemyState enemyState = EnemyState.Patrol;
    public float distToChasePlayer = 5.0f;

    //calculating movement goals/targets for chase
    public Vector2 direction = Vector2.up;
    public Vector2 destination;
    public bool isDoneMoving = true;
   
    //for patrol
    public Vector2[] patrolTargets;
    public int currPatrolTarget = 0;
    public bool reachedTarget = false;

    //for chase
    public Vector2 chaseTarget;
    public Vector2 minDistFromPlayer = new Vector2(1.5f, 1.75f);  //stopping distance from player (diff x and y since player isn't a perfect square)
    public float buffer = 0.3f;

    //for purified/wander
    public Vector2 wanderTarget;
    public float timeInIdle;
    public bool isIdle = false;

    public Animator myAnim;
    public Rigidbody2D myRb;

    public GameObject Player;
    public int roomNumber;
    public float[] roomBounds = new float[4];   //min x, max x, min y, max y

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        myRb.velocity = Vector2.zero;

        //once purified, there's no need for chase or patrol
        if (enemyState == EnemyState.Purified)
        {
            myAnim.SetBool("is_purified", true);
        }
        else
        {
            //behavior depends on distance from player and player's overall position
            if (roomNumber == Player.GetComponent<Player_Controller>().currRoom && canChase() && Vector2.Distance(Player.transform.position, transform.position) <= distToChasePlayer)
            {
                enemyState = EnemyState.Chase;
            }
            else
            {
                enemyState = EnemyState.Patrol;
            }
        }

        //don't calculate a new target/path if we're still moving on the previous path
        if (isDoneMoving)
        {
            isDoneMoving = false;

            //different movement patterns based on state
            switch (enemyState)
            {
                case EnemyState.Chase:
                    chaseTarget = FindClosestAdjacent(Player.transform.position);

                    //only move if we haven't reached the target
                    if (!IsCloseToPlayer(buffer))
                    {
                        myAttack.canAttack = false;
                        ChoosePath(chaseTarget);
                    }
                    //stop moving and attack when we reach the target
                    else
                    {
                        isDoneMoving = true;
                        SwitchIdleAnimations(direction);
                        myAttack.canAttack = true;
                        myAttack.attackDirection = direction;
                    }
                    break;
                    
                case EnemyState.Patrol:
                    //check if we reached our current target
                    if (Vector2.Distance(transform.position, patrolTargets[currPatrolTarget]) <= buffer)
                    {
                        reachedTarget = true;
                    }

                    //get the next patrol point if we reached the current target
                    if (reachedTarget)
                    {
                        currPatrolTarget = (currPatrolTarget + 1) % patrolTargets.Length;
                        reachedTarget = false;
                    }

                    //move to that point
                    ChoosePath(patrolTargets[currPatrolTarget]);
                    break;

                case EnemyState.Purified:
                    //if we're idle, stay in idle until time runs out
                    if (isIdle)
                    {
                        isDoneMoving = true;
                        SwitchIdleAnimations(direction);
                        timeInIdle -= Time.deltaTime;
                        if (timeInIdle <= 0)
                        {
                            isIdle = false;

                        }
                    }
                    //if we're not in idle, get a new target
                    else
                    {
                        //check if we reached our current target
                        if (Vector2.Distance(transform.position, wanderTarget) <= buffer)
                        {
                            reachedTarget = true;
                        }

                        //if we've reached, get a new target
                        if (reachedTarget)
                        {
                            //30% chance of idle
                            if (Random.Range(0, 3) > 1)
                            {
                                timeInIdle = Random.Range(1.0f, 5.0f);
                                isIdle = true;
                            }
                            else
                            {
                                wanderTarget = new Vector2(Random.Range(roomBounds[0], roomBounds[1]), Random.Range(roomBounds[2], roomBounds[3]));
                                wanderTarget.x = Mathf.Round(wanderTarget.x);
                                wanderTarget.y = Mathf.Round(wanderTarget.y);
                                reachedTarget = false;
                            }
                        }
                        ChoosePath(wanderTarget);
                    }
                    break;
            }
        }
    }

    bool canChase()
    {
        //enemy can't chase at edges of the room
        if(Player.transform.position.x > roomBounds[0] && Player.transform.position.x < roomBounds[1] && Player.transform.position.y > roomBounds[2] && Player.transform.position.y < roomBounds[3])
        {
            return true;
        }
        return false;
    }

    //find the closest adjacent tile to the player (up/down/left/right) to use as the target (don't travel directly on top of the player
    Vector2 FindClosestAdjacent(Vector2 basePos)
    {
        float distToTarget = float.MaxValue;

        int closest = 0;

        Vector2[] possibleTargets = { new Vector2(basePos.x + minDistFromPlayer.x, basePos.y), new Vector2(basePos.x - minDistFromPlayer.x, basePos.y), new Vector2(basePos.x, basePos.y + minDistFromPlayer.y), new Vector2(basePos.x, basePos.y - minDistFromPlayer.y) };

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
            //make sure the direction isn't going into a wall
            if(IsValidDirection(possibleMovements[i]) && distToTarget > Vector2.Distance((Vector2)transform.position + possibleMovements[i], target))
            {
                bestPath = i;
                distToTarget = Vector2.Distance((Vector2)transform.position + possibleMovements[i], target);
            }
        }

        direction = possibleMovements[bestPath];
        SwitchWalkAnimations(direction);

        destination = (Vector2)transform.position + direction;
        destination.x = Mathf.Round(destination.x);
        destination.y = Mathf.Round(destination.y);

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        //keep moving until we reach our current destination (make sure we're not going too close to the player)
        while (Vector2.Distance(transform.position, destination) > 0.001f)
        {
            //if we're chasing make sure were not too close to the player
            if(enemyState == EnemyState.Chase && IsCloseToPlayer(buffer))
            {
                break;
            }

            //move
            Vector2 newPos = Vector2.MoveTowards(transform.position, destination, myStats.movementSpeed);
            myRb.MovePosition(newPos);
            yield return new WaitForFixedUpdate();
        }

        isDoneMoving = true;
        StopAllCoroutines();
    }


    //check if we're within <dist> of player
    bool IsCloseToPlayer(float dist)
    {
        //direction is facing player
        Vector2 tempDirection = (Vector2)Player.transform.position - chaseTarget;
        tempDirection.Normalize();

        if (Vector2.Distance((Vector2)transform.position + new Vector2(tempDirection.x * minDistFromPlayer.x, tempDirection.y * minDistFromPlayer.y), Player.transform.position) <= dist)
        {
            //if we have reached the target, face the player
            direction = tempDirection;
            return true;
        }

        return false;
    }

    //make sure we aren't bumping into a wall
    private bool IsValidDirection(Vector2 direction)
    {
        //draw a line to the next tile over (+ a little bit) and check if the wall is in the way
        Vector2 pos = transform.position;
        direction *= 2.5f;

        //check layers to make sure we're not trying to walk into the player or another enemy
        int wallLayer = 1 << 10;
        //int enemyLayer = 1 << 9;
        int combinedLayerMask = wallLayer /*| enemyLayer*/;
        RaycastHit2D hit = Physics2D.Linecast(pos + direction, pos, combinedLayerMask);
        //return true if it doesn't detect the wall
        return hit.collider == null;
    }

    //switch animation based on what direction we're moving in/facing
    void SwitchWalkAnimations(Vector2 direction)
    {
        if(direction == Vector2.up)
        {
            myAnim.SetFloat("walk_direction", 0.0f); //up
        }
        if (direction == Vector2.down)
        {
            myAnim.SetFloat("walk_direction", 1.0f); //down
        }
        if (direction == Vector2.left)
        {
            myAnim.SetFloat("walk_direction", 2.0f); //left
        }
        if (direction == Vector2.right)
        {
            myAnim.SetFloat("walk_direction", 3.0f); //right
        }

        myAttack.currCollider.SetActive(true);
        myAnim.SetBool("is_walking", true);
    }


    public void SwitchIdleAnimations(Vector2 direction)
    {
        if (direction == Vector2.up)
        {
            myAnim.SetFloat("idle_direction", 0.0f); //up
        }
        if (direction == Vector2.down)
        {
            myAnim.SetFloat("idle_direction", 1.0f); //down
        }
        if (direction == Vector2.left)
        {
            myAnim.SetFloat("idle_direction", 2.0f); //left
        }
        if (direction == Vector2.right)
        {
            myAnim.SetFloat("idle_direction", 3.0f); //right
        }

        myAnim.SetBool("is_walking", false);
    }

}
