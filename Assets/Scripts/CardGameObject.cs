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

    public void Activate()
    {
        if (active)
        {
            if (SpriteRenderer.sprite != CardData.Image && this.GetComponent<SuitedDescendingDropReceiver>() == null)
                AddDropReceiver();
        }
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
