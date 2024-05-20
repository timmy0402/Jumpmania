using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControls : MonoBehaviour
{
    public GameObject booletPrefab;
    Vector3 mousePosition;
    Vector3 objectPosition;
    private float angle;
    private GameController gameController;
    public int damage = 25;
    private bool shotgunUpgraded = false;
    private ButtonController buttonController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindAnyObjectByType<GameController>();
        buttonController = FindAnyObjectByType<ButtonController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Shooting stuff
        if (Input.GetMouseButtonDown(0))
        {
            if (!shotgunUpgraded)
            {
                shootBoolet();
            }
            else
            {
                shotgun();
            }
        }
    }
    public void increaseDamge()
    {
        if (gameController.coins >= 10)
        {
            buttonController.button2.gameObject.SetActive(false);
            gameController.coins -= 10;
            damage += 25;
        }
    }
    public void UpdateGun()
    {
        if (gameController.coins >= 10)
        {
            buttonController.button1.gameObject.SetActive(false);
            gameController.coins -= 4;
            shotgunUpgraded = true;
        }
    }
    private void shootBoolet()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = 5.23f;
        objectPosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePosition.x = mousePosition.x - objectPosition.x;
        mousePosition.y = mousePosition.y - objectPosition.y;
        angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        GameObject boolet = Instantiate(booletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        ProjectileLogic projectileLogic = boolet.GetComponent<ProjectileLogic>();
        projectileLogic.Direction(new Vector2(mousePosition.x, mousePosition.y));
    }
    private void shotgun()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = 5.23f;
        objectPosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePosition.x = mousePosition.x - objectPosition.x;
        mousePosition.y = mousePosition.y - objectPosition.y;
        angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        // Define the spread of the shotgun
        float spread = 15f;
        int numberOfBullets = 3;

        for (int i = 0; i < numberOfBullets; i++)
        {
            // Calculate the deviation by subtracting half the spread and then adding the spread times the ratio
            float deviation = spread * (i / (float)numberOfBullets) - spread / 2f;

            GameObject boolet = Instantiate(booletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle + deviation)));
            ProjectileLogic projectileLogic = boolet.GetComponent<ProjectileLogic>();
            // Calculate the vector for each bullet by the unit circle
            Vector2 direction = new Vector2(
                mousePosition.x * Mathf.Cos(deviation * Mathf.Deg2Rad) - mousePosition.y * Mathf.Sin(deviation * Mathf.Deg2Rad),
                mousePosition.x * Mathf.Sin(deviation * Mathf.Deg2Rad) + mousePosition.y * Mathf.Cos(deviation * Mathf.Deg2Rad));
            projectileLogic.Direction(direction);
        }

    }
}
