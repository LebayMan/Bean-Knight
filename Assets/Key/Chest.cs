using UnityEngine;

public class Chest : MonoBehaviour
{

    public Key key;
    void Start()
    {
        
    }
    public void Reset()
    {
        gameObject.SetActive(true); // Reactivate the chest
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (key.doeskeygetpickedup == true)
            {
                GameMaster.instance.playerUI.AddCoin(10);
                gameObject.SetActive(false);
            }
        }
    }
}
