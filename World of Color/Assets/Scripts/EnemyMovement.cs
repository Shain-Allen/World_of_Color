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

    //movement stats
    public float speed = 1.0f;
    public Vector2 direction = new Vector2(1, 0);
    public Vector2 tempDestination = Vector2.zero;

    private Rigidbody2D myRb;
    private Animator myAnim;

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyState)
        {
            //find the closest adjacent tile from the player and move towards it
            case EnemyState.Chase:
                ChoosePath(ChooseTarget((Vector2)Player.transform.position));
                break;
            //idk yet
            case EnemyState.Patrol:
                Debug.Log("to be determined");
                break;
        }
    }

    //find the closest adjacent tile to the player (up/down/left/right)
    Vector2 ChooseTarget(Vector2 initTarget)
    {
        float distToTarget = Vector2.Distance(transform.position, initTarget);
        float minDistToTarget = distToTarget;

        int closest = 0;

        Vector2[] possibleTargets = { new Vector2(initTarget.x + 1, initTarget.y), new Vector2(initTarget.x - 1, initTarget.y), new Vector2(initTarget.x, initTarget.y + 1), new Vector2(initTarget.x, initTarget.y - 1) };

        for (int i = 0; i < possibleTargets.Length; i++)
        {
            distToTarget = Vector2.Distance(transform.position, possibleTargets[i]);
            if(distToTarget < minDistToTarget)
            {
                minDistToTarget = distToTarget;
                closest = i;
            }
        }

        return possibleTargets[closest];
    }

    //choose the best possible option for getting to a certain target
    void ChoosePath(Vector2 target)
    {
        //enemy can choose to continue going straight, turn left/right, or turn back around (currently only 4 directions)
        Vector2[] possibleDirections = { direction, -direction, new Vector2(direction.y, direction.x), new Vector2(-direction.y, -direction.x) };

        //for keeping track of the optimal path
        float distToTarget = float.MaxValue;
        int bestPath = 0;   //default is current direction

        //choose whether to turn, double back, or continue
        for(int i = 0; i < possibleDirections.Length; i++)
        {
            if(distToTarget > Vector2.Distance((Vector2)transform.position + possibleDirections[i], target))
            {
                bestPath = i;
                distToTarget = Vector2.Distance((Vector2)transform.position + possibleDirections[i], target);
            }
        }

        direction = possibleDirections[bestPath];

        Move((Vector2)transform.position + direction);
    }

    void Move(Vector2 destination)
    {
        //keep moving until we reach our destination
        while(Vector2.Distance(transform.position, destination) >= 0.001)
        {
            Vector2 newPos = Vector2.MoveTowards(transform.position, destination, speed);
            myRb.MovePosition(newPos);
        }

    }

    //switch animations based on what direction we are moving in
    void SwitchAnimation()
    {
        

    }
}
