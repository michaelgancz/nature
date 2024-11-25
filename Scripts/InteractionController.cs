using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    // for interaction
    public float interactionRadius = 2.0f; // how close you have to be to interact with things
    public List<GameObject> interactableObjects; // what you can interact with (in general)
    List<GameObject> currentInteractables = new(); // what you can currently interact with, i.e. what's close enough
    [SerializeField] KeyCode interactionKey = KeyCode.X;

    // for text display
    public GameObject textCanvas;

    // for replacing with mirror
    public MirrorController mirrorController;

    // for triggering audio
    public AudioController audioController;

    public GameController gameController;

    // for triggering platform motions in game
    public List<GameObject> rotatingArchways;
    public List<GameObject> movingBlocks1;
    public List<GameObject> movingBlocks2;

    [HideInInspector] public bool clickEnabled = true;


    // Start is called before the first frame update
    void Start()
    {
        InitializeInteractionSettings();
        FadeInText();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentInteractables = ListInteractablesInRadius();
        if (textCanvas.GetComponent<TextSettings>().textCounter < 1 && clickEnabled) // just for the first click
        {
            ListenforClick();
        }
        else if (textCanvas.GetComponent<Canvas>().enabled == true && clickEnabled)
        {
            ListenforClick();
        }
        else if (interactableObjects != null)
        {
            foreach (GameObject interactableObject in interactableObjects.ToArray())
            {
                if (currentInteractables.Contains(interactableObject))
                {
                    Glow(interactableObject);
                    ListenForKeyDown(interactableObject);
                }
                else
                {
                    StopGlow(interactableObject);
                }
            }
        }
    }

    public void InitializeInteractionSettings()
    {
        textCanvas.GetComponent<Canvas>().enabled = true;
        //ToggleCanvas();

        if (interactableObjects != null)
        {
            foreach (GameObject interactable in interactableObjects)
            {
                StopGlow(interactable);
            }
        }
        currentInteractables.Clear(); // initialize anything that might be close enough to interact with
        currentInteractables = ListInteractablesInRadius();
        foreach (GameObject currentInteractable in currentInteractables)
        {
            Glow(currentInteractable);
        }
    }

    // returns list of gameobjects in interaction radius
    public List<GameObject> ListInteractablesInRadius()
    {
        List<GameObject> interactablesInRadius = new();
        if (interactableObjects != null)
        {
            foreach (GameObject interactableObject in interactableObjects)
            {
                float distance = Vector3.Distance(interactableObject.transform.position, this.transform.position);

                if (distance < interactionRadius)
                {
                    interactablesInRadius.Add(interactableObject);
                }
            }
        }
        return interactablesInRadius;
    }

    // toggles glow on
    public void Glow(GameObject interactableObject)
    {
        interactableObject.GetComponent<SpriteController>().Glow();
    }

    // toggles glow off
    public void StopGlow(GameObject interactableObject)
    {
        interactableObject.GetComponent<SpriteController>().StopGlow();
    }

    // if interaction key is pressed, do something
    public void ListenForKeyDown(GameObject interactableObject)
    {
        if (Input.GetKeyDown(interactionKey))
        {
            if (textCanvas.GetComponent<Canvas>().enabled == false)
            {
                StartCoroutine(OnInteraction(interactableObject));
            }
        }
    }

    public void ListenforClick()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            if (textCanvas.GetComponent<Canvas>().enabled == true)
            {
                clickEnabled = false;
                StartCoroutine(OnCanvasClick());
            }
        }
    }

    IEnumerator OnCanvasClick()
    {
        FadeOutText();
        yield return new WaitForSeconds(3f);  // wait for text to fade
        Debug.Log(textCanvas.GetComponent<TextSettings>().textCounter);
        if (textCanvas.GetComponent<TextSettings>().textCounter == 8) // trigger cutscene on last door
        {
            StartCoroutine(gameController.GetComponent<GameController>().LoadScene("Cutscene"));
            yield return new WaitForSeconds(3f);
        }
        TriggerAudio();
        ToggleCanvas();
        AdvanceText();
        if (textCanvas.GetComponent<TextSettings>().textCounter > 1)
        {
            RotateArchways();
            MoveBlocks();
        }
    }

    IEnumerator OnInteraction(GameObject interactableObject)
    {
        ToggleCanvas();
        ReplaceWithMirror(interactableObject);
        FadeInText();
        yield return new WaitForSeconds(3f);
        clickEnabled = true;
    }

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

    public void AdvanceText()
    {
        textCanvas.GetComponent<TextSettings>().AdvanceText();
    }

    public void FadeInText()
    {
        textCanvas.GetComponent<TextSettings>().FadeIn();
    }

    public void FadeOutText()
    {
        textCanvas.GetComponent<TextSettings>().FadeOut();
    }

    public void ReplaceWithMirror(GameObject interactableObject)
    {
        mirrorController.GetComponent<MirrorController>().ReplaceWithMirror(interactableObject);
        interactableObjects.Remove(interactableObject);
    }

    public void RotateArchways()
    {
        foreach (GameObject rotatingArchway in rotatingArchways)
        {
            rotatingArchway.GetComponent<toRotate>().isActive = true;
            rotatingArchway.GetComponent<toWait>().isActive = true;
        }
    }

    // move blocks once the text advances far enough; disable mesh renderer
    public void MoveBlocks()
    {
        if (textCanvas.GetComponent<TextSettings>().textCounter == textCanvas.GetComponent<TextSettings>().textThreshold1)
        {
            foreach (GameObject movingBlock in movingBlocks1)
            {
                movingBlock.GetComponent<toTeleport>().isActive = true;
                movingBlock.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        if (textCanvas.GetComponent<TextSettings>().textCounter == textCanvas.GetComponent<TextSettings>().textThreshold2)
        {
            foreach (GameObject movingBlock in movingBlocks2)
            {
                movingBlock.GetComponent<toTeleport>().isActive = true;
                movingBlock.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    public void TriggerAudio()
    {
        audioController.TriggerAudio();
    }
}
