using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject enemy;
    public float delayTime = 1f;
    public float repeatRate = 3f;
    public Transform[] spawnPoints;
    private bool playerIsDead = false;

    private void playerDeathAction()
    {
        playerIsDead = true;
    }

    private void OnEnable()
    {
        PlayerHealth.PlayerDeathEvent += playerDeathAction;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerDeathEvent -= playerDeathAction;
    }

    private void Spawn()
    {
        if (playerIsDead)
        {
            CancelInvoke("Spawn");
            return;
        }
        int pointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[pointIndex].position, spawnPoints[pointIndex].rotation);
    }

    // Use this for initialization
    void Start () {
        InvokeRepeating("Spawn", delayTime, repeatRate);
	}
	
}
