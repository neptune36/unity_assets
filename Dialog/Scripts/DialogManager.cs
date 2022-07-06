using UnityEngine;

public class DialogManager : MonoBehaviour
{

    public DialogUI dialogPrefab;


    public void StartDialog(Dialog dialog)
    {
        GameObject go = Instantiate(dialogPrefab.gameObject);
       // go.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        DialogUI ui = go.GetComponent<DialogUI>();
        ui.StartDialog(dialog);
    }


}
