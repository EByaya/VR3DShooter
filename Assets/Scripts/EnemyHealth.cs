using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    public int startHealth = 100;
    public int score = 10;
    private int currentHealth;
    private Animator anim;
    private bool isDead;

    private bool isSinking = false;

    public AudioClip deadClip;
    private AudioSource enemyAudio;
    private ParticleSystem hitParticles;

	private void Awake ()
    {
        anim = GetComponent<Animator>();
        currentHealth = startHealth;
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
	}

    private void Death()
    {
        isDead = true;
        anim.SetTrigger("isDead");
        enemyAudio.clip = deadClip;
        enemyAudio.Play();
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
        ScoreManager.score += score;
    }

    public void TakeDamege(int amount, Vector3 position)
    {
        if (isDead)
        {
            return;
        }
        currentHealth -= amount;
        enemyAudio.Play();
        hitParticles.transform.position = position;
        hitParticles.Play();


        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void StartSinking()
    {
        isSinking = true;
        Destroy(gameObject, 2f);
    }

	// Update is called once per frame
	void Update () {
        if (isSinking)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }
	}
}
