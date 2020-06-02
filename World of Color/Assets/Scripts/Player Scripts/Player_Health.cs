using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour
{
    //health UI 
    public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite EmptyHeart;

    //player graidiant Control
    public Material PlayerMat;
    float distance;

    //sounds
    public PlayerSounds mySounds;
    public AudioSource audioSource;

    //shield
    public Player_Shield myShield;
    public Player_Combat myAttack;
    public Vector2[] shieldDirections = { Vector2.down, Vector2.up, Vector2.left, Vector2.right };
    public Animator myAnim;

    private void Start()
    {
        PlayerMat.SetFloat("Distance", 1f);
    }

    private void OnDisable()
    {
        PlayerMat.SetFloat("Distance", 1f);
    }

    private void Update()
    {
        HealthUIController();
        PlayerMatController();
    }

    void HealthUIController()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        if (numOfHearts > hearts.Length)
        {
            numOfHearts = hearts.Length;
        }

        if(health <= 0)
        {
            //TODO: fade into new scene
            SceneManager.LoadScene("GameOver");
        }


        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = EmptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    void PlayerMatController()
    {
        distance = (1f / numOfHearts) * health;
        PlayerMat.SetFloat("Distance", distance);
    }

    public void TakeDamage(int damage, Vector2 attackDirection)
    {
        if (attackDirection == -shieldDirections[(int)myAttack.attackdir] && !myShield.isBroken && !myAnim.GetBool("IsMoving"))
        {
            myShield.BlockAttack();
        }
        else
        {
            health -= damage;
            audioSource.PlayOneShot(mySounds.player_takeDamage);
            if (health <= 3)
            {
                audioSource.PlayOneShot(mySounds.player_lowHealth);
            }
        }
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
    }
}
