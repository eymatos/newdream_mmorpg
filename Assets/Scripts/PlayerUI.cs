using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [Header("Referencias de Player")]
    public PlayerStats playerStats;

    [Header("Elementos de XP")]
    public Slider xpSlider;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;

    [Header("Elementos de Vida")]
    public Slider healthSlider; // NUEVA REFERENCIA
    public TextMeshProUGUI healthText; // OPCIONAL: Para ver números de vida

    void Update()
    {
        if (playerStats != null)
        {
            UpdateXPUI();
            UpdateHealthUI();
        }
    }

    void UpdateXPUI()
    {
        levelText.text = "Lv. " + playerStats.level;
        xpSlider.maxValue = playerStats.xpToNextLevel;
        xpSlider.value = playerStats.currentXP;
        xpText.text = string.Format("{0} / {1}", (int)playerStats.currentXP, (int)playerStats.xpToNextLevel);
    }

    void UpdateHealthUI()
    {
        // Actualizamos la barra de vida roja
        healthSlider.maxValue = playerStats.maxHealth;
        healthSlider.value = playerStats.currentHealth;

        // Si creaste un texto para la vida, lo actualizamos aquí
        if (healthText != null)
        {
            healthText.text = string.Format("HP: {0} / {1}", (int)playerStats.currentHealth, (int)playerStats.maxHealth);
        }
    }
}