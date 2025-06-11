using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameMaster.instance.AddScore(scoreValue);
            GameMaster.instance.playerUI.AddCoin(1);
            Destroy(gameObject);
        }
    }
}
