using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables.ArtificialBased;
using VRTK.Examples;

public class ColorChange : MonoBehaviour
{

    public bool IndexChangeColor;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnCollisionEnter(Collision collider)
    {
 
        if ( collider.gameObject.CompareTag("Hand") && !IndexChangeColor)
        {
            if (gameObject.name == "DialControl")
            {
                //only two buttons clicked will change color
                if (!TouchPanel.Instance.TriggerButtonClicked || !TouchPanel.Instance.GripButtonClicked)
                {
                    return;
                }
            }

            if (TouchPanel.Instance.CurrentTask.ColorChange)
            {
                collider.transform.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.OutlineMaterial;
            }
  
            TouchPanel.Instance.CurrentTask.CurrentTouchTime = DateTime.Now;
            TouchPanel.Instance.CurrentTask.TouchTime = DateTime.Now.ToString("hh:mm:ss:ff");
            TouchPanel.Instance.CurrentTask.MutipleTouchTime++;
        
        }

        if ( collider.gameObject.name == "Index" && IndexChangeColor)
        {
            if (!TouchPanel.Instance.GripButtonClicked || TouchPanel.Instance.TriggerButtonClicked)
            {
                return;
            }
            if (TouchPanel.Instance.CurrentTask.ColorChange)
            {
                collider.transform.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.OutlineMaterial;
            }
         
            TouchPanel.Instance.CurrentTask.CurrentTouchTime = DateTime.Now;
            TouchPanel.Instance.CurrentTask.TouchTime = DateTime.Now.ToString("hh:mm:ss:ff");
            TouchPanel.Instance.CurrentTask.MutipleTouchTime++;

        }


    }

  // private void OnCollisionExit(Collision collider)
  // {
  //     if (TouchPanel.Instance.CurrentTask.ColorChange && collider.gameObject.CompareTag("Hand"))
  //     {
  //
  //         if (!GetComponent<PushButton>())
  //         {
  //             collider.transform.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;
  //         }
  //     }
  //         
  // }


    private void OnTriggerEnter(Collider collider)
    {
        if ( collider.gameObject.CompareTag("Hand") && !IndexChangeColor)
        {
            if (gameObject.name == "DialControl")
            {
                //only two buttons clicked will change color
                if (!TouchPanel.Instance.TriggerButtonClicked || !TouchPanel.Instance.GripButtonClicked)
                {
                    return;
                }
            }

            if (TouchPanel.Instance.CurrentTask.ColorChange)
            {
                collider.transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.OutlineMaterial;
            }
          
            TouchPanel.Instance.CurrentTask.CurrentTouchTime = DateTime.Now;
            TouchPanel.Instance.CurrentTask.TouchTime = DateTime.Now.ToString("hh:mm:ss:ff");
            TouchPanel.Instance.CurrentTask.MutipleTouchTime++;

        }
;
        if ( collider.gameObject.name == "Index" && IndexChangeColor)
        {
            if (!TouchPanel.Instance.GripButtonClicked || TouchPanel.Instance.TriggerButtonClicked)
            {
                return;
            }
            if (TouchPanel.Instance.CurrentTask.ColorChange)
                collider.transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.OutlineMaterial;

            TouchPanel.Instance.CurrentTask.CurrentTouchTime = DateTime.Now;
            TouchPanel.Instance.CurrentTask.TouchTime = DateTime.Now.ToString("hh:mm:ss:ff");
            TouchPanel.Instance.CurrentTask.MutipleTouchTime++;

        }


    }

    private void OnTriggerExit(Collider collider)
    {

        if (TouchPanel.Instance.CurrentTask.ColorChange && collider.gameObject.CompareTag("Hand") && !IndexChangeColor)
        {

            if (!GetComponent<PushButton>())
            {
                collider.transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;
            }
        }
        if (TouchPanel.Instance.CurrentTask.ColorChange && collider.gameObject.name == "Index" && IndexChangeColor)
        {

            if (!GetComponent<PushButton>())
            {
                collider.transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;
            }
        }
    }
}
