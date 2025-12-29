using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float moveSpeed = 2f;
    public float detectionRange = 10f;
    public float attackRange = 1.5f;

    [Header("Configuración de Ataque")]
    public float damage = 10f;
    public float attackRate = 1.5f; // Tiempo entre ataques
    private float nextAttackTime = 0f;

    private Transform player;
    private PlayerStats playerStats;

    void Start()
    {
        // Buscamos al jugador por su Tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerStats = playerObj.GetComponent<PlayerStats>();
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // 1. Si está en rango de detección pero lejos para atacar, lo sigue
        if (distance <= detectionRange && distance > attackRange)
        {
            FollowPlayer();
        }
        // 2. Si está lo suficientemente cerca, ataca
        else if (distance <= attackRange)
        {
            if (Time.time >= nextAttackTime)
            {
                AttackPlayer();
                nextAttackTime = Time.time + attackRate;
            }
        }
    }

    void FollowPlayer()
    {
        // Moverse hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Mirar hacia el jugador (opcional para que el sprite rote)
        if (direction != Vector3.zero)
        {
            transform.forward = new Vector3(direction.x, 0, direction.z);
        }
    }

    void AttackPlayer()
    {
        if (playerStats != null)
        {
            Debug.Log("¡El Slime te ha golpeado!");
            playerStats.TakeDamage(damage);
        }
    }

    // Para ver los rangos en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}