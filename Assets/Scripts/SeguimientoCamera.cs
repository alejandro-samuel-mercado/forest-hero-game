using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 2f, -5f);
    public float rotationSpeed = 300f;
    public float followSpeed = 20f;
    public float smoothSpeed = 0.2f;
    public float minVerticalAngle = -30f;
    public float maxVerticalAngle = 45f;
    public float minDistance = 3f;
    public float maxDistance = 10f;

    private float yaw = 0f;
    private float pitch = 0f;
    private float yawSmooth = 0f;
    private float pitchSmooth = 0f;
    private float currentDistance;

    void Start()
    {
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
        currentDistance = offset.magnitude;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Control de rotación con el ratón
        if (Input.GetMouseButton(1)) // Botón derecho del ratón
        {
            yaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            pitch -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        }

        // Limitar ángulos de rotación
        pitch = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle);
        
        // Control de zoom con la rueda del ratón
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentDistance = Mathf.Clamp(currentDistance - scroll * 5f, minDistance, maxDistance);

        // Suavizado de rotación
        yawSmooth = Mathf.Lerp(yawSmooth, yaw, smoothSpeed);
        pitchSmooth = Mathf.Lerp(pitchSmooth, pitch, smoothSpeed);

        // Calcular rotación y posición
        Quaternion rotation = Quaternion.Euler(pitchSmooth, yawSmooth, 0f);
        Vector3 desiredOffset = rotation * new Vector3(0f, offset.y, -currentDistance);
        Vector3 desiredPosition = player.position + desiredOffset;

        // Detección de obstáculos
        RaycastHit hit;
        if (Physics.Linecast(player.position + Vector3.up * offset.y, desiredPosition, out hit))
        {
            desiredPosition = hit.point;
        }

        // Aplicar posición y rotación
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.LookAt(player.position + Vector3.up * offset.y);
    }
}