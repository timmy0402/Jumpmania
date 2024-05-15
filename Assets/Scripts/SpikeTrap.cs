using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class SpikeTrap : MonoBehaviour
{
    private int damage = 25;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player playerHealth = collision.gameObject.GetComponent<Player>();
            playerHealth.DamagePlayer(damage);

            Knockback knockback = collision.gameObject.GetComponent<Knockback>();
            knockback.PlayFeedback(gameObject);
        }
    }
}
