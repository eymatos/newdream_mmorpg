using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    // Diccionario simple: Nombre del objeto y cantidad
    public Dictionary<string, int> items = new Dictionary<string, int>();

    [Header("Ajustes de Inventario")]
    public int maxSlots = 20;

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
}