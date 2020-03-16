using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100;
    public Slider healthSlider;
    private static int currentHealth;

    public AudioClip deathClip;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    private bool damaged = false;
    private AudioSource playerAudio;

    private bool isDeath = false;
    private Animator playerAnimator;

    public delegate void PlayerDeathAction();
    public static event PlayerDeathAction PlayerDeathEvent;

	// Use this for initialization
    private void Awake()
    {
        healthSlider.maxValue = startingHealth;

        if (currentHealth <= 0)
        {
            healthSlider.value = startingHealth;
            currentHealth = startingHealth;
        }
        else
        {
            healthSlider.value = startingHealth;
        }
        playerAudio = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Death()
    {
        isDeath = true;
        playerAudio.clip = deathClip;
        playerAudio.Play();
        playerAnimator.SetTrigger("Die");
        GetComponent<PlayerMovement>().enabled = false;
        GetComponentInChildren<PlayerShooting>().enabled = false;

        if (PlayerDeathEvent != null)
        {
            PlayerDeathEvent();
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDeath)
        {
            return;
        }

        playerAudio.Play();
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Update()
    {
        if (damaged)
        {
            damaged = false;
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, Time.deltaTime * flashSpeed);
        }
    }
}
