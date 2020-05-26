using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shield : MonoBehaviour
{
    public int currDurability = 10;
    public int maxDurability = 10;

    public float currCooldownTime = 0.0f;
    public float maxCooldownTime = 5.0f;

    public bool isBroken = false;

    public AudioSource shieldSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currDurability <= 0)
        {
            isBroken = true;
        }

        if(isBroken)
        {
            currCooldownTime += Time.deltaTime;

            if(currCooldownTime >= maxCooldownTime)
            {
                isBroken = false;
                currCooldownTime = 0;
                currDurability = maxDurability;
            }
        }
    }

    public void BlockAttack()
    {
        shieldSound.Play();
        currDurability--;
    }
}
