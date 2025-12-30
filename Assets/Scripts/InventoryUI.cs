using UnityEngine;
using UnityEngine.UI; // Necesario para el componente Button
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public InventoryManager inventory;
    public GameObject inventoryPanel;
    public TextMeshProUGUI itemTextPrefab;
    public Transform itemListParent;

    private bool isOpen = false;

    void Start()
    {
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
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

    public void RefreshInventory()
    {
        // 1. Limpiamos la lista vieja
        foreach (Transform child in itemListParent)
        {
            Destroy(child.gameObject);
        }

        // 2. Creamos un elemento por cada objeto que tengamos
        foreach (KeyValuePair<string, int> item in inventory.items)
        {
            // Instanciamos el texto
            TextMeshProUGUI newText = Instantiate(itemTextPrefab, itemListParent);
            newText.text = item.Key + " x" + item.Value;

            // --- MEJORA: Añadir interactividad ---
            // Buscamos o añadimos un componente Button al texto instanciado
            Button btn = newText.gameObject.GetComponent<Button>();
            if (btn == null)
            {
                btn = newText.gameObject.AddComponent<Button>();
            }

            // Guardamos el nombre del item para el evento
            string nameToUse = item.Key;

            // Configuramos el clic del botón
            btn.onClick.AddListener(() => {
                inventory.UseItem(nameToUse);
                RefreshInventory(); // Refrescamos para ver la nueva cantidad
            });
        }
    }
}