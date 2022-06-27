using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;


public class DialogueManager : MonoBehaviour
{
    [Header("Ref: ")]
    public static DialogueManager instance;
    public GameObject dialogueBox, playerControl;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    private Dialogue dialogue;
    private DialogueDatabase dialogueDatabase;
    public FloatingJoystick joystick;

    public bool isInteracted = false, isTyping;
    private string currSentence;
    
    private Queue<string> sentences;
    private IEnumerator coroutine;

    [Space][Space]
    [Header("Settings: ")]
    [SerializeField] private float typeSpeed = .02f;
    [SerializeField] private float delayTime = 1f;
    [SerializeField] private int bossNum, dialogueNum;

    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

        dialogueDatabase = GetComponent<DialogueDatabase>();
    }

    void Start()
    {
        sentences = new Queue<string>();
        var list = sentences.ToList();
        dialogue = dialogueDatabase.allBoss.bossDialogue[bossNum].dialogueList[dialogueNum];
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
        StartCoroutine(Delay(delayTime));
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            playerControl.SetActive(true);
            dialogueBox.SetActive(false);
            return;
        }

        currSentence = sentences.ElementAt(0);
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        coroutine = TypeSentence(sentence);
        StartCoroutine(coroutine);
    }

    private IEnumerator TypeSentence(string sentence)
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
        PlayerHandler.Instance.EnableAnDisableMove();
        isInteracted = false;
    }

    private IEnumerator Delay(float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
        
        isInteracted = true;
        PlayerHandler.Instance.EnableAnDisableMove();
        joystick.OnPointerUp(new PointerEventData(null));
        playerControl.SetActive(false);
        
        dialogueBox.SetActive(true);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
}
