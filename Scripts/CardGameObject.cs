using System.Collections;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CardGameObject : StateController, IClickable
{
    bool active = false;
    SpriteRenderer SpriteRenderer;

    Sprite CardBack;
    PlayingCard CardData;

    public PlayingCard GetCardData()
    {
        return CardData;
    }

    private void Awake()
    {
        SpriteRenderer = this.GetComponent<SpriteRenderer>();
        CardBack = SpriteRenderer.sprite;
    }

    public void SetupCard(PlayingCard _card)
    {
        this.name = _card.Name;
        CardData = _card;
    }

    public void TurnFaceUp()
    {
        if (SpriteRenderer.sprite != CardData.Image) //assuming CurrentState == FaceDown
        {
            currentState.ActivateState(this);
        }

    }

    public void Click(Vector2 ClickPoint)
    {
        if(active)
        {
            currentState.ActivateState(this);
        }
        else
        {
            if (SpriteRenderer.sprite != CardData.Image)
            {
                Debug.Log("Hidden, not Active: " + CardData.Name);
            }
            else
            {
                Debug.Log("Face up, not Active: " + CardData.Name);
            }
        }
    }

    IEnumerator DragDropCoroutine(Vector2 offset)
    {
        bool mouseDown = true;
        Vector3 OriginalPosition = transform.position;
        do
        {
            Vector3 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(MousePosition.x - offset.x, MousePosition.y - offset.y, -5);

            if (Input.GetKeyUp(KeyCode.Mouse0))
                mouseDown = false;
            else
                yield return null;
        } while (mouseDown);
        if (TryDrop())
        {
            Debug.Log("successful drop");
        }
        else
        {
            transform.position = OriginalPosition;
        }
    }

    private bool TryDrop()
    {
        bool DropResult = false;
        RaycastHit2D[] hitAll;
        hitAll = Physics2D.RaycastAll(transform.position, Vector2.zero, 20);
        foreach(RaycastHit2D hit in hitAll)
        {
            IDroppable dropTarget = hit.collider.GetComponent<IDroppable>();
            if (dropTarget != null)
            {
                DropResult = dropTarget.CheckDrop(this);
                if (DropResult)
                {
                    IClickable parentAction = transform.parent.GetComponent<IClickable>();
                    if (parentAction != null)
                    {
                        transform.SetParent(null);
                        parentAction.Activate();
                    }
                    dropTarget.DoDrop(this);
                    break;
                }
            }
        }
        return DropResult;
    }

    public void Activate()
    {
        active = true;
    }

    public void DeActivate()
    {
        active = false;
    }

    public void AddDropReceiver()
    {
        this.gameObject.AddComponent<SuitedDescendingDropReceiver>();
    }
}
