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

        if (collider.gameObject.CompareTag("Hand"))
        {

            CheckWrongGesture();


            if (TouchPanel.Instance.CurrentTask.ColorChange &&!TouchPanel.Instance.CurrentTask.WrongGesture && !IndexChangeColor)
            {
                collider.transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.OutlineMaterial;
                TouchPanel.Instance.CurrentTask.CurrentTouchTime = DateTime.Now;
                TouchPanel.Instance.CurrentTask.TouchTime = DateTime.Now.ToString("hh:mm:ss:ff");
            }



        }
;
        if (collider.gameObject.name == "Index" && IndexChangeColor)
        {

            if (TouchPanel.Instance.CurrentTask.ColorChange && !TouchPanel.Instance.CurrentTask.WrongGesture)
            {
                collider.transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.OutlineMaterial;

                TouchPanel.Instance.CurrentTask.CurrentTouchTime = DateTime.Now;
                TouchPanel.Instance.CurrentTask.TouchTime = DateTime.Now.ToString("hh:mm:ss:ff");
                TouchPanel.Instance.CurrentTask.MutipleTouchTime++;
            }


        }


    }
  
   private void OnCollisionExit(Collision collider)
   {
        if ( collider.gameObject.CompareTag("Hand") )
        {

            if (!GetComponent<PushButton>() && TouchPanel.Instance.CurrentTask.ColorChange)
            {
                collider.transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;
            }
            TouchPanel.Instance.CurrentTask.WrongGesture = false;
            Debug.Log(222222);
        }
        if (collider.gameObject.name == "Index" && IndexChangeColor)
        {
            if (TouchPanel.Instance.CurrentTask.ColorChange )
            {
                if (!GetComponent<PushButton>())
                {
                    if (IndexChangeColor)
                    {
                        collider.transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;
                    }

                }

            }

        }
        
    }


    private void OnTriggerEnter(Collider collider)
    {
        if ( collider.gameObject.CompareTag("Hand"))
        {

            CheckWrongGesture();



            if (TouchPanel.Instance.CurrentTask.ColorChange && !TouchPanel.Instance.CurrentTask.WrongGesture && !IndexChangeColor)
            {
                collider.transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.OutlineMaterial;
                TouchPanel.Instance.CurrentTask.CurrentTouchTime = DateTime.Now;
                TouchPanel.Instance.CurrentTask.TouchTime = DateTime.Now.ToString("hh:mm:ss:ff");
                TouchPanel.Instance.CurrentTask.MutipleTouchTime++;
            }
          


        }
;
        if ( collider.gameObject.name == "Index" && IndexChangeColor)
        {

            if (TouchPanel.Instance.CurrentTask.ColorChange && !TouchPanel.Instance.CurrentTask.WrongGesture)
            {
                collider.transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.OutlineMaterial;

                TouchPanel.Instance.CurrentTask.CurrentTouchTime = DateTime.Now;
                TouchPanel.Instance.CurrentTask.TouchTime = DateTime.Now.ToString("hh:mm:ss:ff");
                TouchPanel.Instance.CurrentTask.MutipleTouchTime++;
            }
 

        }


    }

    private void OnTriggerExit(Collider collider)
    {

        if ( collider.gameObject.CompareTag("Hand"))
        {


            if (!GetComponent<PushButton>() && TouchPanel.Instance.CurrentTask.ColorChange)
            {
                collider.transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;
            }
            TouchPanel.Instance.CurrentTask.WrongGesture = false;
            Debug.Log(222222);
        }
        if ( collider.gameObject.name == "Index" && IndexChangeColor)
        {

            if (!GetComponent<PushButton>() && TouchPanel.Instance.CurrentTask.ColorChange)
            {
                if (IndexChangeColor)
                {
                    collider.transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;
                }
              
            }
        }
    }


    public void CheckWrongGesture()
    {
        if (!TouchPanel.Instance.CurrentTask.WrongGesture)
        {

            if (gameObject.name == "DialControl")
            {
                //only two buttons clicked will change color
                if (!TouchPanel.Instance.TriggerButtonClicked || !TouchPanel.Instance.GripButtonClicked)
                {
                    if (!TouchPanel.Instance.TriggerButtonClicked && TouchPanel.Instance.GripButtonClicked)
                        TouchPanel.Instance.CurrentTask.Sequence += ",Rotatory_IndexFinger";
                    if (TouchPanel.Instance.TriggerButtonClicked && !TouchPanel.Instance.GripButtonClicked)
                        TouchPanel.Instance.CurrentTask.Sequence += ",Rotatory_OK";
                    if (!TouchPanel.Instance.TriggerButtonClicked && !TouchPanel.Instance.GripButtonClicked)
                        TouchPanel.Instance.CurrentTask.Sequence += ",Rotatory_Palm";

                    TouchPanel.Instance.CurrentTask.WrongGesture = true;
                    return;
                }
            }


            if (gameObject.name == "Cylinder")
            {
                //for push button cannot use any button
                if (TouchPanel.Instance.TriggerButtonClicked || TouchPanel.Instance.GripButtonClicked)
                {
                    if (!TouchPanel.Instance.TriggerButtonClicked && TouchPanel.Instance.GripButtonClicked)
                        TouchPanel.Instance.CurrentTask.Sequence += ",Push_IndexFinger";
                    if (TouchPanel.Instance.TriggerButtonClicked && !TouchPanel.Instance.GripButtonClicked)
                        TouchPanel.Instance.CurrentTask.Sequence += ",Push_OK";
                    if (TouchPanel.Instance.TriggerButtonClicked && TouchPanel.Instance.GripButtonClicked)
                        TouchPanel.Instance.CurrentTask.Sequence += ",Push_Grip";

                    Debug.Log(TouchPanel.Instance.CurrentTask.Sequence);
                    Debug.Log(11111111111111);
                    TouchPanel.Instance.CurrentTask.WrongGesture = true;
                    return;
                }
            }


            if (gameObject.name == "Lever")
            {

                if (!TouchPanel.Instance.TriggerButtonClicked && !TouchPanel.Instance.GripButtonClicked)
                    TouchPanel.Instance.CurrentTask.Sequence += ",Toggle_Plam";
                if (TouchPanel.Instance.TriggerButtonClicked && !TouchPanel.Instance.GripButtonClicked)
                    TouchPanel.Instance.CurrentTask.Sequence += ",Toggle_OK";
                if (TouchPanel.Instance.TriggerButtonClicked && TouchPanel.Instance.GripButtonClicked)
                    TouchPanel.Instance.CurrentTask.Sequence += ",Toggle_Grip";
                TouchPanel.Instance.CurrentTask.WrongGesture = true;
                return;
            }
        }
    }
}
