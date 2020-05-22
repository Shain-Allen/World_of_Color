using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StartingAreaManager : MonoBehaviour
{
    public int unpurifiedMonsters;
    public int purifiedMonsters;
    public Material roomMat;
    
    public void UnPurified()
    {
        unpurifiedMonsters += 1;
    }

    public void Purified()
    {
        unpurifiedMonsters -= 1;
        purifiedMonsters += 1;
    }
}
