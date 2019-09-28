using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent GameEvent;
    

    public void OnEventRaised()
    {
        GameEvent.Invoke();
    }

    private void OnEnable()
    {
        Event.Register(this);
    }

    private void OnDisable()
    {
        Event.DeRegister(this);
    }

}
