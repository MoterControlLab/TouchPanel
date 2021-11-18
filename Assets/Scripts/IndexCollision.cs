using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used in Index finger collide with specific toggle
/// </summary>
public class IndexCollision : MonoBehaviour
{
    /// <summary>
    /// used in VRTK_RotateTransformGrabAttach StartGrab Function
    /// </summary>
    public GameObject ControllerAnchor, ControllerAlias;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Lever" && TouchPanel.Instance.GripButtonClicked && !TouchPanel.Instance.TriggerButtonClicked)
        {
            TouchPanel.Instance.TouchToggle = true;
            TouchPanel.Instance.CurrentToggleHashCode = collider.gameObject.GetHashCode();
            //used in start Grab function
            TouchPanel.Instance.ControllerAnchor = ControllerAnchor;
            TouchPanel.Instance.ControllerAlias = ControllerAlias;
        }
    }


    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Lever")
        {
            TouchPanel.Instance.TouchToggle = false;
        }
    }
}
