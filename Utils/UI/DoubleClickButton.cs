using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DoubleClickButton : MonoBehaviour, IPointerClickHandler
{
    float lastClick = 0f;
    float interval = 0.4f;
    public UnityEvent onDoubleClick;
    public void OnPointerClick(PointerEventData eventData)
    {
        if ((lastClick + interval) > Time.time)
        {
            onDoubleClick.Invoke();
        }

        lastClick = Time.time;
    }
}