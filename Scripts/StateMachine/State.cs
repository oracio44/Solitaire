using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    public abstract void EnterState(StateController controller);
    public abstract void ExitState(StateController controller);
    public abstract void UpdateState(StateController controller);
    public abstract void ActivateState(StateController controller);
}
