using UnityEngine;
using System.Collections.Generic;
using TMPro; // Importar TextMesh Pro
using System.Collections;

public class ContainerController : MonoBehaviour
{ public TextMeshProUGUI timerText; // Referencia al TextMeshPro donde se mostrará el tiempo
    public Transform helicopter; // Referencia al helicóptero
    public float liftHeight = 40f; // Altura hacia la que se elevará el helicóptero
    public float moveDistance = 180f; // Distancia que se moverá el helicóptero horizontalmente
    public float liftSpeed = 5f; // Velocidad de elevación del helicóptero
    public float flySpeed = 5f; // Velocidad de movimiento horizontal del helicóptero

    private float timer = 0f; // Contador de tiempo
    private bool hasFlown = false; // Indica si el helicóptero ya ha volado


 void Update()
    {
        // Incrementar el contador de tiempo
        timer += Time.deltaTime;

        // Calcular minutos y segundos
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        // Actualizar el texto del temporizador
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Comprobar si ha pasado 1 minuto
        if (timer >= 60f && !hasFlown)
        {
            hasFlown = true; // Evitar que el helicóptero vuelva a volar
            StartCoroutine(FlyHelicopter());
        }
    }
private IEnumerator FlyHelicopter()
{

    Vector3 initialPosition = helicopter.position;
    Vector3 targetLiftPosition = initialPosition + new Vector3(0, liftHeight, 0); // Volar hacia arriba
    Vector3 targetMovePosition = initialPosition + new Vector3(moveDistance, liftHeight, 0); // Moverse hacia adelante a la altura final

    // Elevar el helicóptero
    while (Vector3.Distance(helicopter.position, targetLiftPosition) > 0.1f)
    {
        helicopter.position = Vector3.MoveTowards(helicopter.position, targetLiftPosition, liftSpeed * Time.deltaTime);
        yield return null; // Esperar el siguiente frame
    }

    // Eliminar a los animales después de la elevación
    foreach (Transform child in transform)
    {
        if (child.CompareTag("animal"))
        {
            Destroy(child.gameObject);
        }
    }

    // Mover el helicóptero hacia adelante
    while (Vector3.Distance(helicopter.position, targetMovePosition) > 0.1f)
    {
        helicopter.position = Vector3.MoveTowards(helicopter.position, targetMovePosition, flySpeed * Time.deltaTime);
        yield return null; // Esperar el siguiente frame
    }

}

void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("animal"))
    {
        other.transform.SetParent(transform); // Establece el animal como hijo del contenedor
    }
}



}

