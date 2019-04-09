using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CardViewManager : MonoBehaviour
{
    [Header("Refereces")]
    [SerializeField]
    protected GameObject cardBasePref;
    [SerializeField]
    protected RectTransform contentRootRef;
    [SerializeField]
    protected RectTransform singleCardViewPositionRef;
    [SerializeField]
    protected RectTransform overlayMaskRef;


    [Header("Other")]
    public float singleCardViewScale = 2;
    public float singleCardViewTweenTime = 0.5f;
    public float overlayEndAlpha = 0.4f;

    protected List<CardUIBase> cardUIs; //list of all card UIs
    protected Comparison<CardUIBase> currentSortComparer;
    protected int themeFilter = 0b11111111;

    public RectTransform SingleCardViewPosition { get { return singleCardViewPositionRef; } }   //the position transform to view the single card

    public CardUIBase CurrentViewedCard { get; private set; } = null;   //the card currrently in single card view mode

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
        
        Clear();

        foreach (var c in deck.CardsList)
        {
            InstantiateCard(c);
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

    //instantiate a card UI object without flushing the view
    protected void InstantiateCard(Card c)
    {
        var cardUI = Instantiate(cardBasePref, contentRootRef).GetComponent<CardUIBase>();
        cardUI.Initialize(c,this);
        cardUIs.Add(cardUI);
    }

    //Add a card UI object and flush the view
    public void AddCard(Card c)
    {
        InstantiateCard(c);
        Reorganze();
    }


    public void ShowSingleCardView(CardUIBase cardBase)
    {
        CurrentViewedCard = cardBase;
        overlayMaskRef.gameObject.SetActive(true);
        overlayMaskRef.GetComponent<Image>().DOColor(new Color(0, 0, 0, overlayEndAlpha), singleCardViewTweenTime);
        cardBase.CardGraphicsRef.DOMove(SingleCardViewPosition.position, singleCardViewTweenTime);
        cardBase.CardGraphicsRef.DOScale(Vector3.one * singleCardViewScale, singleCardViewTweenTime);
        cardBase.CardGraphicsRef.SetParent(overlayMaskRef); //change the parent so that the card is shown on top
        cardBase.SetHighlight(true);
    }

    public void UnshowSingleCardView()
    {
        CurrentViewedCard.CardGraphicsRef.DOMove(CurrentViewedCard.transform.position, singleCardViewTweenTime);
        CurrentViewedCard.CardGraphicsRef.DOScale(Vector3.one, singleCardViewTweenTime);
        overlayMaskRef.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), singleCardViewTweenTime).onComplete =
            () => {
                //only disable the overlay and set it inactive after the tween
                CurrentViewedCard.CardGraphicsRef.SetParent(CurrentViewedCard.transform);
                overlayMaskRef.gameObject.SetActive(false);
                CurrentViewedCard.SetHighlight(false);
                CurrentViewedCard = null;
            };
    }

    public void Clear()
    {
        if (cardUIs != null)
        {
            foreach (var c in cardUIs)
            {
                Destroy(c.gameObject);
            }
            cardUIs.Clear();
        }
        else
        {
            cardUIs = new List<CardUIBase>();
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

    public void OnOverlayClicked()
    {
        //in single card view and the screen is clicked. Exit single card view.
        UnshowSingleCardView();
    }
    #endregion
}
