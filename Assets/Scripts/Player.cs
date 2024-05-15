using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private GameController gameController;
    public HealthBar healthBar;
    public float iframe = 2f;
    private float timer;
    private GameObject spawn;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        spawn = GameObject.FindWithTag("Respawn");
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            currentHealth -= 15;
            healthBar.SetHealth(currentHealth);
        }*/

        if (currentHealth <= 0)
        {
            Time.timeScale = 0;
            Destroy(gameObject);
        }

        if (transform.position.y < -10)
        {
            transform.position = spawn.transform.position;
        }
    }

    public void DamagePlayer(int damage)
    {
        if (Time.time > timer)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            if (currentHealth <= 0)
            {
                Debug.Log("Game Over!");
            }
            timer = Time.time + iframe;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);
            gameController.coins++;
        }
        if (other.tag == "Heart")
        {
            Destroy(other.gameObject);
            currentHealth += 10;
            healthBar.SetHealth(currentHealth);
        }
    }
}