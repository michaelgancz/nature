using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    // for interaction
    [SerializeField] KeyCode interactionKey = KeyCode.Mouse0;

    // for text display
    public GameObject textCanvas;
    public bool clickEnabled = true;

    // for timing
    public float cutsceneDuration = 120f;

    public GameObject gameController;


    // Start is called before the first frame update
    void Start()
    {
        InitializeInteractionSettings();
        StartCoroutine(CountDown(cutsceneDuration));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (textCanvas.GetComponent<Canvas>().enabled == true && clickEnabled)
        {
            ListenforClick();
        }
    }

    public void InitializeInteractionSettings()
    {
        textCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void ListenforClick()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            if (textCanvas.GetComponent<Canvas>().enabled == true)
            {
                clickEnabled = false;
                StartCoroutine(OnClick());
            }
        }
    }

    IEnumerator CountDown(float duration)
    {
        yield return new WaitForSeconds(duration);
        ToggleCanvas();
        FadeInText();
        yield return new WaitForSeconds(3f);
        clickEnabled = true;
    }

    IEnumerator OnClick()
    {
        FadeOutText();
        yield return new WaitForSeconds(3f);  // wait for text to fade
        gameController.GetComponent<GameController>().About();
        //ToggleCanvas();
    }

    // turn text on or off
    public void ToggleCanvas()
    {
        if (textCanvas.GetComponent<Canvas>().enabled == true)
        {
            textCanvas.GetComponent<Canvas>().enabled = false;
        }
        else
        {
            textCanvas.GetComponent<Canvas>().enabled = true;
        }
    }

    public void FadeInText()
    {
        textCanvas.GetComponent<TextSettings>().FadeIn();
    }

    public void FadeOutText()
    {
        textCanvas.GetComponent<TextSettings>().FadeOut();
    }


}