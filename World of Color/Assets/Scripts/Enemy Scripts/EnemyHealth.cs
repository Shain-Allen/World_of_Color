using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GenericStats myStats;

    public int currHealth;

    public AudioSource takeDamageSound;

    public GameObject RoomManager;

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
            //play purification animation?
            RoomManager.GetComponent<StartingAreaManager>().Purified();
        }
    }

    //reduce health if we take damage
    public void TakeDamage(int damage)
    {
        takeDamageSound.Play();
        currHealth -= damage;
    }
}
