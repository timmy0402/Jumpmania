using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private GameController gameController;
    public HealthBar healthBar;
    public float iframe = 2f;
    private float timer;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            currentHealth -= 15;
            healthBar.SetHealth(currentHealth);
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
    }
}