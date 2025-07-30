using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladoeMatafuegos : MonoBehaviour
{
 
    public Transform chestPoint; // Punto en el pecho del jugador
    public GameObject humoObject; // Referencia al objeto vacío que actúa como humo
    private GameObject matafuegoCarried = null; // Extintor que se lleva
    public GameObject humoPrefab; // Prefab del humo
    public Transform salidaHumo; // Punto desde donde se emite el humo

private GameObject humoActual = null; 
  void Start()
    {
        humoObject.SetActive(false); // Asegúrate de que el objeto de humo esté desactivado al inicio
    }

    void Update()
    {
        if (matafuegoCarried != null)
        {
            matafuegoCarried.transform.position = chestPoint.position;
            matafuegoCarried.transform.rotation = Quaternion.Euler(270, transform.eulerAngles.y, 180);

            // Mantener el objeto de humo activo mientras se mantiene el clic izquierdo
            if (Input.GetMouseButton(0) )
            {
                humoObject.SetActive(true); // Activar el objeto de humo
 lanzarHumo(); 
            }
            if (Input.GetMouseButtonUp(0) && humoActual != null)
            {
                humoObject.SetActive(false); // Desactivar el objeto de humo al soltar el clic
Destroy(humoActual);
            }

            // Soltar el matafuegos al hacer clic derecho
            if (Input.GetMouseButtonDown(1))
            {
                DropMatafuego();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifica si el jugador ha colisionado con un extintor
        if (collision.gameObject.CompareTag("Extintor") && matafuegoCarried == null)
        {
            PickUpMatafuego(collision.gameObject);
        }
    }

    void PickUpMatafuego(GameObject matafuego)
    {
        // Desactiva el Rigidbody
        Rigidbody matafuegoRb = matafuego.GetComponent<Rigidbody>();
        if (matafuegoRb != null)
        {
            matafuegoRb.isKinematic = true; // Detiene el movimiento físico del extintor
            matafuegoRb.detectCollisions = false; // Desactiva las colisiones mientras es transportado
        }

        // Guarda la referencia para moverlo junto al jugador
        matafuegoCarried = matafuego;
        matafuegoCarried.transform.position = chestPoint.position; // Coloca en el pecho inicialmente
    }

   
   void lanzarHumo()
    {
        if (humoActual == null) // Asegurarse de que no haya humo previo
        {
            humoActual = Instantiate(humoPrefab, salidaHumo.position, salidaHumo.rotation);
            humoActual.transform.parent = salidaHumo; // Hacer que el humo se mueva con el punto de salida
        }
    }

void DropMatafuego()
    {
        if (matafuegoCarried != null)
        {
            // Reactivar el Rigidbody del extintor para soltarlo
            Rigidbody matafuegoRb = matafuegoCarried.GetComponent<Rigidbody>();
            if (matafuegoRb != null)
            {
                matafuegoRb.isKinematic = false; // Reactivar el movimiento físico del extintor
                matafuegoRb.detectCollisions = true; // Reactivar las colisiones
            
Vector3 releasePosition = transform.position + transform.forward * 2f + transform.up * 0.5f;
                matafuegoRb.MovePosition(releasePosition);
}

            // Dejar de llevar el matafuego
            matafuegoCarried = null;

        }
    }
}


