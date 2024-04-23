using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicPlayer : MonoBehaviour
{
    private float speed = 20f;
    private float horizontalInput;
    private float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Player WASD movement
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if (horizontalInput != 0)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime * -horizontalInput);
        }
        if (verticalInput != 0)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime * verticalInput);
        }
        //
    }
}
