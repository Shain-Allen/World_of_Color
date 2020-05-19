using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    public float speed = 0.05f;
    public int attackDamage = 1;
    public int maxHealth = 3;

    public Rigidbody2D myRb;
    public Animator myAnim;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }
}
