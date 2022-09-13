using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image p1Avatar;
    [SerializeField] private Image p2Avatar;
    [SerializeField] private Image npc1Avatar;
    [SerializeField] private Image npc1AvatarOjosCerrados;
    [SerializeField] private Canvas dialogueUI;
    public bool dialogOpen = false;

    public Queue<Sentence> sentences;

    [SerializeField]
    bool _pasarSiguienteDialogoAuto;

    [SerializeField]
    [Range(0.1f,5)]
    float _tiempoSiguienteDialogo = 2f;
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
        dialogOpen = true;
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
        
        switch (sentence.character)
        {
            case Characters.player1El:
                p1Avatar.enabled = true;
                p2Avatar.enabled = false;
                npc1Avatar.enabled = false;
                npc1AvatarOjosCerrados.enabled = false;
                break;
            case Characters.player2Ella:
                p1Avatar.enabled = false;
                p2Avatar.enabled = true;
                npc1Avatar.enabled = false;
                npc1AvatarOjosCerrados.enabled = false;
                break;
            case Characters.npc1:
                p1Avatar.enabled = false;
                p2Avatar.enabled = false;
                npc1Avatar.enabled = true;
                npc1AvatarOjosCerrados.enabled = false;
                break;
            case Characters.npc1OjosCerrados:
                p1Avatar.enabled = false;
                p2Avatar.enabled = false;
                npc1Avatar.enabled = false;
                npc1AvatarOjosCerrados.enabled = true;
                break;
        }

        StopAllCoroutines();
        StartCoroutine(SetDialogueText(sentence.text));

    }

    IEnumerator SetDialogueText(string text)
    {
        dialogueText.text = "";
        bool simbolo = false;
        foreach(char letter in text.ToCharArray())
        {
            
            dialogueText.text += letter;
            if(letter != '<' && letter !='>' && !simbolo){
                yield return null;
            }   else if(letter == '<'){
                simbolo = true;
            }   else if(letter == '>'){
                simbolo = false;
                yield return null;
            }
            
        }
        if(_pasarSiguienteDialogoAuto){
            yield return new WaitForSeconds(_tiempoSiguienteDialogo);
            DisplayNextSentence();
        }
    }

    public void EndDialogue()
    {
        dialogueUI.enabled = false;
        dialogOpen = false;
    }

    public bool isDialogueOpen(){
        return dialogOpen;
    }
}
