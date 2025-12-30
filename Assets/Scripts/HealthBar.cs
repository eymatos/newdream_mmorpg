using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Referencias UI")]
    public Slider slider;

    // Se eliminó la lógica de LateUpdate para que la barra permanezca fija en el Canvas del HUD.

    // Configura el valor máximo (Vida o Maná) al inicio
    public void SetMaxHealth(float amount)
    {
        if (slider != null)
        {
            slider.maxValue = amount;
            slider.value = amount;
        }
    }

    // Actualiza el valor actual (Vida o Maná)
    public void SetHealth(float amount)
    {
        if (slider != null)
        {
            slider.value = amount;
        }
    }
}