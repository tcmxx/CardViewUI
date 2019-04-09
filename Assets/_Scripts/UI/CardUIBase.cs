using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUIBase : MonoBehaviour
{
    [SerializeField]
    protected RectTransform graphicsRootRef;

    protected CardUIBackground background;
    protected CardUIIllustration illustration;
    protected CardUIForeground foreground;

    public Card LinkedCard { get; protected set; }
    public CardInfo LinkedCardInfo { get; protected set; }

    public void Initialize(Card card)
    {
        LinkedCard = card;
        LinkedCardInfo = card.Info;

        //instantiate the ui components
        background = Instantiate(LinkedCardInfo.cardBackgroundPref, graphicsRootRef).GetComponent<CardUIBackground>();
        illustration = Instantiate(LinkedCardInfo.cardIllustrationPref, graphicsRootRef).GetComponent<CardUIIllustration>();
        foreground = Instantiate(LinkedCardInfo.cardForegroundInfoPref, graphicsRootRef).GetComponent<CardUIForeground>();

        //initialize the components
        illustration.Initialize(LinkedCardInfo);
        foreground.Initialize(LinkedCardInfo);
    }
}
