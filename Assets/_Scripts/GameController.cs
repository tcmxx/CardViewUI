using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameController singleton for simple usage for this assignment
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public Deck PlayerDeck { get; private set; } = new Deck();
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
}
