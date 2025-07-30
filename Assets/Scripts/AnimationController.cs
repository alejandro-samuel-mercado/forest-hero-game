using UnityEngine;

namespace Ursaanimation.CubicFarmAnimals
{
[RequireComponent(typeof(Rigidbody))]
    public class AnimationController : MonoBehaviour
    {
    public float moveSpeed = 3f;            // Velocidad de movimiento
    public float moveRange = 10f;           // Rango de movimiento aleatorio
    public float changeTargetTime = 5f;     // Tiempo entre cambios de dirección

    private Vector3 randomTargetPos;        // Posición objetivo aleatoria
    private float timer;                    // Temporizador para controlar el cambio de posición
 private Rigidbody rb;
    void Start()
    {
       rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Evitar que el Rigidbody rote debido a la física
        SetRandomTargetPosition();
    }

    void Update()
    {

    if(transform.position.y>15){

Destroy(this.gameObject);}
        timer += Time.deltaTime;

        // Cambiar la posición objetivo después del tiempo definido o al alcanzar el objetivo actual
        if (timer >= changeTargetTime || Vector3.Distance(transform.position, randomTargetPos) < 0.5f)
        {
            SetRandomTargetPosition();
            timer = 0f;
        }

        // Mover el animal hacia la posición aleatoria y actualizar la rotación
        MoveTowardsTarget();
    }

    private void SetRandomTargetPosition()
    {
        // Generar una posición aleatoria dentro del rango
        float randomX = Random.Range(-moveRange, moveRange);
        float randomZ = Random.Range(-moveRange, moveRange);
        randomTargetPos = transform.position + new Vector3(randomX, 0, randomZ);
    }

    private void MoveTowardsTarget()
    {  
        // Orientar al animal hacia la dirección del movimiento
        Vector3 direction = (randomTargetPos - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);

        // Mantener la velocidad vertical actual para que la gravedad funcione
        Vector3 move = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);
        rb.velocity = move;
    }

    }
}
