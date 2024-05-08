using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSpawner : MonoBehaviour
{
    public float spawnCooldown = 5f;
    private float timer;

    public int maxEnemy;
    private int numEnemy;
   
    public GameObject[] enemy; //Array in case we have multiple enemy types
    private GameObject[] spawnpoints;
    private GameObject newEnemy;

    // Start is called before the first frame update
    void Start()
    {
        //Store all currently placed spawnpoints
        spawnpoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        foreach (GameObject spawnpoint in spawnpoints)
        {
            //Make spawnpoints invisible when game runs
            SpriteRenderer sr = spawnpoint.GetComponent<SpriteRenderer>();
            sr.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (numEnemy < maxEnemy && Time.time > timer)
        {
            int randEnemy = Random.Range(0, enemy.Length); //Randomly select enemy from array of types
            int spawnpoint = Random.Range(0, spawnpoints.Length); //Randomly select spawnpoint

            newEnemy = Instantiate(enemy[randEnemy]);
            numEnemy++;

            EnemyLogic enemyLogic = newEnemy.GetComponent<EnemyLogic>();
            enemyLogic.setSpawner(gameObject);

            newEnemy.transform.position = spawnpoints[spawnpoint].transform.position;
            timer = Time.time + spawnCooldown;
        }
    }
    public void destroyEnemy()
    {
        numEnemy--;
    }
}
