using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables.ArtificialBased;
using VRTK.Examples;

public class ColorChange : MonoBehaviour
{

    public bool IndexChangeColor;

    private GameObject currentTriggerObj;

    private bool handcolorChanged;

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

        if (collider.gameObject.CompareTag("Button"))
        {

            if (TouchPanel.Instance.isRecording)
            {
                if (!TouchPanel.Instance.isAttachingWithHand && gameObject.name == "HandArea" )
                {
                    TouchPanel.Instance.isAttachingWithHand = true;
                }

                if (collider.gameObject.name == "Lever")
                {
                    //when touch level do not need to use TouchPanel.Instance.isAttachingWithHand as a restriction for multiple ontriggerenter event, as headarea has alreay been used for this retriction
                    CheckWrongGesture(collider.gameObject);

                    if (gameObject.name == "Index" && IndexChangeColor)
                    {
     
                        if (!TouchPanel.Instance.CurrentTask.WrongGesture)
                        {
                            if (TouchPanel.Instance.CurrentTask.ColorChange)
                            {
                                transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.OutlineMaterial;
                            }

                  
                            TouchPanel.Instance.CurrentTask.CurrentTouchTime = DateTime.Now;
                            TouchPanel.Instance.CurrentTask.TouchTime = DateTime.Now.ToString("hh:mm:ss:ff");
                            TouchPanel.Instance.isFirstAttachRecorded = true;
                        }


                    }
                }
                else 
 
                    if (collider.gameObject.name == "Cylinder")
                    {
 
                         CheckWrongGesture(collider.gameObject);
                 //   Debug.Log(TouchPanel.Instance.CurrentTask.WrongGesture + "   " + Time.frameCount);

                    if (!TouchPanel.Instance.CurrentTask.WrongGesture && !IndexChangeColor)
                        {
                            if (TouchPanel.Instance.CurrentTask.ColorChange)
                            {
                                transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.OutlineMaterial;
                            }

                            TouchPanel.Instance.CurrentTask.CurrentTouchTime = DateTime.Now;
                            TouchPanel.Instance.CurrentTask.TouchTime = DateTime.Now.ToString("hh:mm:ss:ff");
                       // Debug.Log(TouchPanel.Instance.CurrentTask.TouchTime + "   " + Time.frameCount);
                        TouchPanel.Instance.isFirstAttachRecorded = true;
                        }

                 
                    }

            }

        }
    }


   private void OnTriggerStay(Collider collider)
    {
        CheckWrongGesture(collider.gameObject);

        if (collider.gameObject.name == "DialBase" )
       {
            if (!TouchPanel.Instance.CurrentTask.WrongGesture)
            {
                if (!IndexChangeColor)
                {
                    if (TouchPanel.Instance.CurrentTask.ColorChange && !handcolorChanged)
                    {
                        transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.OutlineMaterial;
                        handcolorChanged = true;
                    }

                    if (TouchPanel.Instance.CurrentTask.TouchTime == "")
                    {
                        TouchPanel.Instance.CurrentTask.CurrentTouchTime = DateTime.Now;
                        TouchPanel.Instance.CurrentTask.TouchTime = DateTime.Now.ToString("hh:mm:ss:ff");
                        TouchPanel.Instance.isFirstAttachRecorded = true;
                    }

                }
            }
    
            else
            {
                transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;
                handcolorChanged = false;
            }
        }

        if (collider.gameObject.name == "Lever" )
        {
           

            if (!TouchPanel.Instance.CurrentTask.WrongGesture)
            {
                if (IndexChangeColor)
                {
                    if (TouchPanel.Instance.CurrentTask.ColorChange && !handcolorChanged)
                    {
                        transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.OutlineMaterial;
                        handcolorChanged = true;
                    }

                    if (TouchPanel.Instance.CurrentTask.TouchTime == "")
                    {
                        TouchPanel.Instance.CurrentTask.CurrentTouchTime = DateTime.Now;
                        TouchPanel.Instance.CurrentTask.TouchTime = DateTime.Now.ToString("hh:mm:ss:ff");
                        TouchPanel.Instance.isFirstAttachRecorded = true;
                    }

                }
            }

            else
            {
                transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;
                handcolorChanged = false;
            }


        }

    }

    private void OnTriggerExit(Collider collider)
    {
        //*** for the rotatory button, the wrong gesture identification happens in exit process
        //***and currently  TouchPanel.Instance.isAttachingWithHand is already falso as handarea exit other object first
        if (collider.gameObject.name == "DialBase" && gameObject.name == "HandArea")
        {       

            if (TouchPanel.Instance.CurrentTask.ColorChange)
            {

                transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;
                handcolorChanged = false;
            }
            //reset
           TouchPanel.Instance.CurrentTask.WrongGesture = false;
           TouchPanel.Instance.CurrentTask.TouchTime = "";

            Debug.Log("Reset touchtime handarea leave rotatory" );

            return;
        }

        if (TouchPanel.Instance.isAttachingWithHand && gameObject.name == "HandArea")
        {
            TouchPanel.Instance.isAttachingWithHand = false;
        }
       
        if ( collider.gameObject == currentTriggerObj )
        {
         
            if (gameObject.name == "Index" && IndexChangeColor)
            {

                if (!collider.gameObject.GetComponent<PushButton>() && TouchPanel.Instance.CurrentTask.ColorChange)
                {
                    if (IndexChangeColor)
                    {
                        transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;
                        handcolorChanged = false;
                    }
                    TouchPanel.Instance.CurrentTask.WrongGesture = false;
                    TouchPanel.Instance.CurrentTask.TouchTime = "";

                    Debug.Log("Reset touchtime index leave " + currentTriggerObj.name);
                }
            }

            else
            {
                if (TouchPanel.Instance.CurrentTask.ColorChange)
                {

                    transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;
                    handcolorChanged = false;
                }

                //reset when hand leave push
                TouchPanel.Instance.CurrentTask.WrongGesture = false;
                TouchPanel.Instance.CurrentTask.TouchTime = "";

                Debug.Log("Reset touchtime handarea leave " + currentTriggerObj.name);
            }

        }



    }


    public void CheckWrongGesture(GameObject touchButton)
    {

        if (!TouchPanel.Instance.CurrentTask.WrongGesture)
        {
            if (touchButton.name == "DialBase")
            {
                currentTriggerObj = touchButton;
                //only two buttons clicked will change color
                if (!TouchPanel.Instance.TriggerButtonClicked || !TouchPanel.Instance.GripButtonClicked)
                {
                    if (!TouchPanel.Instance.TriggerButtonClicked && TouchPanel.Instance.GripButtonClicked)
                        UpdateSequence("|" + touchButton.transform.parent.GetComponent<ControllableReactor>().outputOnMax +  "Rotatory-IndexFinger");
                       
                    if (TouchPanel.Instance.TriggerButtonClicked && !TouchPanel.Instance.GripButtonClicked)
                        UpdateSequence("|" + touchButton.transform.parent.GetComponent<ControllableReactor>().outputOnMax + "Rotatory-OK");
                    if (!TouchPanel.Instance.TriggerButtonClicked && !TouchPanel.Instance.GripButtonClicked)
                        UpdateSequence("|" + touchButton.transform.parent.GetComponent<ControllableReactor>().outputOnMax + "Rotatory-Palm");


                    TouchPanel.Instance.CurrentTask.WrongGesture = true;

                    return;
                }
            }


            if (touchButton.name == "Cylinder")
            {
                currentTriggerObj = touchButton;
                //for push button cannot use any button
                if (TouchPanel.Instance.TriggerButtonClicked || TouchPanel.Instance.GripButtonClicked)
                {
                    if (!TouchPanel.Instance.TriggerButtonClicked && TouchPanel.Instance.GripButtonClicked)
                        UpdateSequence("|" + touchButton.GetComponent<ControllableReactor>().outputOnMax + "Push-IndexFinger");
                    if (TouchPanel.Instance.TriggerButtonClicked && !TouchPanel.Instance.GripButtonClicked)
                        UpdateSequence("|" + touchButton.GetComponent<ControllableReactor>().outputOnMax + "Push-OK");
                    if (TouchPanel.Instance.TriggerButtonClicked && TouchPanel.Instance.GripButtonClicked)
                        UpdateSequence("|" + touchButton.GetComponent<ControllableReactor>().outputOnMax + "Push-Grip");

                    // Debug.Log(TouchPanel.Instance.CurrentTask.Sequence);

                    TouchPanel.Instance.CurrentTask.WrongGesture = true;
                    return;
                }
            }


            if (touchButton.name == "Lever")
            {
                currentTriggerObj = touchButton;
                if (TouchPanel.Instance.GripButtonClicked && !TouchPanel.Instance.TriggerButtonClicked)
                {
                    return;
                }
                if (!TouchPanel.Instance.TriggerButtonClicked && !TouchPanel.Instance.GripButtonClicked)
                    UpdateSequence("|" + touchButton.transform.parent.GetComponent<ControllableReactor>().outputOnMin + "Toggle-Plam");
              
                if (TouchPanel.Instance.TriggerButtonClicked && !TouchPanel.Instance.GripButtonClicked)
                    UpdateSequence("|" + touchButton.transform.parent.GetComponent<ControllableReactor>().outputOnMin + "Toggle-OK");
                if (TouchPanel.Instance.TriggerButtonClicked && TouchPanel.Instance.GripButtonClicked)
                    UpdateSequence("|" + touchButton.transform.parent.GetComponent<ControllableReactor>().outputOnMin + "Toggle-Grip");
                TouchPanel.Instance.CurrentTask.WrongGesture = true;


            }
        }
    }


    public void UpdateSequence(string newsequence)
    {
        if (TouchPanel.Instance.CurrentTask.LastSequence == "")
        {
            TouchPanel.Instance.CurrentTask.Sequence += newsequence;
            TouchPanel.Instance.CurrentTask.LastSequence = newsequence;
        }
        else
        {
            if (TouchPanel.Instance.CurrentTask.LastSequence != newsequence)
            {
                TouchPanel.Instance.CurrentTask.Sequence += newsequence;
                TouchPanel.Instance.CurrentTask.LastSequence = newsequence;
            }
        }

    }
}
