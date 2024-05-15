using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemyLogic : MonoBehaviour
{
    private GameObject player;
    private Vector2 target;
    public GameObject booletPrefab;

    public float chaseSpeed = 10f;
    public int hp = 4;
    public int eStrength = 10;
    public int projDmg = 5;
    public float shootCooldown = 5f;
    private float knockbackTimer;
    private float shootTimer;

    public bool shooter = false;
    public float agroDist = 5f;
    public float calmDist = 10f;
    public float patrolDist = 10f;
    public float patrolSpeed = 5f;
    private Vector2 patrolPoint;
    bool movingRight = true;

    public GameObject item;
    private GameObject parentSpawner;
    private IndependantSpawner spawnerIndep;
    private GlobalSpawner spawnerGlobal;

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
        player = GameObject.FindWithTag("Player");
        patrolPoint = transform.position;
        curState = State.Patrol;    
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            spawnerGlobal.destroyEnemy();
            Destroy(gameObject);
        }

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
        target = new Vector2(player.transform.position.x, transform.position.y); //Only get the x so they dont start hovering
        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * chaseSpeed);

        if (shooter && Time.time > shootTimer)
        {
            shootTimer = Time.time + shootCooldown;
            shootAtPlayer();
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

    private void shootAtPlayer()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 directionToPlayer = playerPosition - transform.position;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        GameObject boolet = Instantiate(booletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        boolet.GetComponent<EnemyProjectile>().setOwner(gameObject);

        // Set the direction for the projectile logic
        EnemyProjectile enemyProjectile = boolet.GetComponent<EnemyProjectile>();
        enemyProjectile.Direction(new Vector2(directionToPlayer.x, directionToPlayer.y));
    }


    public void setSpawner(GameObject spawner)
    {
        parentSpawner = spawner;
        spawnerIndep = parentSpawner.GetComponent<IndependantSpawner>();
        spawnerGlobal = spawner.GetComponent<GlobalSpawner>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player playerHealth = collision.gameObject.GetComponent<Player>();
            playerHealth.DamagePlayer(eStrength);
            if (knockbackTimer < Time.time)
            {
                knockbackTimer = Time.time + .5f;
                Knockback knockback = collision.gameObject.GetComponent<Knockback>();
                knockback.PlayFeedback(gameObject);
            }

        }
    }

    //For calculating varying levels of damage based on player stats
    public void calculateDamage(int dmg)
    {
        Debug.Log("Hit!");
        hp -= dmg;
        if (hp <= 0)
        {
            if (spawnerGlobal != null)
            {
                //Destroy(gameObject);
                spawnerGlobal.destroyEnemy();
            }
            else if (spawnerIndep != null)
            {
                //Destroy(gameObject);
                spawnerIndep.destroyEnemy();
            }
            else
            {
                Debug.Log("No parent spawner found!");
            }
            DropItem();
            Destroy(gameObject);
        }
    }

    public void DropItem()
    {
        GameObject drop = Instantiate(item);
        drop.transform.position = transform.position;
    }
}
