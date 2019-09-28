using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    [CreateAssetMenu(menuName = "State/DealtFaceDown")]
    public class DealtFaceDown : State
    {
        [SerializeField]
        State ActivationState;
        [SerializeField]
        Sprite CardBack;

        public override void ActivateState(StateController controller)
        {
            controller.ChangeState(ActivationState);
        }

        public override void EnterState(StateController controller)
        {
            controller.GetComponent<SpriteRenderer>().sprite = CardBack;
        }

        public override void ExitState(StateController controller)
        {
            CardGameObject card = controller.GetComponent<CardGameObject>();
            SpriteRenderer renderer = controller.GetComponent<SpriteRenderer>();
            renderer.sprite = card.GetCardData().Image;
            controller.gameObject.AddComponent<SuitedDescendingDropReceiver>();
        }

        public override void UpdateState(StateController controller)
        {

        }
    }
}
