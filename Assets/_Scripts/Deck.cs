using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Deck
{
    public List<Card> CardsList { get; private set; }

    public Deck()
    {
        CardsList = new List<Card>();
    }


    //Add a card with certain info
    public void AddCard(CardInfo info)
    {
        CardsList.Add(new Card(info));
    }

    public void Clear()
    {
        CardsList.Clear();
    }

    public void Remove(Card card)
    {
        CardsList.Remove(card);
    }

}
