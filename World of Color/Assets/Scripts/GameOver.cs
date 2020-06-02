using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public AudioSource gameOverSound;

    // Start is called before the first frame update
    void Start()
    {
        gameOverSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
