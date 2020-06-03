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
    public GameObject[] hearts;
    //public Sprite fullHeart;
    //public Sprite EmptyHeart;

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

    public GameObject FadeCanvas;

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
            StartCoroutine(Death());
        }


        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }

    IEnumerator Death()
    {
        FadeCanvas.SetActive(true);
        FadeCanvas.GetComponent<Animator>().SetInteger("fade_direction", 1);
        yield return new WaitForSeconds(0.9f);
        SceneManager.LoadScene("GameOver");
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
