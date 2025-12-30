using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [Header("Referencias UI")]
    public Slider slider;
    public Transform target; 
    
    [Header("Ajustes de Posición")]
    public Vector3 offset = new Vector3(0, 1.2f, 0);

    void Start()
    {
        // 1. Si no hay target, intentamos seguir al padre (el Visual o el Slime)
        if (target == null) target = transform.parent;

        // 2. Buscamos el script de salud en el objeto padre
        EnemyHealth enemy = GetComponentInParent<EnemyHealth>();
        
        if (enemy != null)
        {
            // Nos vinculamos al enemigo
            enemy.healthBar = this;
            
            // Le pedimos sus datos de vida actuales para llenar la barra
            SetMaxHealth(enemy.maxHealth);
            SetHealth(enemy.maxHealth); 
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Mantener la posición sobre el target
        transform.position = target.position + offset;

        // Billboard: Mirar a la cámara
        if (Camera.main != null)
        {
            transform.LookAt(transform.position + Camera.main.transform.forward);
        }
    }

    public void SetMaxHealth(float health)
    {
        if (slider != null)
        {
            slider.maxValue = health;
            slider.value = health;
        }
    }

    public void SetHealth(float health)
    {
        if (slider != null)
        {
            slider.value = health;
        }
    }
}