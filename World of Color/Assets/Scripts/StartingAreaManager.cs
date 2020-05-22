using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingAreaManager : MonoBehaviour
{
    public int unpurifiedMonsters;
    public int purifiedMonsters;
    
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
