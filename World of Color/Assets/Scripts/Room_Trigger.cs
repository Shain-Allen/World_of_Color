﻿//Room_Trigger.cs
//By: Conor Brennan
//Last Edited: 5/21/2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using Unity.Mathematics;
using System;

public class Room_Trigger : MonoBehaviour
{
    public GameObject virtualCamera;
    public int roomNumber;

    public AudioMixer audioMixer;
    public StartingAreaManager areaManager;
    public int[] enemiesToAudioTracks = new int[5];
    public bool inRoom = false;
    float fadeInTime = 1f;
    float fadeOutTime = 0.000000000000000001f;

    public bool CreditsRoom = false;

    //if the player enters a room, that room's vcam is turned on
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            virtualCamera.SetActive(true);
            Console.WriteLine("Muted");
            inRoom = true;

            collision.gameObject.GetComponent<Player_Controller>().currRoom = roomNumber;
        }
    }

    //if the player enters a room, that room's vcam is turned off
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            virtualCamera.SetActive(false);
            inRoom = false;
        }
    }
    
    private void Update()
    {
        //unmute audio in this order

        //1. Bass1
        //2. Harmony
        //3. Drums
        //4. Bass2
        //5. MelodicLine

        if (inRoom == true && CreditsRoom == false)
        {
            StartCoroutine(Room_Trigger.StartFade(audioMixer, "Credits", fadeOutTime, 0f));


            if (areaManager.purifiedMonsters >= enemiesToAudioTracks[0])
            {
                Console.WriteLine("unmuted");
                StartCoroutine(Room_Trigger.StartFade(audioMixer, "Bass1", fadeInTime, 1f));
            }
            if (areaManager.purifiedMonsters >= enemiesToAudioTracks[1])
            {
                StartCoroutine(Room_Trigger.StartFade(audioMixer, "Harmony", fadeInTime, 1f));
            }
            if (areaManager.purifiedMonsters >= enemiesToAudioTracks[2])
            {
                StartCoroutine(Room_Trigger.StartFade(audioMixer, "Drums", fadeInTime, 1f));
            }
            if (areaManager.purifiedMonsters >= enemiesToAudioTracks[3])
            {
                StartCoroutine(Room_Trigger.StartFade(audioMixer, "Bass2", fadeInTime, 1f));
            }
            if (areaManager.purifiedMonsters >= enemiesToAudioTracks[4])
            {
                StartCoroutine(Room_Trigger.StartFade(audioMixer, "MelodicLine", fadeInTime, 1f));
            }
            if(areaManager.purifiedMonsters != 0 || areaManager.startingUnpurified == 0)
            {
                StartCoroutine(Room_Trigger.StartFade(audioMixer, "Colorless", fadeInTime, 0f));
            }

            if (areaManager.purifiedMonsters <= enemiesToAudioTracks[0])
            {
                StartCoroutine(Room_Trigger.StartFade(audioMixer, "Bass1", fadeOutTime, 0f));
            }
            if (areaManager.purifiedMonsters <= enemiesToAudioTracks[1])
            {
                StartCoroutine(Room_Trigger.StartFade(audioMixer, "Harmony", fadeOutTime, 0f));
            }
            if (areaManager.purifiedMonsters <= enemiesToAudioTracks[2])
            {
                StartCoroutine(Room_Trigger.StartFade(audioMixer, "Drums", fadeOutTime, 0f));
            }
            if (areaManager.purifiedMonsters <= enemiesToAudioTracks[3])
            {
                StartCoroutine(Room_Trigger.StartFade(audioMixer, "Bass2", fadeOutTime, 0f));
            }
            if (areaManager.purifiedMonsters <= enemiesToAudioTracks[4])
            {
                StartCoroutine(Room_Trigger.StartFade(audioMixer, "MelodicLine", fadeOutTime, 0f));
            }
            if (areaManager.purifiedMonsters == 0 && areaManager.startingUnpurified != 0)
            {
                StartCoroutine(Room_Trigger.StartFade(audioMixer, "Colorless", fadeOutTime, 1f));
            }
        }

        if (inRoom == true && CreditsRoom == true)
        {
            StartCoroutine(Room_Trigger.StartFade(audioMixer, "Bass1", fadeOutTime, 0f));
            StartCoroutine(Room_Trigger.StartFade(audioMixer, "Harmony", fadeOutTime, 0f));
            StartCoroutine(Room_Trigger.StartFade(audioMixer, "Drums", fadeOutTime, 0f));
            StartCoroutine(Room_Trigger.StartFade(audioMixer, "Bass2", fadeOutTime, 0f));
            StartCoroutine(Room_Trigger.StartFade(audioMixer, "MelodicLine", fadeOutTime, 0f));
            StartCoroutine(Room_Trigger.StartFade(audioMixer, "Colorless", fadeOutTime, 0f));
            StartCoroutine(Room_Trigger.StartFade(audioMixer, "Credits", fadeInTime, 1f));
        }
    }
        
    public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
    // Initialize variables
    float currentTime = 0;
    float currentVol;
    
    // Get the current volume level of the exposed parameter and convert it from decibels to a linear scale
    audioMixer.GetFloat(exposedParam, out currentVol);
    currentVol = Mathf.Pow(10, currentVol / 20);
    
    // Clamp the target volume level to ensure it stays within a valid range
    float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

    // Loop until the fade is complete
    while (currentTime < duration)
    {
        // Increase the current time by the time since the last frame
        currentTime += Time.deltaTime;
        
        // Calculate the new volume level by interpolating between the current and target values over the duration of the fade
        float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
        
        // Convert the new volume level back to decibels and set it as the new value for the exposed parameter
        audioMixer.SetFloat(exposedParam, Mathf.Log(newVol) * 20);
        
        // Pause briefly before the next iteration of the loop
        yield return null;
    }
}

}
