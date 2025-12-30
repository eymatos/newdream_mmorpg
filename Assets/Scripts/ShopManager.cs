using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Referencias")]
    public PlayerStats playerStats;
    public InventoryManager inventory; // Referencia al inventario del jugador

    [Header("Configuración de Precios")]
    public int precioJalea = 10;
    public string nombreItemJalea = "Jalea de Slime"; // Asegúrate de que coincida con el nombre en el inventario

    public void VenderJalea()
    {
        // 1. Verificar si el inventario existe y si tiene el item
        if (inventory != null && inventory.HasItem(nombreItemJalea))
        {
            // 2. Quitar 1 jalea del inventario
            inventory.RemoveItem(nombreItemJalea, 1);

            // 3. Sumar el oro al jugador
            if (playerStats != null)
            {
                playerStats.AddGold(precioJalea);
                Debug.Log("Venta exitosa: +10 Oro");
            }
        }
        else
        {
            Debug.Log("No tienes '" + nombreItemJalea + "' para vender.");
        }
    }
}