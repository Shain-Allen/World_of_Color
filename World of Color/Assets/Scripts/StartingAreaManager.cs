using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StartingAreaManager : MonoBehaviour
{
    public int startingUnpurified;
    public int unpurifiedMonsters;
    public int purifiedMonsters;
    public Material roomMat;
    float saturation;

    private void OnDisable()
    {
        roomMat.SetFloat("Saturation", 1f);
    }

    private void Update()
    {
        saturation = (1f / startingUnpurified) * purifiedMonsters;
        roomMat.SetFloat("Saturation", saturation);
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
