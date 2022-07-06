using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateOnAwake : MonoBehaviour
{
    public List<StateActive> states;

    private void Awake()
    {
        foreach(StateActive state in states)
        {
            state.gameObject.SetActive(state.state);
        }
    }

    [System.Serializable]
    public class StateActive
    {
        public GameObject gameObject;
        public bool state;
    }
}
