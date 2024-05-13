using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    public float speed = 25f;
    private Vector2 direction;
    private ShootControls shootScript;

    // Start is called before the first frame update
    void Start()
    {
        shootScript = GameObject.FindWithTag("Player").GetComponent<ShootControls>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += transform.up * (speed * Time.smoothDeltaTime);
        transform.position += (Vector3)direction * (speed * Time.smoothDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyLogic enemy = collision.gameObject.GetComponent<EnemyLogic>();
            enemy.calculateDamage(shootScript.damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Direction(Vector2 dir)
    {
        direction = dir.normalized;
    }
}
