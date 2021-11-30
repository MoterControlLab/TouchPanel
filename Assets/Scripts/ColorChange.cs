using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Controllables.ArtificialBased;
using VRTK.Controllables.PhysicsBased;
using VRTK.Examples;

public class ColorChange : MonoBehaviour
{
    //whether change color by index finger
    public bool IndexChangeColor;
    //current triggered object
    private GameObject currentTriggerObj;
    //whether hand color is changed
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

            if (TouchPanel.Instance.BeginStoreData)
            {
                if (!TouchPanel.Instance.IsAttachingWithHand && gameObject.name == "HandArea" )
                {
                    TouchPanel.Instance.IsAttachingWithHand = true;
                }

                if (collider.gameObject.name == "Lever" && gameObject.name == "Index")
                {
  
                  //  Debug.Log("OnTriggerEnter");
                    //when touch level do not need to use TouchPanel.Instance.isAttachingWithHand as a restriction for multiple ontriggerenter event, as headarea has alreay been used for this retriction
                    CheckWrongGesture(collider.gameObject);
                    TouchPanel.Instance.LastTriggerButtonName = "Lever";
    

                    if (gameObject.name == "Index" && IndexChangeColor)
                    {
                    

                        if (!TouchPanel.Instance.CurrentTask.WrongGestureOccur)
                        {
                            if (TouchPanel.Instance.CurrentTask.ColorChange)
                            {
                                transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.OutlineMaterial;
                            }

                            GetCurrentContactPoint(collider);

                            TouchPanel.Instance.CurrentTask.CurrentTouchTime = DateTime.Now;
                            TouchPanel.Instance.CurrentTask.TouchTime = DateTime.Now.ToString("hh:mm:ss:ff");
                            TouchPanel.Instance.isFirstAttachRecorded = true;
                        }


                    }
                }
                else if (collider.gameObject.name == "Cylinder")
                {
                    TouchPanel.Instance.LastTriggerButtonName = "Cylinder";


                   CheckWrongGesture(collider.gameObject);
                   //  Debug.Log(TouchPanel.Instance.CurrentTask.WrongGesture + " Enter Cylinder!  ");

                    if (!TouchPanel.Instance.CurrentTask.WrongGestureOccur && !IndexChangeColor)
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
                else if (collider.gameObject.name == "DialBase")
                {

                    TouchPanel.Instance.LastTriggerButtonName = "DialBase";
                    CheckWrongGesture(collider.gameObject);

                    if (!TouchPanel.Instance.CurrentTask.WrongGestureOccur)
                    {
                        if (!IndexChangeColor)
                        {
                           // Debug.Log("handcolorChanged before enter" + handcolorChanged);
                            if (TouchPanel.Instance.CurrentTask.ColorChange )
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


                            if (TouchPanel.Instance.CurrentTask.RightHand)
                            {
                                TouchPanel.Instance.RightHand.transform.parent.GetComponent<VRTK_ObjectAutoGrab>().enabled = true;
                                TouchPanel.Instance.RightHand.transform.parent.GetComponent<VRTK_ObjectAutoGrab>().objectToGrab = collider.gameObject.transform.parent.GetComponent<VRTK_InteractableObject>();

                                if (transform.parent.parent.parent.GetComponent<VRTK_InteractGrab>())
                                {
                                    transform.parent.parent.parent.GetComponentInChildren<VRTK_InteractGrab>().controllerAttachPoint = GetComponent<Rigidbody>();
                                }
                              
                            }

                            else
                            {
                                TouchPanel.Instance.LeftHand.transform.parent.GetComponent<VRTK_ObjectAutoGrab>().enabled = true;
                                TouchPanel.Instance.LeftHand.transform.parent.GetComponent<VRTK_ObjectAutoGrab>().objectToGrab = collider.gameObject.transform.parent.GetComponent<VRTK_InteractableObject>();
                                if (transform.parent.parent.parent.GetComponent<VRTK_InteractGrab>())
                                {
                                    transform.parent.parent.parent.GetComponentInChildren<VRTK_InteractGrab>().controllerAttachPoint = GetComponent<Rigidbody>();
                                }

                            }



                        }
                    }


                }
            }

        }
    }
    /// <summary>
    /// Get the realtime contact point between index finger collider and the toggle button lever collider
    /// </summary>
    /// <param name="collider"></param>
    private void GetCurrentContactPoint(Collider collider)
    {

        GameObject contactPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);


        var collisionPoint = GetComponent<CapsuleCollider>().ClosestPoint(collider.gameObject.transform.position);


        contactPoint.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        contactPoint.transform.SetParent(transform);


        float angle = CalculateAngle180_v3(gameObject.transform.up, (collisionPoint - transform.position));


        float distance;
        if (angle < 90 && angle > 0)
        {
            float radians = (angle * Mathf.PI) / 180;
            distance = Mathf.Cos(radians) * Vector3.Distance(collisionPoint, transform.position);
        }

        if (angle > -90 && angle < 0)
        {

            float radians = (-angle * Mathf.PI) / 180;
            distance = Mathf.Cos(-radians) * Vector3.Distance(collisionPoint, transform.position);
        }
        else if (angle < -90)
        {

            float radians = ((angle + 180) * Mathf.PI) / 180;
            distance = Mathf.Cos(radians) * Vector3.Distance(collisionPoint, transform.position);
        }
        else if (angle > 90)
        {
            float radians = ((180 - angle) * Mathf.PI) / 180;
            distance = Mathf.Cos(radians) * Vector3.Distance(collisionPoint, transform.position);
        }

        else distance = Vector3.Distance(collisionPoint, transform.position);



        Vector3 targetPoint;

        if (angle == 90 || angle == -90)
        {
            targetPoint = transform.position;
        }
        else
        {
            targetPoint = transform.position - transform.up * distance;
        }


        contactPoint.transform.position = targetPoint;

        TouchPanel.Instance.CurrentIndexContactPoint = contactPoint;
    }

    private void OnTriggerStay(Collider collider)
    {

        CheckWrongGesture(collider.gameObject);

        if (collider.gameObject.name == "DialBase" )
       {
            TouchPanel.Instance.LastTriggerButtonName = "DialBase";
            if (!TouchPanel.Instance.CurrentTask.WrongGestureOccur)
            {
                if (!IndexChangeColor)
                {
                    if (TouchPanel.Instance.CurrentTask.ColorChange)
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
                    if (TouchPanel.Instance.CurrentTask.RightHand)
                    {
                        TouchPanel.Instance.RightHand.transform.parent.GetComponent<VRTK_ObjectAutoGrab>().enabled = true;
                        TouchPanel.Instance.RightHand.transform.parent.GetComponent<VRTK_ObjectAutoGrab>().objectToGrab = collider.gameObject.transform.parent.GetComponent<VRTK_InteractableObject>();
                   //     TouchPanel.Instance.RightHand.transform.parent.GetComponent<VRTK_InteractGrab>().grabbedObject = collider.gameObject;
                    }
                    else
                    {
                        TouchPanel.Instance.LeftHand.transform.parent.GetComponent<VRTK_ObjectAutoGrab>().enabled = true;
                        TouchPanel.Instance.LeftHand.transform.parent.GetComponent<VRTK_ObjectAutoGrab>().objectToGrab = collider.gameObject.transform.parent.GetComponent<VRTK_InteractableObject>();
                    //    TouchPanel.Instance.LeftHand.transform.parent.GetComponent<VRTK_InteractGrab>().grabbedObject = collider.gameObject;

                    }
                    
                }
            }
    
            else
            {

                if (TouchPanel.Instance.CurrentTask.RightHand)
                {
                    TouchPanel.Instance.RightHand.transform.parent.GetComponent<VRTK_ObjectAutoGrab>().enabled = false;
                }
                else
                {
                    TouchPanel.Instance.LeftHand.transform.parent.GetComponent<VRTK_ObjectAutoGrab>().enabled = false;

                }

             
                transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;

                handcolorChanged = false;

            }

          //  Debug.Log(collider.gameObject.transform.parent.GetComponent<VRTK_PhysicsRotator>().IsGrabbedByAttach);
        }

        if (collider.gameObject.name == "Lever"  && gameObject.name == "Index")
        {
       //     Debug.Log("Stay in Level with gesture" + TouchPanel.Instance.CurrentTask.WrongGesture);

            if (!TouchPanel.Instance.CurrentTask.WrongGestureOccur)
            {
                if (IndexChangeColor)
                {

                    if (TouchPanel.Instance.CurrentTask.ColorChange && !handcolorChanged)
                    {
                        transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.OutlineMaterial;
                        handcolorChanged = true;

                    }

                    if (!TouchPanel.Instance.CurrentIndexContactPoint)
                    {
                        GetCurrentContactPoint(collider);
                    }

                    if (TouchPanel.Instance.CurrentTask.TouchTime == "")
                    {
                        TouchPanel.Instance.CurrentTask.CurrentTouchTime = DateTime.Now;
                        TouchPanel.Instance.CurrentTask.TouchTime = DateTime.Now.ToString("hh:mm:ss:ff");
                        TouchPanel.Instance.isFirstAttachRecorded = true;
                    }

                }

                if (TouchPanel.Instance.CurrentTask.Vibrate)
                {
                    VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(transform.parent.parent.parent.gameObject), 0.5f, 1f, 0.5f);
                }
              //  Debug.Log(transform.parent.parent.parent.gameObject.name);
           

            }

            else
            {
         
                transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;

                handcolorChanged = false;
            }


        }

    }
    /// <summary>
    /// get angle degree between two vectors
    /// </summary>
    /// <param name="fromDir"></param>
    /// <param name="toDir"></param>
    /// <returns></returns>
    private static float CalculateAngle180_v3(Vector3 fromDir, Vector3 toDir)
    {
        float angle = Quaternion.FromToRotation(fromDir, toDir).eulerAngles.y;

        if (angle > 180) { return 360 - angle; }
        return angle;
    }

    private void OnTriggerExit(Collider collider)
    {
        //*** for the rotatory button, the wrong gesture identification happens in exit process
        //***and currently  TouchPanel.Instance.isAttachingWithHand is already falso as handarea exit other object first
        if (collider.gameObject.name == "DialBase" && gameObject.name == "HandArea")
        {
            if (TouchPanel.Instance.CurrentTask.RightHand)
            {
                TouchPanel.Instance.RightHand.transform.parent.GetComponent<VRTK_ObjectAutoGrab>().enabled = false;
            }
            else TouchPanel.Instance.LeftHand.transform.parent.GetComponent<VRTK_ObjectAutoGrab>().enabled = false;


            if (TouchPanel.Instance.CurrentTask.ColorChange)
            {
                
                transform.parent.parent.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;
                handcolorChanged = false;


            }
            //reset
           TouchPanel.Instance.CurrentTask.WrongGestureOccur = false;

            if (TouchPanel.Instance.CurrentTask.RightHand)
                TouchPanel.Instance.RightHand.transform.parent.GetComponent<VRTK_ObjectAutoGrab>().enabled = false;
            else TouchPanel.Instance.LeftHand.transform.parent.GetComponent<VRTK_ObjectAutoGrab>().enabled = false;


            //  Debug.Log("Reset touchtime handarea leave rotatory" );

            return;
        }

        if (TouchPanel.Instance.IsAttachingWithHand && gameObject.name == "HandArea")
        {
            TouchPanel.Instance.IsAttachingWithHand = false;
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
                    TouchPanel.Instance.CurrentTask.WrongGestureOccur = false;
                    Destroy(TouchPanel.Instance.CurrentIndexContactPoint);
                  //  Debug.Log("Reset touchtime index leave " + currentTriggerObj.name);
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
                TouchPanel.Instance.CurrentTask.WrongGestureOccur = false;

               // Debug.Log("Reset touchtime handarea leave " + currentTriggerObj.name);
            }

        }



    }

    /// <summary>
    ///  If a wrong gesture occurs
    /// </summary>
    /// <param name="touchButton"></param>
    public void CheckWrongGesture(GameObject touchButton)
    {

        if (!TouchPanel.Instance.CurrentTask.WrongGestureOccur)
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

                
                    if (TouchPanel.Instance.CurrentTask.LastSequence != "")
                    {
                        TouchPanel.Instance.CurrentTask.WrongGestureOccur = true;

                    }


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

                    if (TouchPanel.Instance.CurrentTask.LastSequence != "")
                    {
                        TouchPanel.Instance.CurrentTask.WrongGestureOccur = true;

                    }

                }

            }


            if (touchButton.name == "Lever")
            {
                currentTriggerObj = touchButton;
                if (!TouchPanel.Instance.TriggerButtonClicked && !TouchPanel.Instance.GripButtonClicked)
                    UpdateSequence("|" + touchButton.transform.parent.GetComponent<ControllableReactor>().outputOnMin + "Toggle-Plam");
              
                if (TouchPanel.Instance.TriggerButtonClicked && !TouchPanel.Instance.GripButtonClicked)
                    UpdateSequence("|" + touchButton.transform.parent.GetComponent<ControllableReactor>().outputOnMin + "Toggle-OK");
                if (TouchPanel.Instance.TriggerButtonClicked && TouchPanel.Instance.GripButtonClicked)
                    UpdateSequence("|" + touchButton.transform.parent.GetComponent<ControllableReactor>().outputOnMin + "Toggle-Grip");         

                if (TouchPanel.Instance.CurrentTask.LastSequence!= "")
                {
                    TouchPanel.Instance.CurrentTask.WrongGestureOccur = true;

                }



            }
        }

        else
        {
            if (touchButton.name == "DialBase")
            {
                currentTriggerObj = touchButton;
                //only two buttons clicked will change color
                if (TouchPanel.Instance.TriggerButtonClicked && TouchPanel.Instance.GripButtonClicked)
                {
                    TouchPanel.Instance.CurrentTask.WrongGestureOccur = false;

                }

            }


            if (touchButton.name == "Cylinder")
            {
                currentTriggerObj = touchButton;
                //for push button cannot use any button
                if (!TouchPanel.Instance.TriggerButtonClicked && !TouchPanel.Instance.GripButtonClicked)
                {
                    TouchPanel.Instance.CurrentTask.WrongGestureOccur = false;
                }

            }


            if (touchButton.name == "Lever")
            {
                if (TouchPanel.Instance.GripButtonClicked && !TouchPanel.Instance.TriggerButtonClicked)
                {
                    TouchPanel.Instance.CurrentTask.WrongGestureOccur = false;

                }

            }
        }

    }

    /// <summary>
    ///  update current wrong gesture sequence
    /// </summary>
    /// <param name="newsequence"></param>
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
