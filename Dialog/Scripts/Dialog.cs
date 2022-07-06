using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new dialog",menuName ="Game/Dialog")]
public class Dialog : ScriptableObject
{
    [TextArea(3,10)]
    public string[] sentences;
    private Queue<string> sentencesQueue = new Queue<string>();
    public DialogueEvent nextSentenceEvent;
    public DialogueEvent endDialogEvent;
    public string CurrentSentence { get; set; }

    public void StartDialog()
    {
        sentencesQueue.Clear();

        foreach (string sentence in sentences)
        {
            sentencesQueue.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentencesQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        CurrentSentence = sentencesQueue.Dequeue();
        nextSentenceEvent.Raise(this);
    }

    public void EndDialogue()
    {
        endDialogEvent.Raise(this);
    }

    public bool IsFinished()
    {
        return sentencesQueue.Count == 0;
    }
}
