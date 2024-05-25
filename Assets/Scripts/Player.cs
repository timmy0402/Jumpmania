using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private GameController gameController;
    public HealthBar healthBar;
    public float iframe = 2f;
    private float timer;
    private Vector2 spawn = new Vector2(-17f, -3.3f);
    private ButtonController buttonController;
    public string loseScene = "Assets/Scenes/LoseScreen.unity";

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        buttonController = FindAnyObjectByType<ButtonController>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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
            SceneManager.LoadScene(loseScene);
        }

        if (transform.position.y < -10)
        {
            transform.position = spawn;
        }
    }
    public void increaseHealth()
    {
        if (gameController.coins >= 10)
        {
            if (currentHealth <= 80)
            {
                currentHealth += 20;
            }
            else
            {
                currentHealth = 100;
            }
            gameController.coins -= 10;
            buttonController.button3.gameObject.SetActive(false);
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
        if (other.tag == "Heart")
        {
            Destroy(other.gameObject);
            currentHealth += 10;
            healthBar.SetHealth(currentHealth);
        }
    }
}