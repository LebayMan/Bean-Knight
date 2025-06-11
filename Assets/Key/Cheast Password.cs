using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasswordChecker : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField passwordInput;
    public TextMeshProUGUI resultText;

    [Header("Settings")]
    public string correctPassword = "12345";

    public void CheckPassword()
    {
        if (passwordInput.text == correctPassword)
        {
            //playerStatsUI.RefillAmmo(); // Call the method to refill ammo
            resultText.text = "Access Granted!";
            Destroy(gameObject); // Destroy the password checker object
            // You can add more actions here, like loading a new scene or enabling a panel
        }
        else
        {
            resultText.text = "Access Denied!";
            resultText.color = Color.red;
        }
    }
}
