using UnityEngine;

public class PlayerCombat : MonoBehaviour 
{ 
    [Header("Ajustes de Ataque Básico")] 
    public float attackRange = 2f; 
    public float attackDamage = 10f; 
    public LayerMask enemyLayer;

    [Header("Habilidad Especial (Área)")]
    public float specialRange = 5f;
    public float specialDamage = 25f;
    public float manaCost = 20f;
    
    [Header("Efectos Visuales")]
    public GameObject areaAttackEffect; // Aquí arrastraremos el efecto luego

    private PlayerStats stats;

    void Start()
    {
        // Importante: Buscamos las estadísticas en el mismo objeto
        stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        // Ataque básico (Espacio)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        // Habilidad Especial (E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Verificamos si tenemos el componente y si hay maná suficiente
            if (stats != null)
            {
                if (stats.UseMana(manaCost))
                {
                    SpecialAreaAttack();
                }
                else
                {
                    Debug.Log("No tienes maná suficiente para el ataque de área.");
                }
            }
        }
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(attackDamage);
            }
        }
    }

    void SpecialAreaAttack()
    {
        Debug.Log("¡Ejecutando Golpe de Área!");

        // 1. Instanciar efecto visual si existe
        if (areaAttackEffect != null)
        {
            GameObject effect = Instantiate(areaAttackEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f); // Se destruye tras 2 segundos
        }
        
        // 2. Daño en área
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, specialRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(specialDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, specialRange);
    }
}