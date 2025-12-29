using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject enemyPrefab;    // Arrastra aquí el Prefab del Slime
    public float respawnTime = 5f;    // Segundos para que reaparezca
    
    private GameObject currentEnemy;
    private Vector3 spawnPosition;

    void Start()
    {
        spawnPosition = transform.position;
        SpawnEnemy();
    }

    void Update()
    {
        // Si no hay enemigo vivo y no estamos ya esperando un respawn
        if (currentEnemy == null)
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    void SpawnEnemy()
    {
        currentEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemy.name = "Enemy_Slime_Respawned";
    }

    IEnumerator RespawnRoutine()
    {
        // Creamos un objeto temporal para no disparar mil corrutinas
        GameObject placeholder = new GameObject("WaitingForRespawn");
        currentEnemy = placeholder; 

        yield return new WaitForSeconds(respawnTime);

        Destroy(placeholder);
        SpawnEnemy();
    }
}