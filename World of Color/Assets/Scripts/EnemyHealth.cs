using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyObject enemyObj;

    public int currHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyObj = GetComponent<EnemyObject>();
        currHealth = enemyObj.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currHealth > enemyObj.maxHealth)
        {
            currHealth = enemyObj.maxHealth;
        }

        if(currHealth <= 0)
        {
            //TODO: play death animation
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            currHealth--;
        }
    }
}
