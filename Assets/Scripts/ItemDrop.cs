using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [Header("Configuración del Item")]
    public string itemName = "Jalea de Slime";
    public int amount = 1;
    public int experienceBonus = 10;

    [Header("Efecto de Consumible")]
    public bool isConsumable = true;
    public float healthRegen = 20f; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Intentamos obtener el Inventario del jugador
            InventoryManager inventory = other.GetComponent<InventoryManager>();
            if (inventory != null)
            {
                inventory.AddItem(itemName, amount);
            }

            // 2. Mantenemos el bonus de experiencia
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.AddExperience(experienceBonus);
                Debug.Log("Recogiste: " + itemName + " y ganaste XP!");
            }

            Destroy(gameObject);
        }
    }
}