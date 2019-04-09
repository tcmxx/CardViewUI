using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUIIllustration : MonoBehaviour
{
    [SerializeField]
    protected Image imageUiRef;
    [SerializeField]
    protected Material highlightMaterial;

    protected CardUIBase uiBaseRef;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(CardInfo info, CardUIBase uiBase)
    {
        uiBaseRef = uiBase;
        imageUiRef.sprite = info.cardSprite;
    }

    public void SetHighlight(bool highlight)
    {
        imageUiRef.material = highlight?highlightMaterial:null;
    }
    
}
