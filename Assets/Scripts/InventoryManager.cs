using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    // Diccionario simple: Nombre del objeto y cantidad
    public Dictionary<string, int> items = new Dictionary<string, int>();

    [Header("Ajustes de Inventario")]
    public int maxSlots = 20;

    private PlayerStats playerStats;

    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    // Función para añadir objetos
    public void AddItem(string itemName, int amount)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName] += amount;
        }
        else
        {
            if (items.Count < maxSlots)
            {
                items.Add(itemName, amount);
            }
        }
        
        Debug.Log("Inventario: " + itemName + " x" + (items.ContainsKey(itemName) ? items[itemName] : 0));
    }

    // --- FUNCIÓN: Usar Objeto ---
    public void UseItem(string itemName)
    {
        if (items.ContainsKey(itemName) && items[itemName] > 0)
        {
            if (itemName == "Jalea de Slime")
            {
                playerStats.Heal(20f); 
                Debug.Log("Has usado una Jalea de Slime.");
            }

            // Restamos uno de la cantidad usando nuestra nueva función interna
            RemoveItem(itemName, 1);
        }
        else
        {
            Debug.Log("No tienes suficientes " + itemName);
        }
    }

    // --- NUEVA FUNCIÓN PARA LA TIENDA: Verificar si existe el item ---
    public bool HasItem(string itemName)
    {
        return items.ContainsKey(itemName) && items[itemName] > 0;
    }

    // --- NUEVA FUNCIÓN PARA LA TIENDA: Quitar cantidad específica ---
    public void RemoveItem(string itemName, int amount)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName] -= amount;

            if (items[itemName] <= 0)
            {
                items.Remove(itemName);
            }
            Debug.Log("Inventario actualizado: " + itemName + " restante: " + (items.ContainsKey(itemName) ? items[itemName] : 0));
        }
    }
}