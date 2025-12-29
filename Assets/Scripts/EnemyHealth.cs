using UnityEngine;

public class EnemyHealth : MonoBehaviour 
{ 
    [Header("Estadísticas")] 
    public float maxHealth = 30f; 
    public float xpReward = 50f; 
    private float currentHealth; 
    
    [Header("Botín (Drop)")]
    public GameObject itemToDrop; // Arrastra aquí el Prefab del Drop_Jalea

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        Debug.Log(gameObject.name + " recibió daño. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("¡El enemigo ha muerto!");

        // 1. Soltar el objeto
        if (itemToDrop != null)
        {
            // Instanciamos el item en la posición donde murió el slime
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
        }

        // 2. Dar experiencia
        PlayerStats player = FindFirstObjectByType<PlayerStats>();
        if (player != null)
        {
            player.AddExperience(xpReward);
        }

        Destroy(gameObject);
    }
}