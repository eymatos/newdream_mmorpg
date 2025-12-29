using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public InventoryManager inventory; // Referencia al inventario del player
    public GameObject inventoryPanel;   // El panel que creamos arriba
    public TextMeshProUGUI itemTextPrefab; // Un prefab de texto para cada item
    public Transform itemListParent;    // El objeto Item_List

    private bool isOpen = false;

    void Start()
    {
        inventoryPanel.SetActive(false); // Empezamos con el inventario cerrado
    }

    void Update()
    {
        // Abrir/Cerrar con la tecla I
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);

        if (isOpen)
        {
            RefreshInventory();
        }
    }

    void RefreshInventory()
    {
        // 1. Limpiamos la lista vieja
        foreach (Transform child in itemListParent)
        {
            Destroy(child.gameObject);
        }

        // 2. Creamos un texto por cada objeto que tengamos
        foreach (KeyValuePair<string, int> item in inventory.items)
        {
            TextMeshProUGUI newText = Instantiate(itemTextPrefab, itemListParent);
            newText.text = item.Key + " x" + item.Value;
        }
    }
}