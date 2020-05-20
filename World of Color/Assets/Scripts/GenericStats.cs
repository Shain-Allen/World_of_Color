using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GenericStats", menuName = "ScriptableObjects/Generic Stat")]
public class GenericStats : ScriptableObject
{
    public int maxHealth;
    public float movementSpeed;
    public float attackRange;
    public float attackCooldown;
    public int attackDamage;
}
