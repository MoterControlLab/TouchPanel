using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables.ArtificialBased;
using VRTK.Examples;

public class ColorChange : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }

   // private void OnCollisionEnter(Collision collider)
   // {
   //     Debug.Log(111111111111);
   //     if (TouchPanel.Instance.CurrentTask.ColorChange && collider.gameObject.CompareTag("Hand"))
   //     {
   //         collider.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.OutlineMaterial;
   //
   //     }
   //
   //
   //
   // }
   //
   // private void OnCollisionExit(Collision collider )
   // {
   //     if (TouchPanel.Instance.CurrentTask.ColorChange && collider.gameObject.CompareTag("Hand"))
   //         collider.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;
   // }

    private void OnCollisionEnter(Collision collider)
    {
 
        if (TouchPanel.Instance.CurrentTask.ColorChange && collider.gameObject.CompareTag("Hand"))
        {
            collider.transform.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.OutlineMaterial;
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
        if (TouchPanel.Instance.CurrentTask.ColorChange && collider.gameObject.CompareTag("Hand"))
        {
            collider.transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.OutlineMaterial;
            TouchPanel.Instance.CurrentTask.CurrentTouchTime = DateTime.Now;
            TouchPanel.Instance.CurrentTask.TouchTime = DateTime.Now.ToString("hh:mm:ss:ff");
            TouchPanel.Instance.CurrentTask.MutipleTouchTime++;

        }



    }

    private void OnTriggerExit(Collider collider)
    {
        Debug.Log(collider.name);

        if (TouchPanel.Instance.CurrentTask.ColorChange && collider.gameObject.CompareTag("Hand"))
        {

            if (!GetComponent<PushButton>())
            {
                collider.transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;
            }
        }

    }
}
