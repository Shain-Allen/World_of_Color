//Menu_Controller.cs
//By: Conor Brennan
//Last Edited: 5/17/2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu_Controller : MonoBehaviour
{
    public AudioMixer mainMixer;

    public void Next_Scene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Set_Volume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
    }
}
 