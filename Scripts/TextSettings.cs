using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSettings : MonoBehaviour
{
    public List<string> headingTextToDisplay;
    [TextArea(15, 20)]
    public List<string> mainTextToDisplay;
    public TMP_Text headingText;
    public TMP_Text mainText;

    public GameObject textFadePanel;
    public Animator textAnimator;
    public GameObject tooltip;
    public Animator tooltipAnimator;

    public int textCounter = 0;
    public int textThreshold1 = 7;
    public int textThreshold2 = 6;

    private void Start()
    {
        headingText.text = headingTextToDisplay[0];
        mainText.text = mainTextToDisplay[0];

        textAnimator = textFadePanel.GetComponent<Animator>();
        tooltipAnimator = tooltip.GetComponent<Animator>();
    }

    public void AdvanceText()
    {
        textCounter += 1;
        if (textCounter > mainTextToDisplay.Count - 1)
        {
            textCounter = 0;
        }
        mainText.text = mainTextToDisplay[textCounter];
        headingText.text = headingTextToDisplay[textCounter];

        //audioController.textCounter = textCounter;
    }

    public void FadeIn()
    {
        textAnimator.SetTrigger("FadeIn");
        tooltipAnimator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        textAnimator.SetTrigger("FadeOut");
        tooltipAnimator.SetTrigger("FadeOut");
    }
}
