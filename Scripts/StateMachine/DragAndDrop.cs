using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    [CreateAssetMenu(menuName = "State/DragAndDrop")]
    public class DragAndDrop : State
    {
        Vector2 offset;
        Vector3 originalPosition;
        [SerializeField]
        State callbackState;

        public override void EnterState(StateController controller)
        {
            Vector3 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = new Vector2(MousePosition.x - controller.transform.position.x, MousePosition.y - controller.transform.position.y);
            originalPosition = controller.transform.position;
        }

        public override void ExitState(StateController controller)
        {
            if (!DropCard(controller))
            {
                controller.transform.position = originalPosition;
            }
        }

        private bool DropCard(StateController controller)
        {
            bool DropResult = false;
            RaycastHit2D[] hitAll;
            IDroppable dropTarget = null;
            hitAll = Physics2D.RaycastAll(controller.transform.position, Vector2.zero, 20);
            foreach (RaycastHit2D hit in hitAll)
            {
                dropTarget = hit.collider.GetComponent<IDroppable>();
                if (dropTarget != null)
                {
                    DropResult = CheckDropResult(controller, dropTarget);
                    if (DropResult)
                    {
                        break;
                    }
                }
            }
            if (DropResult)
            {
                ActivateParent(controller);
                dropTarget.DoDrop(controller.GetComponent<CardGameObject>());
            }
            return DropResult;
        }

        private static void ActivateParent(StateController controller)
        {
            IClickable parentAction = controller.transform.parent.GetComponent<IClickable>();
            if (parentAction != null)
            {
                controller.transform.SetParent(null);
                parentAction.Activate();
            }
        }

        bool CheckDropResult(StateController controller, IDroppable dropTarget)
        {
            CardGameObject card = controller.GetComponent<CardGameObject>();
            return dropTarget.CheckDrop(card);

        }

        public override void UpdateState(StateController controller)
        {
            followMouse(controller);
        }

        private void followMouse(StateController controller)
        {
            Vector3 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            controller.transform.position = new Vector3(MousePosition.x - offset.x, MousePosition.y - offset.y, -5);
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                controller.ChangeState(callbackState);
            }
        }

        public override void ActivateState(StateController controller)
        {
            
        }
    }
}
