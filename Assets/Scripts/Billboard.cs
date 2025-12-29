using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera != null)
        {
            // Hace que el sprite siempre mire hacia la cámara
            transform.forward = mainCamera.transform.forward;
        }
    }
}