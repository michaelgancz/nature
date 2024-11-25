using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    public Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myAnimator != null)
        {
            if(Input.anyKey)
            {
                myAnimator.SetTrigger("Walking");
            }
            else
            {
                myAnimator.SetTrigger("Idling");
            }
        }
        
    }
}
