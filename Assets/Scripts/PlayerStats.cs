using UnityEngine;
using TMPro; // Necesario para el texto del maná y el oro

public class PlayerStats : MonoBehaviour
{
    [Header("Referencias de UI")]
    public HealthBar healthBar; // Arrastra aquí la barra roja
    public HealthBar manaBar;   // Arrastra aquí la barra azul
    public TextMeshProUGUI manaText; // Arrastra aquí el texto de maná (SP: 100/100)
    public TextMeshProUGUI goldText; // Arrastra aquí el texto que mostrará el oro

    [Header("Nivel y Experiencia")]
    public int level = 1;
    public float currentXP = 0f;
    public float xpToNextLevel = 100f;

    [Header("Salud del Jugador")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Maná del Jugador")]
    public float maxMana = 50f;
    public float currentMana;
    public float manaRegenRate = 2f; // Cantidad de maná recuperado por segundo

    [Header("Economía")]
    public int gold = 0; // Cantidad actual de monedas

    [Header("Estadísticas de Combate")]
    public float baseDamage = 10f;

    [Header("Efectos Visuales")]
    public GameObject levelUpEffectPrefab;

    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;

        // Inicializar barras visuales
        if (healthBar != null) healthBar.SetMaxHealth(maxHealth);
        if (manaBar != null) manaBar.SetMaxHealth(maxMana);
        
        UpdateManaUI();
        UpdateGoldUI(); // Inicializar el texto del oro
    }

    void Update()
    {
        // --- NUEVA LÓGICA: Regeneración de Maná Pasiva ---
        if (currentMana < maxMana)
        {
            currentMana += manaRegenRate * Time.deltaTime;
            
            // Asegurarnos de no sobrepasar el máximo
            if (currentMana > maxMana) currentMana = maxMana;

            // Actualizar la barra azul y el texto en tiempo real
            if (manaBar != null) manaBar.SetHealth(currentMana);
            UpdateManaUI();
        }
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

        maxHealth += 20f; 
        maxMana += 10f; 
        currentHealth = maxHealth; 
        currentMana = maxMana; 

        // Actualizar máximos en la UI
        if (healthBar != null) healthBar.SetMaxHealth(maxHealth);
        if (manaBar != null) manaBar.SetMaxHealth(maxMana);
        UpdateManaUI();

        Debug.Log("¡LEVEL UP! Stats aumentados.");

        if (levelUpEffectPrefab != null)
        {
            GameObject effect = Instantiate(levelUpEffectPrefab, transform.position, Quaternion.identity);
            effect.transform.SetParent(this.transform);
            Destroy(effect, 3f);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if (healthBar != null) healthBar.SetHealth(currentHealth);

        Debug.Log("Jugador recibió daño. Vida actual: " + currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("Has muerto");
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        if (healthBar != null) healthBar.SetHealth(currentHealth);

        Debug.Log("Curación: " + currentHealth);
    }

    public bool UseMana(float amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;

            if (manaBar != null) manaBar.SetHealth(currentMana);
            UpdateManaUI();

            Debug.Log("Maná restante: " + currentMana);
            return true;
        }
        else
        {
            Debug.Log("¡No tienes suficiente maná!");
            return false;
        }
    }

    // --- NUEVA LÓGICA: Gestión de Oro ---
    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldUI();
        Debug.Log("Oro ganado: " + amount + ". Total: " + gold);
    }

    public bool RemoveGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            UpdateGoldUI();
            return true;
        }
        return false;
    }

    // Función auxiliar para mantener el texto actualizado
    void UpdateManaUI()
    {
        if (manaText != null)
        {
            manaText.text = "SP: " + Mathf.FloorToInt(currentMana) + " / " + Mathf.FloorToInt(maxMana);
        }
    }

    void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = "Oro: " + gold;
        }
    }
}