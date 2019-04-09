using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//instance of cards
public class Card {

    public CardInfo Info{ get; protected set; }

    public int Level { get; set; }

    public Card(CardInfo info, int level = 1)
    {
        Info = info;
        Level = level;
    }
}
