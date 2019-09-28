using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitedDescendingDropReceiver : MonoBehaviour, IDroppable
{
    public bool CheckDrop(CardGameObject sendingCard)
    {
        PlayingCard card = sendingCard.GetCardData();
        PlayingCard CardData = this.GetComponent<CardGameObject>().GetCardData();
        bool thisRed = false, thatRed = false;
        if (CardData.Suit == 'D' || CardData.Suit == 'H')
            thisRed = true;
        if (card.Suit == 'D' || card.Suit == 'H')
        {
            thatRed = true;
        }
        if (thisRed != thatRed)
        {
            if (CardData.Value - 1 == card.Value)
            {
                Destroy(this);
                return true;
            }
        }
        return false;
    }

    public void DoDrop(CardGameObject cardGO)
    {
        cardGO.transform.position = this.transform.position - new Vector3(0, Solitaire.GameManager.YOffset, Solitaire.GameManager.ZOffset);
        cardGO.transform.SetParent(this.transform);
        if (cardGO.transform.childCount == 0 && cardGO.GetComponent<IDroppable>() == null)
        {
            cardGO.AddDropReceiver();
        }
    }
}
