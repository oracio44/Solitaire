using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitlessKingDropReceiver : MonoBehaviour, IDroppable
{
    public bool CheckDrop(CardGameObject sendingCard)
    {
        PlayingCard card = sendingCard.GetCardData();
        if (card.Value == 13 && transform.childCount == 0)
        {
            return true;
        }
        return false;
    }

    public void DoDrop(CardGameObject cardGO)
    {
        cardGO.transform.position = this.transform.position + (Vector3.back * 10);
        cardGO.transform.SetParent(this.transform);
        if (cardGO.transform.childCount == 0 && cardGO.GetComponent<IDroppable>() == null)
        {
            cardGO.AddDropReceiver();
        }
    }
}
