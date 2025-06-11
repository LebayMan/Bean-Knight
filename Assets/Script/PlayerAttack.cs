using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerTypingChallenge : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera typingCamera;

    [Header("UI Settings")]
    [SerializeField] private Canvas typingCanvas;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text targetWordText;

    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float attackAngle = 45f;
    [SerializeField] private float attackCooldown = 0.5f;

    [Header("Typing Challenge")]
    [SerializeField] private string[] wordList = new string[] 
    { 
        "jump", "run", "attack", "dodge", "sprint", 
        "climb", "shoot", "block", "heal", "dash" 
    };
    public Animator SwordAnimator;

    private float lastAttackTime;
    private string currentTargetWord;
    private bool isTypingChallengeActive = false;
    private GameObject currentEnemy;

    void Start()
    {
        // Initial setup
        typingCanvas.enabled = false;
        typingCamera.enabled = false;
        mainCamera.enabled = true;
        
        inputField.onEndEdit.AddListener(CheckTypedWord);
    }

    void Update()
    {
        if (!isTypingChallengeActive && Input.GetMouseButtonDown(0) && 
            Time.time >= lastAttackTime + attackCooldown)
        {
            PerformConeAttack();
            lastAttackTime = Time.time;
        }
        //if (!Input.GetMouseButtonUp(0))
        //{
        //    SwordAnimator.ResetTrigger("Attack");
        //}
    }

    void PerformConeAttack()
    {
        Debug.Log("afwjiawf");
        SwordAnimator.SetTrigger("Attack");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        
        foreach (Collider hit in hitColliders)
        {
            Vector3 directionToTarget = (hit.transform.position - transform.position).normalized;
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);
            float distanceToTarget = Vector3.Distance(transform.position, hit.transform.position);
            
            if (angleToTarget <= attackAngle / 2 && distanceToTarget <= attackRange)
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                SimpleVN enemyVN = hit.GetComponent<SimpleVN>();
                if (enemy != null)
                {
                    if(!enemyVN.isdialougedone)
                    {
                    enemyVN.StartDialogue(); // Start dialogue with the enemy
                    mainCamera.enabled = false;
                    typingCamera.enabled = true;
                    Time.timeScale = 0f;
                    }
                    else
                    {
                        // Start typing challenge if enemy is already in dialogue
                        StartTypingChallenge(enemy);
                    }
                    break; // Only trigger for first enemy hit
                }
            }
        }
    }

    public void StartTypingChallenge(Enemy enemy)
    {
        currentEnemy = enemy.gameObject;
        isTypingChallengeActive = true;

        // Switch cameras
        mainCamera.enabled = false;
        typingCamera.enabled = true;

        // Enable UI
        typingCanvas.enabled = true;
        
        // Select random word
        currentTargetWord = wordList[Random.Range(0, wordList.Length)];
        targetWordText.text = currentTargetWord;
        
        // Clear and focus input field
        inputField.text = "";
        inputField.ActivateInputField();

        // Freeze player movement (assuming you have a character controller)
        Time.timeScale = 0f;
    }

    void CheckTypedWord(string typedWord)
    {
        if (typedWord == currentTargetWord)
        {
            EndTypingChallenge(true);
        }
        else
        {
            GameMaster.instance.playerUI.AddHealth(-10); // Deduct score for incorrect typing
            inputField.text = "";
            inputField.ActivateInputField();
            Debug.Log("Incorrect! Try again.");
        }
    }

    void EndTypingChallenge(bool success)
    {
        if (success && currentEnemy != null)
        {
            IDamageable damageable = currentEnemy.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(30);
                GameMaster.instance.playerUI.AddExp(20);
                Debug.Log($"Enemy hit successfully!");
            }
        }

        // Reset everything
        isTypingChallengeActive = false;
        typingCanvas.enabled = false;
        typingCamera.enabled = false;
        mainCamera.enabled = true;
        Time.timeScale = 1f;
        currentEnemy = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 forward = transform.forward * attackRange;
        Quaternion leftRot = Quaternion.Euler(0, -attackAngle/2, 0);
        Quaternion rightRot = Quaternion.Euler(0, attackAngle/2, 0);
        
        Vector3 leftRay = leftRot * transform.forward * attackRange;
        Vector3 rightRay = rightRot * transform.forward * attackRange;

        Gizmos.DrawRay(transform.position, leftRay);
        Gizmos.DrawRay(transform.position, rightRay);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

// Keep the IDamageable interface and Enemy class from previous example
public interface IDamageable
{
    void TakeDamage(int damage);
}

