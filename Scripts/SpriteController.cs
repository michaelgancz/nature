using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour // place this on sprites
{
    Renderer myRenderer;
    Renderer outlineRenderer;

    public List<Sprite> sprites;
    public List<GameObject> Outlines; // glowing outline is just a slightly larger copy of the sprite, only applies to interactable sprites

    public bool switchable = true;
    //public bool interactable = true;

    [HideInInspector] public bool alreadySwitched = false;
    [HideInInspector] public bool readyToSwitch = false;

    public Camera mainCamera;
    public float viewportBufferHorizontal; // for fine-tuning when a sprite is considered out of view
    public float viewportBufferVertical; // for fine-tuning when a sprite is considered out of view

    public MirrorController mirrorController; // keeps track of all our mirrors
    public GameObject textCanvas;

    public Material normalMaterial;
    public Material glowMaterial;

    // Initialize renderer & assign initial sprite
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        GetComponent<SpriteRenderer>().sprite = sprites[0];

        foreach (GameObject outline in Outlines)
        {
            outlineRenderer = outline.GetComponent<Renderer>();
            outline.GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ReadyToSwitch() && switchable)
        {
            SwitchToRandomSprite();
        }
    }

    public void SwitchToRandomSprite()
    {
        int choice = Random.Range(0, sprites.Count);
        GetComponent<SpriteRenderer>().sprite = sprites[choice];
        foreach (GameObject outline in Outlines)
        {
            outline.GetComponent<SpriteRenderer>().sprite = sprites[choice];
        }
    }

    public void Glow()
    {
        foreach (GameObject outline in Outlines)
        {
            outline.GetComponent<SpriteRenderer>().material = glowMaterial;
        }
    }

    public void StopGlow()
    {
        foreach (GameObject outline in Outlines)
        {
            outline.GetComponent<SpriteRenderer>().material = normalMaterial;
        }
    }

    bool InViewport(GameObject gameObject, Camera camera)
    {
        Vector3 viewPos = camera.WorldToViewportPoint(gameObject.transform.position);
        if (textCanvas.GetComponent<Canvas>().enabled == true)
        {
            return false;
        }
        else if (viewPos.x > 0f - viewportBufferHorizontal && viewPos.x < 1f + viewportBufferHorizontal && viewPos.y > 0f - viewportBufferVertical && viewPos.y < 1f + viewportBufferVertical)
        {
            return true;
        }
            return false;
    }

    bool ReadyToSwitch() // returns false if this object is in the main camera viewport, or a mirror reflecting this object is in the main camera viewport
    {
        if (InViewport(this.gameObject, mainCamera))
        {
            return false;
        }
        foreach (GameObject mirror in mirrorController.GetComponent<MirrorController>().activeMirrors)
        {
            if (InViewport(mirror, mainCamera))
            {
                if (InViewport(this.gameObject, mirror.GetComponentInChildren<Camera>()))
                {
                    return false;
                }
            }
        }
        return true;
    }
}
