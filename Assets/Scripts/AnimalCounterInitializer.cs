using UnityEngine;
using TMPro; // Necesario para usar TextMeshPro

public class AnimalCounterInitializer : MonoBehaviour
{
    public TextMeshProUGUI animalCounter;

    void Start()
    {
        AnimalHealth.animalCounterText = animalCounter;
        AnimalHealth.UpdateAnimalCounter(); // Actualiza el contador al iniciar
    }
}

