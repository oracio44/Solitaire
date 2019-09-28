using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayingCard
{
    public PlayingCard()
    {
        Name = string.Empty;
        Image = null;
    }

    public PlayingCard(string _name, Sprite _image, char _suit, int _value)
    {
        Name = _name;
        Image = _image;
        Suit = _suit;
        Value = _value;
    }

    public string Name;
    public Sprite Image;
    public char Suit;
    public int Value;
}

public class PlayingCardsDeck
{
    public List<PlayingCard> Cards = new List<PlayingCard>();
    static readonly string[] Values = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    static readonly string[] Suits = { "C", "D", "H", "S" };
    public Sprite CardBack;

    public int Count
    {
        get { return Cards.Count; }
    }

    public PlayingCardsDeck()
    {
        CreateDeck();
    }

    void CreateDeck()
    {
        foreach (string s in Suits)
        {
            for (int i = 0; i < Values.Length; i++)
            {
                string CardName = (s + Values[i]);
                Sprite CardFace = Resources.Load<Sprite>("PlayingCards/" + CardName);
                Cards.Add(new PlayingCard(CardName, CardFace, s.ToCharArray()[0], i + 1));
            }
        }
        CardBack = Resources.Load<Sprite>("PlayingCards/CardBack");
    }

    public void ShuffleDeck()
    {
        System.Random rand = new System.Random();
        int n = Cards.Count;
        int k;
        PlayingCard temp;
        for (int i = 0; i < n; i++)
        {
            k = rand.Next(i);
            temp = Cards[i];
            Cards[i] = Cards[k];
            Cards[k] = temp;
        }
    }

    //TODO change to bool with "OUT" value
    public PlayingCard DealTopCard()
    {
        PlayingCard Card = new PlayingCard();
        if (Cards.Count > 0)
        {
            Card = Cards[0];
            Cards.RemoveAt(0);
        }
        return Card;
    }

    public PlayingCard DealRandomCard()
    {
        PlayingCard Card = new PlayingCard();
        if (Cards.Count > 0)
        {
            System.Random rand = new System.Random();
            int i = rand.Next(Cards.Count);
            Card = Cards[i];
            Cards.RemoveAt(i);
        }
        return Card;
    }

    public void AddCardtoDeck(PlayingCard card)
    {
        Cards.Add(card);
    }
}
