/*
 * Copy and paste this for quick verb making
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class toFixRotation : Verb
{



    //     Public variables visible in inspector
    //________________________________________________
    //||||||||||||||||||||||||||||||||||||||||||||||||

    [Tooltip("Drag the gameobject you want to make rotate here, leaving it blank defaults to this object")]
    public Transform rotatingObject;

    [Tooltip("Any verbs added to this array will trigger when this is activated")]
    public Verb[] triggeredVerbs;




    //     The Update Function
    //________________________________________________
    //||||||||||||||||||||||||||||||||||||||||||||||||



    private void Start()
    {
        //Initialize variables here if required
        // If the Object to rotate field was left blank
        if (rotatingObject == null)
        {
            // Make this object the object to rotate
            rotatingObject = this.gameObject.transform;
        }
    }
    


    //     The Update Function
    //________________________________________________
    //||||||||||||||||||||||||||||||||||||||||||||||||



   private void Update()
    {
        if (isActive)
        {
            Quaternion fixedRotation = rotatingObject.rotation;

            fixedRotation.x = Mathf.Round(rotatingObject.rotation.x);
            fixedRotation.y = Mathf.Round(rotatingObject.rotation.y);
            fixedRotation.z = Mathf.Round(rotatingObject.rotation.z);
            fixedRotation.w = Mathf.Round(rotatingObject.rotation.w);
            rotatingObject.rotation = fixedRotation;

            //Debugging message
            //Debug.Log("I am");

            //Remove comments below to enable a conditional statement for ending the verb
            // if ()
            //{         
            isActive = false;
            Activate(triggeredVerbs);
            //}
        }
    }
}
//     Verb Description Below
//________________________________________________
//||||||||||||||||||||||||||||||||||||||||||||||||
/*
 * This just fixes the small rounding errors that accumulate when toRotate and toWait are paired.
 */
