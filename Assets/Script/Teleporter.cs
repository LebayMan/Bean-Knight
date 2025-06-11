using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportDestination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ThirdPersonController controller = other.GetComponent<ThirdPersonController>();

            if (controller != null)
            {
                controller.enabled = false; // Disable to prevent unwanted physics issues
                other.transform.position = teleportDestination.position;
                controller.enabled = true; // Re-enable after teleporting
            }
        }
    }
    public void TeleportPlayer(Transform player)
    {
        if (player != null && teleportDestination != null)
        {
            player.gameObject.SetActive(false); // Optionally deactivate the player object
            player.position = teleportDestination.position;
            player.gameObject.SetActive(true); // Reactivate the player object
        }
    }
}
