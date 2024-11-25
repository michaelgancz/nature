using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorController : MonoBehaviour
{
    public List<GameObject> activeMirrors;
    public GameObject mirror;
    public int mirrorCounter = 0;
    public List<GameObject> northFacingWalls;
    public List<GameObject> southFacingWalls;
    public List<GameObject> eastFacingWalls;
    public List<GameObject> westFacingWalls;
    public List<Material> renTexMaterials;
    public List<RenderTexture> renderTextures;

    // replace some interactable object with a mirror
    public void ReplaceWithMirror(GameObject interactableObject) // should this be in a separate script?? 
    {
        GameObject myMirror = Instantiate(mirror, interactableObject.transform.parent, true) as GameObject;
        activeMirrors.Add(myMirror);

        // make sure the mirror is rendering its own camera
        myMirror.GetComponent<MeshRenderer>().material = renTexMaterials[mirrorCounter];
        myMirror.GetComponentInChildren<Camera>().targetTexture = renderTextures[mirrorCounter];

        // put the mirror in the position of whatever we're replacing
        Vector3 newPos = new Vector3(interactableObject.transform.position.x, interactableObject.transform.position.y + 0.1f, interactableObject.transform.position.z);
        myMirror.transform.position = interactableObject.transform.position;

        // reorient the mirror depending on which way its parent wall is facing
        if (northFacingWalls.Contains(interactableObject.transform.parent.gameObject))
        {
            myMirror.transform.Rotate(90, 180, 180);
        }
        else if (southFacingWalls.Contains(interactableObject.transform.parent.gameObject))
        {
            myMirror.transform.Rotate(90, 0, 180);
        }
        else if (eastFacingWalls.Contains(interactableObject.transform.parent.gameObject))
        {
            myMirror.transform.Rotate(0, 180, 0);
        }
        else if (westFacingWalls.Contains(interactableObject.transform.parent.gameObject))
        {
            myMirror.transform.Rotate(0, 180, 180);  // 0 90 90
        }
        SetGlobalScale(myMirror.transform, new Vector3(-0.12f, 0.8f, 0.06f));
        myMirror.transform.Translate(0, 0.1f, 0);

        mirrorCounter += 1;

        Destroy(interactableObject);
    }

    // set scale irrespective of parents (utility)
    public static void SetGlobalScale(Transform transform, Vector3 globalScale)
    {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
    }

}
