using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsController : MonoBehaviour
{

    public float duration;
    public GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Countdown(duration));
    }

    IEnumerator Countdown(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameController.GetComponent<GameController>().MainLevel();
    }


}
