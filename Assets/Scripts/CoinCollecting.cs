using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollecting : MonoBehaviour
{
    public Text coinScore = null;
    private int numberOfCoins;
    private ShootControls shootScript;
    // Start is called before the first frame update
    void Start()
    {
        shootScript = GetComponent<ShootControls>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            gameObject.GetComponent<ShootControls>();
            if (numberOfCoins >= 5)
            {
                numberOfCoins -= 5;
                shootScript.damage += 1;

                coinScore.text = "Score: " + numberOfCoins;
                Debug.Log("Score: " + numberOfCoins);
            }

        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            numberOfCoins++;
            Destroy(collision.gameObject);
            coinScore.text = "Score: " + numberOfCoins;
            Debug.Log("Score: " +  numberOfCoins);
        }
    }
}
