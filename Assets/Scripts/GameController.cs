using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public Text score;
    public int coins = 0;
    public GameObject popup;
    private bool activatePopup = true;
    private ShootControls shootScript;
    // Start is called before the first frame update
    void Start()
    {
        shootScript = GameObject.FindWithTag("Player").GetComponent<ShootControls>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Coins: " + coins;
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P pressed");
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
            popup.SetActive(activatePopup);
            activatePopup = !activatePopup;

        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            gameObject.GetComponent<ShootControls>();
            if (coins >= 5)
            {
                coins -= 5;
                shootScript.damage += 1;
            }
        }

    }
}
