using UnityEngine;

public class SmokeController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisiona tiene el tag "Humo"
        if (other.CompareTag("Humo"))
        {
            Debug.Log("Fuego tocado por humo");
            Destroy(this.gameObject); // Destruye el fuego
        }
    }
}

