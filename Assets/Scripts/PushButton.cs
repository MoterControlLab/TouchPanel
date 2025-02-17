﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables.ArtificialBased;
using VRTK.Examples;

public class PushButton : MonoBehaviour
{
    private ControllableReactor controllableReactor;
    private bool isOncollision;
    private GameObject currentHand;
    private float distancetohand;
    // Start is called before the first frame update
    void Start()
    {
        controllableReactor = GetComponent<ControllableReactor>();

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHand)
        {
            distancetohand = Vector3.Distance(this.gameObject.transform.position, currentHand.transform.position);
            //when the distance between hand and push button is more than 0.075, the push button begin to reset to original position

            if (!isOncollision && controllableReactor.controllable.gameObject.GetComponent<VRTK_ArtificialPusher>().stayPressed && distancetohand > 0.075f)
            {

                controllableReactor.controllable.gameObject.GetComponent<VRTK_ArtificialPusher>().stayPressed = false;
                controllableReactor.controllable.gameObject.GetComponent<VRTK_ArtificialPusher>().SetToRestingPosition();
                currentHand.transform.GetComponentInChildren<SkinnedMeshRenderer>().material = TouchPanel.Instance.HandInitialMaterial;
            }

        }

     



    }

    private void OnTriggerEnter(Collider other)
    {


        currentHand = other.gameObject;
        if (!TouchPanel.Instance.TriggerButtonClicked && !TouchPanel.Instance.GripButtonClicked)
        {
            isOncollision = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {

        isOncollision = false;

    }
}
