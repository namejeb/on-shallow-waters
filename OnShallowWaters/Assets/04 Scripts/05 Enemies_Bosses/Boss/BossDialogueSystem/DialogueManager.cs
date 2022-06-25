using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;


public class DialogueManager : MonoBehaviour
{
    [Header("Ref: ")]
    public static DialogueManager instance;
    public GameObject dialogueBox;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    
    public Dialogue dd;

    public bool isInteracted = false, isTyping;
    public string currSentence;
    
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
    }

    void Start()
    {
        sentences = new Queue<string>();
        var list = sentences.ToList();
        StartDialogue(dd);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isInteracted)
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

    public void StartDialogue(Dialogue dialogue)
    {
        isInteracted = true;
        dialogueBox.SetActive(true);
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
        isInteracted = false;
    }
}
