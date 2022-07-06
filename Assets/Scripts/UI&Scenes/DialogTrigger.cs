using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private DialogueManager dm;
    private bool wasOpen = false;

    private void Start()
    {
        dm = FindObjectOfType<DialogueManager>();
    }
    public void TriggerDialogue()
    {
        dm.StartDialogue(dialogue);
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.tag == "Player" && !wasOpen)
        {
            TriggerDialogue();
            wasOpen = true;
        }
    }
}
