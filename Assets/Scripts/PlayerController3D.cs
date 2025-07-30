using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class PlayerController3D : MonoBehaviour
{public float speed = 5f;
    public float runSpeed = 8f;
    private Rigidbody rb;
    private Animator animator;

    private Vector3 movement;
 public Transform cameraTransform;  // Transform de la cámara
    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Obtiene el componente Rigidbody
        animator = GetComponent<Animator>();  // Obtiene el componente Animator
    }

    void Update()
    {
        // Obtener entradas de teclado
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
  // Calcular dirección de movimiento en función de la cámara
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Ignorar la componente y (vertical) para que el personaje no intente moverse en el eje Y
        forward.y = 0f;
        right.y = 0f;
           forward.Normalize();
        right.Normalize();

        // Crear vector de movimiento basado en la entrada del usuario
        movement = (forward * moveZ + right * moveX).normalized;

        // Actualizar los parámetros del Animator
        bool isMoving = movement.magnitude > 0;

        animator.SetBool("isWalking", isMoving);
        animator.SetFloat("moveX", moveX);
        animator.SetFloat("moveZ", moveZ);

        // Cambiar animaciones según el movimiento
        if (movement.magnitude > 0)
        {
            animator.SetBool("isWalking", isMoving);
            animator.SetFloat("moveX", moveX);
            animator.SetFloat("moveZ", moveZ);

        
        if (Input.GetKey(KeyCode.LeftShift) && isMoving)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
  
    }
    }

    void FixedUpdate()
    {
        // Aplicar movimiento al Rigidbody
        if (movement.magnitude > 0)
        {
            // Ajustar la velocidad dependiendo si está corriendo o caminando
            float currentSpeed = animator.GetBool("isRunning") ? runSpeed : speed;
         
   Vector3 newPosition = rb.position + movement * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
            // Rotar el personaje en la dirección del movimiento
            Quaternion newRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, newRotation, Time.fixedDeltaTime * 10f);
        }
        
    }

}
