using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public Image currImage;
    public Sprite[] sprites;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeSprite());
    }

    IEnumerator ChangeSprite()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            currImage.sprite = sprites[index];
            index = index ^ 1;
        }
    }
}
