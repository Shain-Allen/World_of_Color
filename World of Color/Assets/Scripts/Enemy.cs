using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public float speed = 0.1f;
    public int damage = 1;
    public int health = 3;
}
