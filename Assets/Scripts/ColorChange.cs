using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnTriggerEnter(Collider collider)
    {

        if (TouchPanel.Instance.CurrentTask.ColorChange && collider.gameObject.CompareTag("Hand"))
        {
          // Debug.Log(11111111);
          // Debug.Log(collider.transform.parent.parent.name);
            collider.transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.OutlineMaterial;

        }



    }

    private void OnTriggerExit(Collider collider)
    {
        if (TouchPanel.Instance.CurrentTask.ColorChange && collider.gameObject.CompareTag("Hand"))
            collider.transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;
    }
}
