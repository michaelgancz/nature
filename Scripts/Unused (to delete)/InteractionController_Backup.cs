//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class InteractionController : MonoBehaviour
//{
//    public float interactionRadius = 2.0f; // how close you have to be to interact with things
//    public float interactionLimit = 0.05f; // debug - can't activate door from too close
//    public List<GameObject> interactableObjects; // what you can interact with
//    List<GameObject> currentInteractables = new(); // what you can currently interact with, i.e. what's close enough

//    // for glow interaction
//    public Material normalMaterial;
//    public Material glowMaterial;

//    // for interaction
//    [SerializeField] KeyCode interactionKey = KeyCode.X;

//    // for text display
//    public GameObject textCanvas;
//    public int textCounter = 0;
//    public int textThreshold0 = 2;
//    public int textThreshold1 = 5;
//    public int textThreshold2 = 8;

//    // for replacing with mirror
//    public GameObject mirror;
//    public MirrorController mirrorController;
//    public int mirrorCounter = 1;
//    public List<GameObject> northFacingWalls;
//    public List<GameObject> southFacingWalls;
//    public List<GameObject> eastFacingWalls;
//    public List<GameObject> westFacingWalls;
//    public List<Material> renTexMaterials; // potential brute force fix for camera issue
//    public List<RenderTexture> renderTextures; // potential brute force fix for camera issue

//    // for triggering platform motions in game
//    public List<GameObject> rotatingArchways;
//    public List<GameObject> movingBlocks1;
//    public List<GameObject> movingBlocks2;
//    public List<GameObject> movingDoors;

//    // for triggering audio
//    public AudioController audioController;

//    public bool clickEnabled = true;


//    // Start is called before the first frame update
//    void Start()
//    {
//        InitializeInteractionSettings();
//        FadeInText();
//    }

//    // Update is called once per frame
//    void FixedUpdate()
//    {
//        currentInteractables = ListInteractablesInRadius();
//        if (textCounter < 1 && clickEnabled) // just for the first click
//        {
//            ListenforClick();
//        }
//        else if (textCanvas.GetComponent<Canvas>().enabled == true && clickEnabled)
//        {
//            ListenforClick();
//        }
//        else if (interactableObjects != null)
//        {
//            foreach (GameObject interactableObject in interactableObjects.ToArray())
//            {
//                if (currentInteractables.Contains(interactableObject))
//                {
//                    Glow(interactableObject);
//                    ListenForKeyDown(interactableObject);
//                }
//                else
//                {
//                    StopGlow(interactableObject);
//                }
//            }
//        }

//    }

//    public void InitializeInteractionSettings()
//    {
//        textCanvas.GetComponent<Canvas>().enabled = false;
//        ToggleCanvas(); // not sure why this works but ok

//        if (interactableObjects != null)
//        {
//            foreach (GameObject interactable in interactableObjects)
//            {
//                StopGlow(interactable);
//            }
//        }
//        currentInteractables.Clear(); // initialize anything that might be close enough to interact with
//        currentInteractables = ListInteractablesInRadius();
//        foreach (GameObject currentInteractable in currentInteractables)
//        {
//            Glow(currentInteractable);
//        }
//    }

//    // returns list of gameobjects in interaction radius
//    public List<GameObject> ListInteractablesInRadius()
//    {
//        List<GameObject> interactablesInRadius = new();
//        if (interactableObjects != null)
//        {
//            foreach (GameObject interactableObject in interactableObjects)
//            {
//                float distance = Vector3.Distance(interactableObject.transform.position, this.transform.position);

//                if (distance < interactionRadius && distance > interactionLimit)
//                {
//                    interactablesInRadius.Add(interactableObject);
//                }
//            }
//        }
//        return interactablesInRadius;
//    }

//    // toggles glow on
//    public void Glow(GameObject interactableObject)
//    {
//        GameObject glowObject = interactableObject.GetComponent<SpriteController>().myOutlines[0];

//        if (glowObject.GetComponent<Renderer>().material != glowMaterial)
//        {
//            glowObject.GetComponent<Renderer>().material = glowMaterial;
//        }
//        else if (glowObject.GetComponent<Renderer>().material != normalMaterial)
//        {
//            glowObject.GetComponent<Renderer>().material = normalMaterial;
//        }
//    }

//    // toggles glow off
//    public void StopGlow(GameObject interactableObject)
//    {
//        GameObject glowObject = interactableObject.GetComponent<SpriteController>().myOutlines[0];

//        if (glowObject.GetComponent<Renderer>().material != normalMaterial)
//        {
//            glowObject.GetComponent<Renderer>().material = normalMaterial;
//        }
//    }

//    // if interaction key is pressed, do something
//    public void ListenForKeyDown(GameObject interactableObject)
//    {
//        if (Input.GetKeyDown(interactionKey))
//        {
//            //Debug.Log("Key down!");

//            if (textCanvas.GetComponent<Canvas>().enabled == false)
//            {
//                StartCoroutine(OnInteraction(interactableObject));
//            }
//        }
//    }

//    public void ListenforClick()
//    {
//        if (Input.GetKeyDown(interactionKey))
//        {
//            if (textCanvas.GetComponent<Canvas>().enabled == true)
//            {
//                clickEnabled = false;
//                StartCoroutine(OnClick());
//            }
//        }
//    }

//    IEnumerator OnClick()
//    {
//        FadeOutText();
//        yield return new WaitForSeconds(3f);  // wait for text to fade
//        TriggerAudio();
//        ToggleCanvas();
//        AdvanceText();
//        if (textCounter > 1)
//        {
//            RotateArchways();
//            MoveBlocks();
//        }
//    }

//    IEnumerator OnInteraction(GameObject interactableObject)
//    {
//        ReplaceWithMirror(interactableObject);
//        FadeInText();
//        yield return new WaitForSeconds(3f);
//        clickEnabled = true;
//    }

//    // turn text on or off
//    public void ToggleCanvas()
//    {
//        if (textCanvas.GetComponent<Canvas>().enabled == true)
//        {
//            textCanvas.GetComponent<Canvas>().enabled = false;
//        }
//        else
//        {
//            textCanvas.GetComponent<Canvas>().enabled = true;
//        }
//    }

//    // update text counter but don't display the new text yet
//    public void AdvanceText()
//    {
//        textCounter += 1;
//        if (textCounter > textCanvas.GetComponent<TextSettings>().mainTextToDisplay.Count - 1)
//        {
//            textCounter = 0;
//        }
//        textCanvas.GetComponent<TextSettings>().mainText.text = textCanvas.GetComponent<TextSettings>().mainTextToDisplay[textCounter];
//        textCanvas.GetComponent<TextSettings>().headingText.text = textCanvas.GetComponent<TextSettings>().headingTextToDisplay[textCounter];

//        audioController.textCounter = textCounter;
//    }

//    public void FadeInText()
//    {
//        textCanvas.GetComponent<TextSettings>().FadeIn();
//    }

//    public void FadeOutText()
//    {
//        textCanvas.GetComponent<TextSettings>().FadeOut();
//    }

//    // replace some interactable object with a mirror
//    public void ReplaceWithMirror(GameObject interactableObject) // should this be in a separate script?? 
//    {
//        GameObject myMirror = Instantiate(mirror, interactableObject.transform.parent, true) as GameObject;

//        mirrorController.GetComponent<MirrorController>().activeMirrors.Add(myMirror); // add new mirror to list of active mirrors

//        myMirror.GetComponent<MeshRenderer>().material = renTexMaterials[mirrorCounter]; // make sure the mirror is rendering its own camera
//        myMirror.GetComponentInChildren<Camera>().targetTexture = renderTextures[mirrorCounter];

//        Vector3 newPos = new Vector3(interactableObject.transform.position.x, interactableObject.transform.position.y + 0.1f, interactableObject.transform.position.z);
//        myMirror.transform.position = interactableObject.transform.position; // put the mirror in the position of whatever we're replacing

//        if (northFacingWalls.Contains(interactableObject.transform.parent.gameObject))
//        {
//            myMirror.transform.Rotate(90, 0, 180);
//        }
//        else if (southFacingWalls.Contains(interactableObject.transform.parent.gameObject))
//        {
//            myMirror.transform.Rotate(90, 180, 180);
//        }
//        else if (eastFacingWalls.Contains(interactableObject.transform.parent.gameObject))
//        {
//            myMirror.transform.Rotate(0, 180, 0);
//        }
//        else if (westFacingWalls.Contains(interactableObject.transform.parent.gameObject))
//        {
//            myMirror.transform.Rotate(0, 180, 180);  // 0 90 90
//        }

//        //SetGlobalScale(myMirror.transform, new Vector3 (-0.15f, 1, 0.075f));
//        SetGlobalScale(myMirror.transform, new Vector3(-0.12f, 0.8f, 0.06f));
//        myMirror.transform.Translate(0, 0.1f, 0);

//        mirrorCounter += 1;

//        Destroy(interactableObject);
//        interactableObjects.Remove(interactableObject);  // find a better way to do this?

//    }

//    // utility
//    public static void SetGlobalScale(Transform transform, Vector3 globalScale)
//    {
//        transform.localScale = Vector3.one;
//        transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
//    }

//    // rotate archways
//    public void RotateArchways()
//    {
//        foreach (GameObject rotatingArchway in rotatingArchways)
//        {
//            rotatingArchway.GetComponent<toRotate>().isActive = true;
//            rotatingArchway.GetComponent<toWait>().isActive = true;
//        }
//        //Debug.Log("Rotating Archways");
//    }

//    // move blocks once the text advances far enough, disables mesh renderer
//    public void MoveBlocks()
//    {
//        if (textCounter == textThreshold0)
//        {
//            foreach (GameObject movingDoor in movingDoors)
//            {
//                movingDoor.GetComponent<toTeleport>().isActive = true;
//            }
//        }
//        if (textCounter == textThreshold1)
//        {
//            foreach (GameObject movingBlock in movingBlocks1)
//            {
//                movingBlock.GetComponent<toTeleport>().isActive = true;
//                movingBlock.GetComponent<MeshRenderer>().enabled = false;
//            }
//        }
//        if (textCounter == textThreshold2)
//        {
//            foreach (GameObject movingBlock in movingBlocks2)
//            {
//                movingBlock.GetComponent<toTeleport>().isActive = true;
//                movingBlock.GetComponent<MeshRenderer>().enabled = false;
//            }
//        }
//    }

//    public void TriggerAudio()
//    {
//        audioController.TriggerAudio();
//    }

//    //IEnumerator TriggerAudio()
//    //{
//    //    audioController.TriggerAudio();
//    //    yield return new WaitForSeconds(5f);
//    //}
//}
