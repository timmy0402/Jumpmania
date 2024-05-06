using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        
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
            enemy.calculateDamage(damage);
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
