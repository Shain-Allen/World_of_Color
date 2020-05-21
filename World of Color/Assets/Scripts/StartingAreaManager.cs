using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingAreaManager : MonoBehaviour
{
    public int unpurifiedMonsters;
    public int purifiedMonsters;
    public BoxCollider2D roomArea;
    public ContactFilter2D enemyies;

    private void Awake()
    {
        Collider2D[] result = null;
        unpurifiedMonsters = Physics2D.OverlapCollider(roomArea, enemyies, result);
    }

    public void Purified()
    {
        unpurifiedMonsters -= 1;
        purifiedMonsters += 1;
    }
}
