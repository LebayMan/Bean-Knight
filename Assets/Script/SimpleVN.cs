using UnityEngine;
using TMPro;

public class SimpleVN : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        public string characterName;
        [TextArea]
        public string dialogueText;
    }

    [Header("UI")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    [Header("Dialogue Data")]
    public Dialogue[] dialogues;
    public PlayerTypingChallenge attack;
    public Enemy enemy;

    public GameObject nextbuttonObject; // Optional, still works for clicking manually if needed

    private int currentIndex = 0;
    private bool dialogueActive = false;
    public bool isdialougedone = false; // Optional, to check if dialogue is done

    void Start()
    {
        nextbuttonObject.SetActive(false); // Hide button at start
    }

    void Update()
    {
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue();
        }
    }

    public void StartDialogue()
    {
        currentIndex = 0;
        dialogueActive = true;
        nextbuttonObject.SetActive(true);
        ShowDialogue(currentIndex);
    }

    public void NextDialogue()
    {
        currentIndex++;
        if (currentIndex < dialogues.Length)
        {
            ShowDialogue(currentIndex);
        }
        else
        {
            dialogueActive = false;
            attack.StartTypingChallenge(enemy);
            nextbuttonObject.SetActive(false);
            nameText.text = "";
            dialogueText.text = "Gelud di mulai.";
            isdialougedone = true; // Optional, to check if dialogue is done
        }
    }

    void ShowDialogue(int index)
    {
        nameText.text = dialogues[index].characterName;
        dialogueText.text = dialogues[index].dialogueText;
    }
}
