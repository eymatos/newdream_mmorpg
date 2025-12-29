using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform target; // Arrastra aquí a tu jugador (Character)

    [Header("Configuración")]
    public Vector3 offset = new Vector3(0, 10f, -10f); // Ajusta esto para la distancia
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Posición a la que queremos llegar
        Vector3 desiredPosition = target.position + offset;
        
        // Movimiento suave
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        
        transform.position = smoothedPosition;

        // La cámara siempre mira al jugador
        transform.LookAt(target);
    }
}