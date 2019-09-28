using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [SerializeField]
    protected State currentState;

    private void Update()
    {
        if(currentState != null)
            currentState.UpdateState(this);
    }

    public void ChangeState(State newState)
    {
        if (currentState != null)
            currentState.ExitState(this);
        currentState = newState;
        newState.EnterState(this);
    }
}
