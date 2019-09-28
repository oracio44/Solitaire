using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    [CreateAssetMenu(menuName = "State/FaceUp")]
    public class FaceUp : State
    {
        [SerializeField]
        State ActivationState;

        public override void ActivateState(StateController controller)
        {
            controller.ChangeState(ActivationState);
        }

        public override void EnterState(StateController controller)
        {

        }

        public override void ExitState(StateController controller)
        {

        }

        public override void UpdateState(StateController controller)
        {

        }
    }
}
