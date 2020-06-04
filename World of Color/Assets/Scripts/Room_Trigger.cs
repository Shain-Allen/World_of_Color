//Room_Trigger.cs
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
    float fadeOutTime = 0.001f;

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

        if (inRoom == true)
        {
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
            if(areaManager.purifiedMonsters != 0)
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
            if (areaManager.purifiedMonsters == 0)
            {
                StartCoroutine(Room_Trigger.StartFade(audioMixer, "Colorless", fadeOutTime, 1f));
            }
        }        
    }
        
    public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log(newVol) * 20);
            yield return null;
        }
        yield break;
    }

}
