using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CardTheme {
Advanture,
Neutral,
SciFi,
Mystical,
Fantasy
}

/// <summary>
/// use this scriptable object to define the infomation of cards
/// </summary>
[CreateAssetMenu]
public class CardInfo : ScriptableObject
{
    public string cardTitle;
    
    public GameObject cardBackgroundPref;       //background of cards. Backgrounds can be independent of card type. Not actually used in this assignment
    public GameObject cardForegroundInfoPref;   //info panels to show the numbers and other info of the card.  Not actually used in this assignment
    public GameObject cardIllustrationPref;    //main illustration ui

    public Sprite cardSprite;   //main illustraction sprite

    //those stats are not used for visualization here. But in real game the stats of cards should be dynamically rendered based on data.
    public CardTheme theme;
    public string description;
    public int cost;
    public int initialHealth;
    public int initialAttack;

    //the actual attack and health might depend on the level.

    public virtual int CalculateAttack(int level)
    {
        return initialAttack;
    }
    public virtual int CalculateHealth(int level)
    {
        return initialHealth;
    }
}
