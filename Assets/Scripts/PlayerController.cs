using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float moveSpeed = 6f;
    public float rotationSpeed = 15f;
    
    [Header("Estado del Jugador")]
    public CharacterState currentState = CharacterState.Idle;

    private CharacterController controller;
    private Vector3 moveInput;
    private Vector3 velocity;

    public enum CharacterState { Idle, Moving, Attacking, Stunned }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveInput = new Vector3(horizontal, 0f, vertical).normalized;

        if (moveInput.magnitude >= 0.1f)
        {
            // --- SEGURIDAD AUTORITATIVA ---
            // Antes de movernos, calculamos a dónde queremos ir
            Vector3 targetPosition = transform.position + (moveInput * moveSpeed * Time.deltaTime);

            // Le pedimos permiso al NetworkManager (El Servidor)
            if (NetworkManager.Instance.ValidateMove(transform.position, targetPosition, Time.deltaTime))
            {
                MovePlayer();
                currentState = CharacterState.Moving;
            }
            else
            {
                // Si el servidor dice que es ilegal, el jugador se queda quieto
                currentState = CharacterState.Idle;
            }
        }
        else
        {
            currentState = CharacterState.Idle;
        }

        ApplyGravity();
    }

    private void MovePlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(moveInput);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        controller.Move(moveInput * moveSpeed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0) velocity.y = -2f;
        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}