using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDropReceiver : MonoBehaviour, IDroppable
{
    char PileSuit = char.MinValue;
    int currentValue = 0;

    public bool CheckDrop(CardGameObject sendingCard)
    {
        if(sendingCard.transform.childCount == 0)
        {
            PlayingCard card = sendingCard.GetCardData();
            if (card.Value == 1 && PileSuit == char.MinValue)
            {
                PileSuit = card.Suit;
            }
            if (PileSuit == card.Suit && card.Value == currentValue + 1)
            {
                return true;
            }
        }
        return false;
    }

    public void DoDrop(CardGameObject cardGO)
    {
        currentValue++;
        cardGO.transform.position = this.transform.position + (Vector3.back * currentValue);
        cardGO.transform.SetParent(this.transform);
        SuitedDescendingDropReceiver sourceDrop = cardGO.GetComponent<SuitedDescendingDropReceiver>();
        if (sourceDrop != null)
        {
            Destroy(cardGO.GetComponent<SuitedDescendingDropReceiver>());
        }
    }
}
