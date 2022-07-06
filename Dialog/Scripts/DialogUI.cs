using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    public Dialog dialog;
    public Text sentenceLabel;
    public Text buttonLabel;

    public void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        transform.SetParent(canvas.transform, false);
        StartDialog(dialog);
    }

    public void StartDialog(Dialog dialog)
    {
        this.dialog = dialog;
        dialog.StartDialog();
    }

    public void NextSentence()
    {
        dialog.DisplayNextSentence();
    }

    public void OnDialogueEvolve(Dialog dialog)
    {
        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialog.CurrentSentence));
        if (dialog.IsFinished())
        {
            buttonLabel.text = "End";
        }
        else
        {
            buttonLabel.text = "Next";
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        string text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            text += letter;
            sentenceLabel.text = text;
            yield return null;
        }
    }

    public void OnEndDialog(Dialog dialog)
    {
        Destroy(gameObject);
    }
}
