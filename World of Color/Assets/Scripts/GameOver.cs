using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public AudioSource gameOverSound;

    public GameObject FadeCanvas;

    // Start is called before the first frame update
    void Start()
    {
        gameOverSound.Play();
        StartCoroutine(FadeOut());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeOut()
    {
        FadeCanvas.GetComponent<Animator>().SetInteger("fade_direction", -1);
        yield return new WaitForSeconds(0.9f);
        FadeCanvas.SetActive(false);
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
