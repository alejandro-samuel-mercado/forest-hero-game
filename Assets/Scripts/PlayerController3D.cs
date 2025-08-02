using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeed = 8f;
    public float rotationSpeed = 10f;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private Animator animator;
    private Vector3 movement;
    private bool isGrounded;
    private Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Verificar si está en el suelo
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundCheckDistance, groundLayer);

        // Obtener entradas
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calcular dirección relativa a la cámara
        Vector3 forward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 right = cameraTransform.right;

        movement = (forward * moveZ + right * moveX).normalized;

        // Actualizar animaciones
        bool isMoving = movement.magnitude > 0.1f;
        animator.SetBool("isWalking", isMoving);
        animator.SetBool("isRunning", Input.GetKey(KeyCode.RightShift) && isMoving);
        animator.SetBool("isGrounded", isGrounded);

        if (isMoving)
        {
            animator.SetFloat("moveX", moveX);
            animator.SetFloat("moveZ", moveZ);
        }
    }

    void FixedUpdate()
    {
        if (movement.magnitude > 0.1f)
        {
            // Calcular velocidad
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed;

            // Movimiento
            Vector3 targetVelocity = movement * currentSpeed;
            targetVelocity.y = rb.velocity.y;
            rb.velocity = targetVelocity;

            // Rotación suave hacia la dirección de movimiento
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Frenar si no hay movimiento
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}