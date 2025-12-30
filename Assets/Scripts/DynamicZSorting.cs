using UnityEngine;

public class DynamicZSorting : MonoBehaviour
{
    [Header("Configuración de Orden")]
    [Tooltip("Ajuste para que el punto de pivote sea la base de los pies")]
    public int precisionMultiplier = 100;
    public int offset = 0;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Usamos LateUpdate para que se calcule después del movimiento del jugador
    void LateUpdate()
    {
        if (spriteRenderer != null)
        {
            // Cuanto más abajo esté el objeto (menor Y), mayor será su Sorting Order
            // Esto crea el efecto de profundidad 2.5D
            spriteRenderer.sortingOrder = (int)(transform.position.y * -precisionMultiplier) + offset;
        }
    }
}