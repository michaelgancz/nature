using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public GameObject textCanvas;
    public List<GameObject> audioSourcesA;
    public List<GameObject> audioSourcesB;
    public List<GameObject> audioSourcesC;
    public List<GameObject> audioSourcesD;
    public List<GameObject> audioSourcesE;

    public List<GameObject> audioAccessoriesC;

    public void TriggerAudio()
    {
        if (textCanvas.GetComponent<TextSettings>().textCounter == 0)
        {
            //Debug.Log("Playing Section A");
            foreach (GameObject audioSource in audioSourcesA)
            {
                audioSource.GetComponent<Renderer>().enabled = true;
                audioSource.GetComponent<toWait>().isActive = true;
                audioSource.GetComponent<toGrow>().isActive = true;
            }
        }
        if (textCanvas.GetComponent<TextSettings>().textCounter == 1)
        {
            //Debug.Log("Playing Section B");
            foreach (GameObject audioSource in audioSourcesB)
            {
                audioSource.GetComponent<Renderer>().enabled = true;
                audioSource.GetComponent<toWait>().isActive = true;
            }
            foreach (GameObject audioSource in audioSourcesA)
            {
                audioSource.GetComponent<Renderer>().enabled = false;
            }
        }
        if (textCanvas.GetComponent<TextSettings>().textCounter == 3)
        {
            //Debug.Log("Playing Section C");
            foreach (GameObject audioSource in audioSourcesC)
            {
                audioSource.GetComponent<Renderer>().enabled = true;
                audioSource.GetComponent<toWait>().isActive = true;
            }
            foreach (GameObject audioSource in audioSourcesB)
            {
                audioSource.GetComponent<Renderer>().enabled = false;
            }
        }
        if (textCanvas.GetComponent<TextSettings>().textCounter == 6)
        {
            //Debug.Log("Playing Section D");
            foreach (GameObject audioSource in audioSourcesD)
            {
                audioSource.GetComponent<Renderer>().enabled = true;
                audioSource.GetComponent<toWait>().isActive = true;
            }
            foreach (GameObject audioSource in audioSourcesC)
            {
                audioSource.GetComponent<Renderer>().enabled = false;
            }
            foreach (GameObject audioSource in audioAccessoriesC)
            {
                audioSource.GetComponent<Renderer>().enabled = false;
            }
        }
        if (textCanvas.GetComponent<TextSettings>().textCounter == 7)
        {
            //Debug.Log("Playing Section E");
            foreach (GameObject audioSource in audioSourcesE)
            {
                audioSource.GetComponent<Renderer>().enabled = true;
                audioSource.GetComponent<toWait>().isActive = true;
            }
            foreach (GameObject audioSource in audioSourcesD)
            {
                audioSource.GetComponent<Renderer>().enabled = false;
            }
        }
    }
}
