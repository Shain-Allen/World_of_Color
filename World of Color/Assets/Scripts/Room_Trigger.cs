//Room_Trigger.cs
//By: Conor Brennan
//Last Edited: 5/20/2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Trigger : MonoBehaviour
{
    public GameObject virtualCamera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            virtualCamera.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            virtualCamera.SetActive(false);
        }
    }

}
