//House_Teleport.cs
//By: Conor Brennan
//Last Edited: 5/28/2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House_Teleport : MonoBehaviour
{
    public GameObject teleportLocation;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.transform.position = teleportLocation.transform.position;
        }
    }
}
