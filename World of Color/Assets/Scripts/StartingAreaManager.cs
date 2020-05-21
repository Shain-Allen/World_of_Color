using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingAreaManager : MonoBehaviour
{
    public int unpurifiedMonsters;
    public int purifiedMonsters;
    public BoxCollider2D roomArea;
    public ContactFilter2D enemies;

    private void Awake()
    {
        Collider2D[] result = new Collider2D[50];
        unpurifiedMonsters = Physics2D.OverlapCollider(roomArea, enemies, result);
    }

    public void Purified()
    {
        unpurifiedMonsters -= 1;
        purifiedMonsters += 1;
    }
}
