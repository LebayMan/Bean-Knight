using UnityEngine;

public class Key : MonoBehaviour
{
    public bool doeskeygetpickedup = false;

    void Start()
    {
        
    }
    public void Reset()
    {
        gameObject.SetActive(true); // Reactivate the key
        doeskeygetpickedup = false; // Reset the pickup state
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            doeskeygetpickedup = true;
            gameObject.SetActive(false); // Deactivate the key after pickup
        }
    }
}
