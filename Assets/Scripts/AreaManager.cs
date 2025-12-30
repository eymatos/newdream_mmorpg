using UnityEngine;
using TMPro;

public class AreaManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI areaNameText; // Texto que aparecerá en pantalla
    public GameObject areaPanel;         // El panel que contiene el texto

    [Header("Configuración")]
    public float displayDuration = 3f;   // Cuánto tiempo se muestra el nombre

    void Start()
    {
        if (areaPanel != null) areaPanel.SetActive(false);
    }

    // Esta función la llamaremos cuando el jugador entre en un trigger de zona
    public void ShowAreaName(string name)
    {
        if (areaNameText != null && areaPanel != null)
        {
            areaNameText.text = name;
            areaPanel.SetActive(true);
            
            // Cancelamos cualquier ocultación previa y programamos la nueva
            CancelInvoke("HideArea");
            Invoke("HideArea", displayDuration);
        }
    }

    void HideArea()
    {
        if (areaPanel != null) areaPanel.SetActive(false);
    }
}