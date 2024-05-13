using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControls : MonoBehaviour
{
    public GameObject booletPrefab;
    Vector3 mousePosition;
    Vector3 objectPosition;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Shooting stuff
        /*if (Input.GetKeyDown(KeyCode.LeftArrow))
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
        }*/
        if (Input.GetMouseButtonDown(0))
        {
            shootBoolet();
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
}
