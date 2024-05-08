using UnityEngine;

public class IndependantSpawner : MonoBehaviour
{
    public GameObject enemyType;
    private GameObject newEnemy;
    public float spawnCooldown = 5f;
    private float timer;
    public int maxEnemy = 4;
    private int numEnemy;

    // Start is called before the first frame update
    void Start()
    {
        //Make spawnpoints invisible when game runs
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (numEnemy < maxEnemy && Time.time > timer)
        {
            newEnemy = Instantiate(enemyType);
            EnemyLogic enemyLogic = newEnemy.GetComponent<EnemyLogic>();
            enemyLogic.setSpawner(gameObject);
            numEnemy++;
            newEnemy.transform.position = transform.position;
            timer = Time.time + spawnCooldown;
        }
    }

    public void destroyEnemy()
    {
        numEnemy--;
    }
}
