//Room_Trigger.cs
//By: Conor Brennan
//Last Edited: 5/21/2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Trigger : MonoBehaviour
{
    public GameObject virtualCamera;

    //if the player enters a room, that room's vcam is turned on
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            virtualCamera.SetActive(true);
        }
    }

    //if the player enters a room, that room's vcam is turned off
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            virtualCamera.SetActive(false);
        }
    }

}
