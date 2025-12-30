using UnityEngine;

public class EnemyHealth : MonoBehaviour 
{ 
    [Header("Estadísticas")] 
    public float maxHealth = 30f; 
    public float xpReward = 50f; 
    private float currentHealth; 
    
    [Header("Botín (Drop)")]
    public GameObject itemToDrop; 

    [Header("Referencia UI")]
    // Esta casilla se llenará sola al darle a Play gracias al script de la barra
    public FloatingHealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        // Si la barra logró vincularse, se actualizará aquí
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
        if (itemToDrop != null)
        {
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
        }

        PlayerStats player = FindFirstObjectByType<PlayerStats>();
        if (player != null)
        {
            player.AddExperience(xpReward);
        }

        if (healthBar != null)
        {
            Destroy(healthBar.gameObject);
        }

        Destroy(gameObject);
    }
}