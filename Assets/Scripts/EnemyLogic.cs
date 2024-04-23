using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyLogic : MonoBehaviour
{
    private GameObject player;
    private Vector2 target;

    public float chaseSpeed = 10f;
    public float minDist = 1f;

    public float agroDist = 5f;
    public float calmDist = 10f;
    public float patrolDist = 10f;
    public float patrolSpeed = 5f;
    private Vector2 patrolPoint;
    bool movingRight = true;

    //State machine for patrol and chase
    private enum State
    {
        Patrol, 
        Chase
    }
    private State curState;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        patrolPoint = transform.position;
        curState = State.Patrol;    
    }

    // Update is called once per frame
    void Update()
    {
        switch (curState)
        {
            case State.Patrol:
                Patrol();
                //If player gets too close, chase
                if (Vector2.Distance(transform.position, player.transform.position) < agroDist)
                {
                    curState = State.Chase;
                }
                break;
            case State.Chase:
                Chase();
                //If player gets too far, stop chasing
                if (Vector2.Distance(transform.position, player.transform.position) > calmDist)
                {
                    curState = State.Patrol;
                    patrolPoint = transform.position; //Patrol current area
                }
                break;
        }


    }

    private void Chase()
    {
        //Follow player within minDist distance.
        if (Vector2.Distance(transform.position, player.transform.position) > minDist)
        {
            target = new Vector2(player.transform.position.x, transform.position.y); //Only get the x so they dont start hovering
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * chaseSpeed);
        }
    }

    private void Patrol()
    {
        //If end of patrol is reached, change direction. Maybe add short pause at end of patrol?
        if (movingRight && transform.position.x >= patrolPoint.x + patrolDist)
        {
            movingRight = false; 
        }
        else if (transform.position.x <= patrolPoint.x - patrolDist)
        {
            movingRight = true;
        }

        // Move in the current direction
        float moveX = movingRight ? patrolSpeed : -patrolSpeed;
        transform.position = new Vector2(transform.position.x + moveX * Time.deltaTime, transform.position.y);
    }
}
