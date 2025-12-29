using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Nivel y Experiencia")]
    public int level = 1;
    public float currentXP = 0f;
    public float xpToNextLevel = 100f;

    [Header("Salud del Jugador")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Estadísticas de Combate")]
    public float baseDamage = 10f;

    [Header("Efectos Visuales")]
    public GameObject levelUpEffectPrefab;

    void Start()
    {
        // Al empezar, el jugador tiene la vida llena
        currentHealth = maxHealth;
    }

    public void AddExperience(float amount)
    {
        currentXP += amount;
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentXP -= xpToNextLevel;
        level++;
        xpToNextLevel *= 1.2f;
        baseDamage += 5f;

        // --- MEJORA: Recuperar Vida al subir de nivel ---
        maxHealth += 20f; // Aumentamos su vida máxima
        currentHealth = maxHealth; // Curación completa
        Debug.Log("¡LEVEL UP! Vida restaurada y aumentada a: " + maxHealth);

        if (levelUpEffectPrefab != null)
        {
            GameObject effect = Instantiate(levelUpEffectPrefab, transform.position, Quaternion.identity);
            effect.transform.SetParent(this.transform);
            Destroy(effect, 3f);
        }
    }

    // Función para que el Slime nos haga daño después
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Jugador recibió daño. Vida actual: " + currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("Has muerto");
            // Aquí iría la lógica de Game Over
        }
    }
}