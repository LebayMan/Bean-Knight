using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Target yang diikuti (biasanya player)
    public float distance = 5.0f; // Jarak dari target
    public float height = 2.0f; // Tinggi kamera dari target
    public float rotationSpeed = 3.0f;
    public float minVerticalAngle = -20f;
    public float maxVerticalAngle = 80f;

    private float currentX = 0.0f;
    private float currentY = 0.0f;

    void LateUpdate()
    {
        if (target == null) return;

        // Input mouse untuk rotasi
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Batasi sudut vertikal
        currentY = Mathf.Clamp(currentY, minVerticalAngle, maxVerticalAngle);

        // Kalkulasi rotasi dan posisi
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 direction = new Vector3(0, 0, -distance);
        Vector3 position = target.position + new Vector3(0, height, 0) + rotation * direction;

        // Update posisi dan rotasi kamera
        transform.position = position;
        transform.LookAt(target.position + new Vector3(0, height, 0));
    }
}