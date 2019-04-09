using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardViewManager : MonoBehaviour
{
    [Header("Refereces")]
    [SerializeField]
    protected GameObject cardBasePref;
    [SerializeField]
    protected RectTransform contentRootRef;

    protected List<CardUIBase> cardUIs; //list of all card UIs

    protected Comparison<CardUIBase> currentSortComparer;
    protected int themeFilter = 0b11111111;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeWithDeck(Deck deck)
    {
        cardUIs = new List<CardUIBase>();

        foreach (var c in deck.CardsList)
        {
            var cardUI = Instantiate(cardBasePref, contentRootRef).GetComponent<CardUIBase>();
            cardUI.Initialize(c);
            cardUIs.Add(cardUI);
        }

        //intiially sort by theme
        SetSortByTheme(true);
        Reorganze();
    }

    
    public void SetFilterTheme(CardTheme theme, bool allow)
    {
        if (allow)
        {
            themeFilter = themeFilter | 1<<(int)theme;
        }
        else
        {
            themeFilter = themeFilter & (~(1 << (int)theme));
        }
    }

    //reorder the cards based on filter and sorting functions
    public void Reorganze()
    {
        cardUIs.Sort(currentSortComparer); //for the cards based on comparer
        
        for(int i = 0; i < cardUIs.Count; ++i)
        {
            cardUIs[i].transform.SetSiblingIndex(i);
                cardUIs[i].gameObject.SetActive(FilterThemeFunction(cardUIs[i].LinkedCardInfo.theme));
        }
    }

    
    protected bool FilterThemeFunction(CardTheme theme)
    {
        return ((1 << (int)theme) & themeFilter) != 0;
    }


    #region callbacked for UI Unity events
    public void FilterThemeAdvanture(bool on)
    {
        SetFilterTheme(CardTheme.Advanture, on);
        Reorganze();
    }
    public void FilterThemeNeutral(bool on)
    {
        SetFilterTheme(CardTheme.Neutral, on);
        Reorganze();
    }
    public void FilterThemeSciFi(bool on)
    {
        SetFilterTheme(CardTheme.SciFi, on);
        Reorganze();
    }
    public void FilterThemeMystical(bool on)
    {
        SetFilterTheme(CardTheme.Mystical, on);
        Reorganze();
    }
    public void FilterThemeFantasy(bool on)
    {
        SetFilterTheme(CardTheme.Fantasy, on);
        Reorganze();
    }

    public void SetSortByTheme(bool on)
    {
        currentSortComparer = (c1, c2) => {
            int result = c1.LinkedCardInfo.theme.CompareTo(c2.LinkedCardInfo.theme);
            if (result == 0)
                return c1.LinkedCardInfo.name.CompareTo(c2.LinkedCardInfo.name);    //use name for comparison by default
            else
                return result;
        };
        Reorganze();
    }

    public void SetSortByManaCost(bool on)
    {
        currentSortComparer = (c1, c2) => {
            int result = c1.LinkedCardInfo.cost.CompareTo(c2.LinkedCardInfo.cost);
            if (result == 0)
                return c1.LinkedCardInfo.name.CompareTo(c2.LinkedCardInfo.name);    //use name for comparison by default
            else
                return result;
        };
        Reorganze();
    }
    #endregion

}
