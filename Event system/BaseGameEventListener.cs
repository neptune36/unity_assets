using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseGameEventListener<T,E,UER> : MonoBehaviour, IGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T> 
{
    public E gameEvent;

    [SerializeField]
    public E GameEvent { get { return gameEvent; } set { GameEvent = value; } }
    public UER unityEventResponse;

    private void OnEnable()
    {
        if (GameEvent == null) return;
        GameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (GameEvent == null) return;
        GameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(T t)
    {
        if(unityEventResponse != null)
        {
            unityEventResponse.Invoke(t);
        }
    }
}
