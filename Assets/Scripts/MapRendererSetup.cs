using UnityEngine;
using UnityEngine.Tilemaps;

public class MapRendererSetup : MonoBehaviour
{
    [Header("Configuración de Capas")]
    public int ordenEnCapa = 0; 

    void Awake()
    {
        TilemapRenderer renderer = GetComponent<TilemapRenderer>();
        if (renderer != null)
        {
            renderer.sortingOrder = ordenEnCapa;
        }
    }
}