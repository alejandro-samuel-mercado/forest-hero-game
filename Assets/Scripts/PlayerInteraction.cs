using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform chestPoint; // Punto en el pecho del jugador donde se colocarán los animales.
    private GameObject animalCarried = null; // Referencia al animal que está siendo levantado.

    void Update()
    {
        // Suelta al animal si se presiona el clic izquierdo del mouse
        if (Input.GetMouseButtonDown(0) && animalCarried != null)
        {
            ReleaseAnimal();
        }

        // Si hay un animal siendo llevado, actualizar su posición
        if (animalCarried != null)
        {
            animalCarried.transform.position = chestPoint.position;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifica si el jugador ha colisionado con un animal
        if (collision.gameObject.CompareTag("animal") && animalCarried == null)
        {
            PickUpAnimal(collision.gameObject);
        }
    }

    void PickUpAnimal(GameObject animal)
    {
        // Detén la animación y el movimiento del animal
        animal.GetComponent<Animator>().enabled = false;

        // Desactiva el Rigidbody del animal para evitar problemas de física
        Rigidbody animalRb = animal.GetComponent<Rigidbody>();
        if (animalRb != null)
        {
            animalRb.isKinematic = true; // Detiene el movimiento físico del animal
            animalRb.detectCollisions = false; // Desactiva las colisiones mientras es transportado
        }

        // Guarda la referencia del animal para moverlo junto al jugador
        animalCarried = animal;
        animalCarried.transform.position = chestPoint.position; // Coloca el animal en el pecho inicialmente
    }

    void ReleaseAnimal()
    {
        // Suelta al animal y restablece la animación y movimiento
        if (animalCarried != null)
        {
            Animator animalAnimator = animalCarried.GetComponent<Animator>();
            if (animalAnimator != null)
            {
                animalAnimator.enabled = true; // Vuelve a activar la animación del animal
            }

            Rigidbody animalRb = animalCarried.GetComponent<Rigidbody>();
 if (animalRb != null)
            {
                animalRb.isKinematic = false; // Permite que el animal vuelva a moverse
                animalRb.detectCollisions = true; // Reactiva las colisiones

                // Mueve al animal un poco hacia adelante y hacia arriba al soltarlo, simulando un lanzamiento
                Vector3 releasePosition = transform.position + transform.forward * 2f + transform.up * 0.5f;
                animalRb.MovePosition(releasePosition);
            }

            animalCarried = null;
        }
    }
}

