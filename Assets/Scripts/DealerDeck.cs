using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerDeck : MonoBehaviour, IClickable
{
    [SerializeField]
    int CardsToDeal = 3;
    List<GameObject>  dealtCards = new List<GameObject>();
    PlayingCardsDeck deck;

    Vector3 baseOffset = new Vector3(2.5f, 0, 0);
    [SerializeField]
    float xOffset = 1f;
    [SerializeField]
    float zOffset = 0.1f;

    void GetMasterDeck()
    {
        deck = Solitaire.GameManager.Deck;
    }

    public void Click(Vector2 ignore)
    {
        HideOldTuples();
        if (deck == null)
        {
            GetMasterDeck();
        }
        if (deck.Count > 0)
        {
            PlayTuple();
        }
        else
        {
            ReShuffleDeck();
        }
    }

    void HideOldTuples()
    {
        CleanupDealtCards();
        List<GameObject> hideList = new List<GameObject>();
        foreach (GameObject c in dealtCards)
        {
            c.SetActive(false);
        }

    }

    void CleanupDealtCards()
    {
        List<GameObject> removeList = new List<GameObject>();
        foreach (GameObject c in dealtCards)
        {
            if (!c.transform.IsChildOf(this.transform))
            { 
                removeList.Add(c);
            }
        }
        foreach (GameObject r in removeList)
        {
            dealtCards.Remove(r);
        }
    }

    void PlayTuple()
    {
        List<PlayingCard> tuple = DealThreeCards();
        CreateCardObjects(tuple);
        if (deck.Count == 0)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void CreateCardObjects(List<PlayingCard> tuple)
    {
        Transform parentObject = this.transform;
        CardGameObject newestCardObject = null;
        for (int i = 0; i < tuple.Count; i++)
        {
            //if (newestCardObject != null)
            //    newestCardObject.enabled = false;

            Vector3 newPosition = this.transform.position + baseOffset + new Vector3(xOffset * i, 0, -(zOffset * i));
            GameObject newCard = Solitaire.GameManager.CreateCardAtPosition(tuple[i], newPosition);
            newCard.transform.SetParent(parentObject);
            parentObject = newCard.transform;
            newestCardObject = newCard.GetComponent<CardGameObject>();
            newestCardObject.TurnFaceUp();
            dealtCards.Add(newCard);
        }
        newestCardObject.Activate();
    }

    private List<PlayingCard> DealThreeCards()
    {
        //get 3 new cards from deck
        List<PlayingCard> tuple = new List<PlayingCard>();
        for (int i = 0; i < CardsToDeal; i++)
        {
            PlayingCard dealtCard = deck.DealTopCard();
            if (dealtCard.Name != string.Empty)
            {
                tuple.Add(dealtCard);
            }
        }

        return tuple;
    }

    void ReShuffleDeck()
    {
        foreach(GameObject c in dealtCards)
        {
            deck.AddCardtoDeck(c.GetComponent<CardGameObject>().GetCardData());
            Destroy(c.gameObject); //Maybe don't destroy? recycle better?
        }
        dealtCards.Clear();
        this.GetComponent<SpriteRenderer>().enabled = true;
    }
     
    void RevealOldTuple()
    {
        CleanupDealtCards();
        if (dealtCards.Count == 0)
            return;
        int i = dealtCards.Count - CardsToDeal;
        int k = 0;
        if (i < 0)
        {
            i = 0;
        }
        GameObject lastEnabled = null;
        while(i < dealtCards.Count)
        {
            Vector3 newPosition = this.transform.position + baseOffset + new Vector3(xOffset * k, 0, -(zOffset * k));
            lastEnabled = dealtCards[i];
            lastEnabled.SetActive(true);
            lastEnabled.GetComponent<CardGameObject>().DeActivate();
            lastEnabled.transform.position = newPosition;
            i++;
            k++;
        }
        if(lastEnabled != null)
            lastEnabled.GetComponent<CardGameObject>().Activate();
    }

    public void Activate()
    {
        RevealOldTuple();
    }
}
