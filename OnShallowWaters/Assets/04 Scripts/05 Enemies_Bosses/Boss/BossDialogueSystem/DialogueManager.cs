using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;


public class DialogueManager : MonoBehaviour
{
    [Header("Ref: ")]
    public static DialogueManager instance;
    public GameObject dialogueBox, playerControl;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    private Dialogue dialogue;
    private DialogueDatabase dialogueDatabase;

    [SerializeField] private int bossNum, dialogueNum;
    private bool isInteracted = false, isTyping;
    private string currSentence;
    
    private Queue<string> sentences;
    private IEnumerator coroutine;

    [Space][Space]
    [Header("Settings: ")]
    [SerializeField] private float typeSpeed = .02f;
    

    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

        dialogueDatabase = GetComponent<DialogueDatabase>();
        dialogue = dialogueDatabase.allBoss.bossDialogue[bossNum].dialogueList[dialogueNum];
    }

    void Start()
    {
        sentences = new Queue<string>();
        var list = sentences.ToList();
    }

    private void Update()
    {
        if (isInteracted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!isTyping)
                    DisplayNextSentence();
                else
                {
                    StopCoroutine(coroutine);
                    dialogueText.text = currSentence;
                    isTyping = false;
                }
            }
        }
    }

    public void StartDialogue()
    {
        isInteracted = true;
        dialogueBox.SetActive(true);
        playerControl.SetActive(false);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            dialogueBox.SetActive(false);
            return;
        }

        currSentence = sentences.ElementAt(0);
        Debug.Log(currSentence);
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        coroutine = TypeSentence(sentence);
        StartCoroutine(coroutine);
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTyping = false;
    }

    public void EndDialogue()
    {
        Debug.Log("there is no sentences alr");
        playerControl.SetActive(true);
        isInteracted = false;
    }
}
