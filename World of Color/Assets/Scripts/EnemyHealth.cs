using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GenericStats myStats;

    public int currHealth;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = myStats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currHealth > myStats.maxHealth)
        {
            currHealth = myStats.maxHealth;
        }

        if(currHealth <= 0)
        {
            //TODO: play death animation
            Destroy(gameObject, 3.0f);
        }
    }

    //reduce health if we take damage
    void TakeDamage(int damage)
    {
        currHealth -= damage;
    }
}
