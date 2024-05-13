using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string nextScene = "Assets/Scenes/CaitlinSampleScene.unity";
    public void PlayGame()
    {
        SceneManager.LoadScene(nextScene);
    }
}
