using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInitialDeck : MonoBehaviour
{
    public CardViewManager viewerManagerRef;
    public List<CardInfo> initialCardList;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var c in initialCardList)
        {
            GameController.Instance.PlayerDeck.AddCard(c);
        }

        viewerManagerRef.InitializeWithDeck(GameController.Instance.PlayerDeck);
    }
    
}
