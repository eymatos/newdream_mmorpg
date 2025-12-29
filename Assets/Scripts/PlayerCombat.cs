using UnityEngine;

public class PlayerCombat : MonoBehaviour 
{ 
    [Header("Ajustes de Ataque")] 
    public float attackRange = 2f; 
    public float attackDamage = 10f; 
    public LayerMask enemyLayer;

    void Update()
    {
        // Detectamos cuando presionas la tecla Espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        // 1. Detectamos todos los objetos en la capa "Enemy" dentro del rango
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        // 2. Por cada enemigo encontrado, le restamos vida
        foreach (Collider enemy in hitEnemies)
        {
            // Buscamos el componente EnemyHealth en el objeto que golpeamos
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            
            if (health != null)
            {
                health.TakeDamage(attackDamage);
                Debug.Log("¡Golpeaste a " + enemy.name + "!");
            }
        }
    }

    // Esto dibuja una esfera roja en la escena para que veas el alcance del golpe
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}