using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Referencias UI")]
    public Slider slider;
    public Transform target;
    
    [Header("Ajustes de Posición")]
    // Esta es la línea que permite que la barra flote sobre la cabeza
    public Vector3 offset = new Vector3(0, 2f, 0);

    void LateUpdate()
    {
        // 1. Hace que la barra siga al objetivo (el Slime)
        if (target != null)
        {
            transform.position = target.position + offset;
        }

        // 2. Hace que la barra siempre mire a la cámara (Billboard)
        if (Camera.main != null)
        {
            transform.LookAt(transform.position + Camera.main.transform.forward);
        }
    }

    // Configura la vida máxima al inicio
    public void SetMaxHealth(float health)
    {
        if (slider != null)
        {
            slider.maxValue = health;
            slider.value = health;
        }
    }

    // Actualiza el valor de la barra cuando el enemigo recibe daño
    public void SetHealth(float health)
    {
        if (slider != null)
        {
            slider.value = health;
        }
    }
}