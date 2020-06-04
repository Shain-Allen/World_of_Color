using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;

public class StartingAreaManager : MonoBehaviour
{
    public int startingUnpurified;
    public int unpurifiedMonsters;
    public int purifiedMonsters;
    public Material roomMat;
    float saturation;

    //lights
    public Light2D pointlight;
    public Room_Trigger room;

    private void OnDisable()
    {
        roomMat.SetFloat("Saturation", 1f);
    }

    private void Update()
    {
        if (startingUnpurified != 0)
        {
            saturation = (1f / startingUnpurified) * purifiedMonsters;
            roomMat.SetFloat("Saturation", saturation);

            if (room.inRoom)
            {
                pointlight.pointLightOuterRadius = (unpurifiedMonsters / startingUnpurified) * (34.75f - 70f) + 70f;
            }
        }
    }

    public void UnPurified()
    {
        unpurifiedMonsters += 1;
        startingUnpurified += 1;
    }

    public void Purified()
    {
        unpurifiedMonsters -= 1;
        purifiedMonsters += 1;
    }

    
}
