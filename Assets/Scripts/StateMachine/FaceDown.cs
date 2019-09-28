using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    [CreateAssetMenu(menuName = "State/FaceDown")]
    public class FaceDown : State
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
            CardGameObject card = controller.GetComponent<CardGameObject>();
            SpriteRenderer renderer = controller.GetComponent<SpriteRenderer>();
            renderer.sprite = card.GetCardData().Image;
        }

        public override void UpdateState(StateController controller)
        {

        }
    }
}
