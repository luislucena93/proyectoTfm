using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField]
    DialogueManager _dialogManager;
    public Dialogue dialogue;

    private bool wasOpen = false;

    private void Start()
    {
        _dialogManager = FindObjectOfType<DialogueManager>();
    }
    public void TriggerDialogue()
    {
        _dialogManager.StartDialogue(dialogue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER) && !wasOpen)
        {
            TriggerDialogue();
            wasOpen = true;
        }
    }
}
