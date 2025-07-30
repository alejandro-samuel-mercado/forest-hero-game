using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AnimalHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Salud máxima del animal
    private float currentHealth; // Salud actual
    public float damagePerSecond = 10f; // Daño por segundo al tocar el fuego

    // Referencia al contador de animales
    private static int totalAnimals = 0;
  public static TextMeshProUGUI animalCounterText;

    void Start()
    {
        currentHealth = maxHealth;
        totalAnimals++;
        UpdateAnimalCounter();
    }

    void Update()
    {
        // Si la vida del animal llega a cero, lo eliminamos
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void OnTriggerStay(Collider other)
    {
    // Si el animal está tocando el fuego, reducir la salud de forma gradual
    if (other.CompareTag("Fire"))
    {
        Debug.Log("El animal está en contacto con el fuego.");
        currentHealth -= damagePerSecond * Time.deltaTime;
    }
    }

    void Die()
    {
        totalAnimals--;
        UpdateAnimalCounter();
        Destroy(gameObject); // Elimina al animal de la escena
    }

    public static void UpdateAnimalCounter()
    {
        if (animalCounterText != null)
        {
            animalCounterText.text= " "+totalAnimals;
        }
    }
}

