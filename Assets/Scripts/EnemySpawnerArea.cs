using UnityEngine;

public class EnemySpawnerArea : MonoBehaviour
{
    [Header("Configuración de Spawn")]
    public GameObject enemyPrefab;    // El prefab del Slime que ya tienes listo
    public int maxEnemies = 5;        // Máximo de enemigos en esta zona
    public float spawnInterval = 5f;  // Cada cuántos segundos intenta spawnear

    private BoxCollider spawnArea;
    private int currentEnemyCount = 0;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider>();
        // Empezamos a spawnear repetidamente
        InvokeRepeating("TrySpawn", 2f, spawnInterval);
    }

    void TrySpawn()
    {
        if (currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // Calculamos una posición aleatoria dentro del Box Collider
        Vector3 randomPos = new Vector3(
            Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            transform.position.y, // Mantenemos la altura del spawner
            Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
        );

        GameObject newEnemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        currentEnemyCount++;

        // Importante: Necesitamos saber cuándo muere para restar el contador
        // Para esto, el enemigo debe avisar al morir.
    }

    // Función que llamará el Slime al morir (opcional por ahora)
    public void EnemyDied()
    {
        currentEnemyCount--;
    }
}