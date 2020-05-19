//Menu_Controller.cs
//By: Conor Brennan
//Last Edited: 5/17/2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class Menu_Controller : MonoBehaviour
{
    public AudioMixer mainMixer;

    public TMPro.TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start()
    {
        //gives the resolutions array all available screen resolutions, not including resolutions with different refresh rates
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();

        resolutionDropdown.ClearOptions();

        //creates a new list of strings
        List<string> options = new List<string>();

        int currResIndex = 0;

        //adds each resolution from the resolutions array as a string to the options list
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            //if the resolution being added to the options list is equal to the screen's current resolution, it's saved in currResIndex
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currResIndex = i;
            }
        }
        //adds the list of resolution options to the resolution dropdown's options
        resolutionDropdown.AddOptions(options);
        // sets the resolution dropdown's value to currResIndex
        resolutionDropdown.value = currResIndex;
        //refreshes the resolution dropdown's shown value so it shows the screen's current resolution
        resolutionDropdown.RefreshShownValue();
    }

    //loads the scene that comes after the current scene in the build index
    public void Next_Scene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //closes the application
    public void Quit()
    {
        Application.Quit();
    }

    //sets the value of the main mixer equal to the value of the volume slider
    public void Set_Volume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
    }

    //toggles bewteen fullscreen and windows depending on the value of the bool given
    public void Set_Fullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void Set_Resolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
 