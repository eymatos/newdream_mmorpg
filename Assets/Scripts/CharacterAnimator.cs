using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private PlayerController playerController;
    private Vector3 originalScale;
    
    [Header("Ajustes de Animación")]
    public float idleSpeed = 2f;
    public float walkSpeed = 10f;
    public float bounceAmount = 0.1f;

    void Start()
    {
        // Buscamos el controlador en el objeto padre (el Cubo)
        playerController = GetComponentInParent<PlayerController>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        float currentSpeed = (playerController.currentState == PlayerController.CharacterState.Moving) ? walkSpeed : idleSpeed;
        
        // Calculamos el rebote usando una onda Senoidal
        float bounce = Mathf.Sin(Time.time * currentSpeed) * bounceAmount;
        
        // Aplicamos el cambio de escala para simular animación
        transform.localScale = new Vector3(originalScale.x, originalScale.y + bounce, originalScale.z);
    }
}