using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    [Header("Configuración")]
    public string npcName = "Comerciante";
    public GameObject shopUI; // Arrastra aquí el Panel de la Tienda que crearemos
    
    private bool isPlayerInRange = false;

    void Update()
    {
        // Si el jugador está cerca y presiona la tecla E
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleShop();
        }
    }

    public void ToggleShop()
    {
        if (shopUI != null)
        {
            bool isActive = !shopUI.activeSelf;
            shopUI.SetActive(isActive);

            // Bloquear/Desbloquear el mouse según si la tienda está abierta
            if (isActive)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Presiona 'E' para hablar con " + npcName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (shopUI != null) shopUI.SetActive(false);
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}