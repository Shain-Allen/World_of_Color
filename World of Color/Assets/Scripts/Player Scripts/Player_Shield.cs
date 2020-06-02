using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player_Shield : MonoBehaviour
{
    public int currDurability = 3;
    public int maxDurability = 3;

    public float currCooldownTime = 0.0f;
    public float maxCooldownTime = 15.0f;

    public bool isBroken = false;

    //sounds
    public PlayerSounds mySounds;
    public AudioSource audioSource;

    //UI
    public Image shieldBar;
    public Material shieldBarMat;

    // Start is called before the first frame update
    void Start()
    {
        shieldBarMat.SetFloat("Saturation", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(currDurability <= 0)
        {
            shieldBarMat.SetFloat("Saturation", 0.0f);
            isBroken = true;
        }

        if (isBroken)
        {
            currCooldownTime += Time.deltaTime;

            shieldBar.fillAmount = (1f / maxCooldownTime) * currCooldownTime;

            if (currCooldownTime >= maxCooldownTime)
            {
                isBroken = false;
                currCooldownTime = 0;
                currDurability = maxDurability;
                shieldBar.fillAmount = 1.0f;
                shieldBarMat.SetFloat("Saturation", 1.0f);
            }
        }
        else
        {
            shieldBar.fillAmount = (1f / maxDurability) * currDurability;
        }
    }

    public void BlockAttack()
    {
        audioSource.PlayOneShot(mySounds.player_shield);
        currDurability--;
    }
}
