using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float jumpForce = 10f;
    public float jumpDelay = 0.25f;
    public float runningJumpForce = 5f;
    public LayerMask groundLayer;
    public float groundLength = 0.27f;
    public float linearDrag = 4f;
    public float gravity = 1;
    public float fallMultiplier = 5f;
    public Vector3 colliderOffset;
    private bool onGround;
    private Rigidbody2D rb;
    private float jumpTimer;
    public GameObject booletPrefab;

    public ParticleSystem dust;
    // Start is called before the first frame update
    void Start()
    {
    }
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    public float speed = 5;
    private float maxSpeed = 7;
    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer)
        || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);
        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
            Debug.Log(jumpTimer);
        }
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.A))
        {
            CreateDust();
            pos.x -= speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            CreateDust();
            pos.x += speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        transform.position = pos;

        //Shooting stuff
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //GameObject boolet = Instantiate(Resources.Load<GameObject>("Prefabs/Boolet"));
            shootBoolet(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            shootBoolet(Vector2.right);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            shootBoolet(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            shootBoolet(Vector2.down);
        }

    }

    private void shootBoolet(Vector2 direction)
    {
        GameObject boolet = Instantiate(booletPrefab, transform.position, Quaternion.identity);
        ProjectileLogic projectileLogic = boolet.GetComponent<ProjectileLogic>();
        projectileLogic.Direction(direction);

    }

    void FixedUpdate()
    {
        if (jumpTimer > Time.time && onGround)
        {
            Jump();
        }
        ModifyPhysics();
    }
    private void ModifyPhysics()
    {
        if (!onGround)
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }

        else
        {
            rb.drag = 0f;
            rb.gravityScale = 0;
        }
    }
    private void Jump()
    {
        float runJump = (Mathf.Abs(rb.velocity.x) / maxSpeed) * runningJumpForce;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * (jumpForce + runJump), ForceMode2D.Impulse);
        jumpTimer = 0;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }
    void CreateDust()
    {
        dust.Play();
    }
}
