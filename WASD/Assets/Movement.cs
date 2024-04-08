using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting triangle: " + gameObject.name);
    }

    public float speed = 5;

    private float leftControlTime;


    // Update is called once per frame
    void Update()
    {


        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            pos.y += speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            pos.y -= speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            pos.x += speed * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed *= 2;
        }


        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = 40;
            leftControlTime = Time.time;
        }

        if (Time.time - leftControlTime > 2)
        {
            speed = 5;
        }


        transform.position = pos;
    }
}
