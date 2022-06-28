using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Text dialogueText;
    [SerializeField] private Image p1Avatar;
    [SerializeField] private Image p2Avatar;
    [SerializeField] private Canvas dialogueUI;
    public bool dialogOpen = false;

    public Queue<Sentence> sentences;
    // Start is called before the first frame update
    void Start()
    {
        dialogueUI.enabled = false;
        sentences = new Queue<Sentence>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach (Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        dialogueUI.enabled = true;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        Debug.Log("show next");
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        Sentence sentence = sentences.Dequeue();
        dialogueText.text = sentence.text;
        if (sentence.character == Characters.player1)
        {
            dialogueText.alignment = (TextAnchor)TextAlignment.Left;
            p1Avatar.enabled = true;
            p2Avatar.enabled = false;
        }
        else
        {
            dialogueText.alignment = (TextAnchor)TextAlignment.Right;
            p1Avatar.enabled = false;
            p2Avatar.enabled = true;
        }
    }

    public void EndDialogue()
    {
        dialogueUI.enabled = false;
    }
}
