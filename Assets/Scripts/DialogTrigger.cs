using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private DialogueManager dm;

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
        Debug.Log(other.gameObject.tag);
       if (other.gameObject.tag == "Untagged")
        {
            TriggerDialogue();
        }
    }
}
