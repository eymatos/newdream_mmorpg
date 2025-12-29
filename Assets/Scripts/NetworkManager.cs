using UnityEngine;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;

    [Header("Configuración de Seguridad")]
    public float maxServerSpeed = 7f; // El servidor no permite más de esta velocidad
    public float antiBotThreshold = 0.5f; // Margen de error para lag

    void Awake()
    {
        // Esto permite que el NetworkManager sea accesible desde cualquier lugar
        if (Instance == null) Instance = this;
    }

    // Esta función simula la validación del servidor
    public bool ValidateMove(Vector3 currentPos, Vector3 targetPos, float deltaTime)
    {
        float distance = Vector3.Distance(currentPos, targetPos);
        float speedAchieved = distance / deltaTime;

        // SEGURIDAD ANTI-BOT:
        // Si la velocidad del jugador supera el límite permitido por el servidor, bloqueamos el movimiento.
        if (speedAchieved > maxServerSpeed + antiBotThreshold)
        {
            Debug.LogWarning("¡ALERTA ANTI-BOT! Movimiento ilegal detectado. Velocidad: " + speedAchieved);
            return false;
        }

        return true;
    }
}