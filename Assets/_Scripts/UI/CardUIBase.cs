using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardUIBase : MonoBehaviour
{
    [SerializeField]
    protected RectTransform graphicsRootRef;



    protected CardUIBackground background;
    protected CardUIIllustration illustration;
    protected CardUIForeground foreground;

    public Card LinkedCard { get; protected set; }
    public CardInfo LinkedCardInfo { get; protected set; }

    public CardViewManager CardViewManagerRef { get; private set; }
    public RectTransform CardGraphicsRef { get; private set; }

    public void Initialize(Card card, CardViewManager manager)
    {
        CardViewManagerRef = manager;
        LinkedCard = card;
        LinkedCardInfo = card.Info;
        CardGraphicsRef = graphicsRootRef;

        //instantiate the ui components
        background = Instantiate(LinkedCardInfo.cardBackgroundPref, graphicsRootRef).GetComponent<CardUIBackground>();
        illustration = Instantiate(LinkedCardInfo.cardIllustrationPref, graphicsRootRef).GetComponent<CardUIIllustration>();
        foreground = Instantiate(LinkedCardInfo.cardForegroundInfoPref, graphicsRootRef).GetComponent<CardUIForeground>();

        //initialize the components
        illustration.Initialize(LinkedCardInfo, this);
        foreground.Initialize(LinkedCardInfo);
    }

    public void ShowSingleCardView()
    {
        CardViewManagerRef.ShowSingleCardView(this);
    }

    public void SetHighlight(bool highlight)
    {
        background.SetHighlight(highlight);
        illustration.SetHighlight(highlight);
        foreground.SetHighlight(highlight);
    }
}
