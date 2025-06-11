using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public int score = 0;
    public SimplePlayerUI playerUI;
    public TextMeshProUGUI scoreText;
    private Teleporter teleporter;
    [Header("REFERENCES")]
    public GameObject wonpanel;
    public GameObject lostpanel;
    public GameObject player;
    public Enemy enemy;
    public SimpleVN simpleVN;
    public Key key;
    public Chest chest;
    public TextMeshProUGUI scorewin;
    public TextMeshProUGUI scorelose;
    public GameObject StarPanel;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateStars()
    {
        // Turn off all stars initially
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        StarPanel.SetActive(true);

        // Turn on stars based on score
        if (score >= 25)
            star1.SetActive(true);
        if (score >= 50)
            star2.SetActive(true);
        if (score >= 75)
            star3.SetActive(true);

        
    }
    public void WIN()
    {
        scorewin.text = "Score : " + score.ToString();
        UpdateStars();
        wonpanel.SetActive(true);
        player.SetActive(false);
        Time.timeScale = 0f; // Pause the game
    }
    public void Lose()
    {
        scorelose.text = "Score : "+ score.ToString();
        UpdateStars();
        lostpanel.SetActive(true);
        player.SetActive(false);
        Time.timeScale = 0f; // Pause the game
    }
    public void Reset()
    {
        score = 0;
        StarPanel.SetActive(false);
        enemy.Reset(); // Reset the enemy
        simpleVN.Reset(); // Reset the dialogue
        key.Reset(); // Reset the key
        chest.Reset(); // Reset the chest
        Time.timeScale = 1f; // Resume the game
        wonpanel.SetActive(false);
        lostpanel.SetActive(false);
        playerUI.currentHealth = playerUI.maxHealth;
        playerUI.UpdateUI();
        teleporter.TeleportPlayer(player.transform);
        PlayerTypingChallenge attack = player.GetComponent<PlayerTypingChallenge>();
        attack.EndTypingChallenge(false);
    }

    private void Start()
    {
        teleporter = GetComponent<Teleporter>();
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
