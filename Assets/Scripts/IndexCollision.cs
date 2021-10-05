using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexCollision : MonoBehaviour
{
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
     //   Debug.Log(collider.gameObject.name + "dddddd");
        if (collider.gameObject.name == "Lever" && TouchPanel.Instance.GripButtonClicked && !TouchPanel.Instance.TriggerButtonClicked)
        {
            TouchPanel.Instance.TouchToggle = true;
            TouchPanel.Instance.TouchedToggle = true;
           TouchPanel.Instance.CurrentToggleHashCode = collider.gameObject.GetHashCode();
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
