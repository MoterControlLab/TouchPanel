using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Configuration", menuName = "ScriptableObjects/Configuration", order = 1)]
public class Configuration : ScriptableObject
{

  public string ConfigurationName;

  public  int L3Position = 0;
  public  int L2Position = 0;
  public  int L1Position = 0;
  public  int R3Position = 0;
  public  int R2Position = 0;
  public  int R1Position = 0;

  public  int L3Button = 0;
  public  int L2Button = 0;
  public  int L1Button = 0;
  public  int R3Button = 0;
  public  int R2Button = 0;
  public  int R1Button = 0;


    public string L3PositionStr;
    public string L2PositionStr;
    public string L1PositionStr;
    public string R3PositionStr;
    public string R2PositionStr;
    public string R1PositionStr;

    public string L3ButtonStr;
    public string L2ButtonStr;
    public string L1ButtonStr;
    public string R3ButtonStr;
    public string R2ButtonStr;
    public string R1ButtonStr;


    public string[] positions = new string[]
    {
     "Top", "Middle", "Bottom",
    };

    public string[] buttons = new string[]
    {
         "Toggle", "Rotatory", "Pusher",
    };


    public void GetDetails()
    {
        switch (L3Position)
        {
            case 0:
                L3PositionStr = "Top";
                break;
            case 1:
                L3PositionStr = "Middle";
                break;
            case 2:
                L3PositionStr = "Bottom";
                break;

            default:
                break;
        }

        switch (L2Position)
        {
            case 0:
                L2PositionStr = "Top";
                break;
            case 1:
                L2PositionStr = "Middle";
                break;
            case 2:
                L2PositionStr = "Bottom";
                break;

            default:
                break;
        }

        switch (L1Position)
        {
            case 0:
                L1PositionStr = "Top";
                break;
            case 1:
                L1PositionStr = "Middle";
                break;
            case 2:
                L1PositionStr = "Bottom";
                break;

            default:
                break;
        }

        switch (R1Position)
        {
            case 0:
                R1PositionStr = "Top";
                break;
            case 1:
                R1PositionStr = "Middle";
                break;
            case 2:
                R1PositionStr = "Bottom";
                break;

            default:
                break;
        }

        switch (R2Position)
        {
            case 0:
                R2PositionStr = "Top";
                break;
            case 1:
                R2PositionStr = "Middle";
                break;
            case 2:
                R2PositionStr = "Bottom";
                break;

            default:
                break;
        }

        switch (R3Position)
        {
            case 0:
                R3PositionStr = "Top";
                break;
            case 1:
                R3PositionStr = "Middle";
                break;
            case 2:
                R3PositionStr = "Bottom";
                break;

            default:
                break;
        }

        switch (L1Button)
        {


            case 0:
                L1ButtonStr = "Toggle";
                break;
            case 1:
                L1ButtonStr = "Rotatory";
                break;
            case 2:
                L1ButtonStr = "Pusher";
                break;

            default:
                break;
        }
        switch (L2Button)
        {
            case 0:
                L2ButtonStr = "Toggle";
                break;
            case 1:
                L2ButtonStr = "Rotatory";
                break;
            case 2:
                L2ButtonStr = "Pusher";
                break;

            default:
                break;
        }

        switch (L3Button)
        {
            case 0:
                L3ButtonStr = "Toggle";
                break;
            case 1:
                L3ButtonStr = "Rotatory";
                break;
            case 2:
                L3ButtonStr = "Pusher";
                break;

            default:
                break;
        }
        switch (R1Button)
        {
            case 0:
                R1ButtonStr = "Toggle";
                break;
            case 1:
                R1ButtonStr = "Rotatory";
                break;
            case 2:
                R1ButtonStr = "Pusher";
                break;

            default:
                break;
        }
        switch (R2Button)
        {
            case 0:
                R2ButtonStr = "Toggle";
                break;
            case 1:
                R2ButtonStr = "Rotatory";
                break;
            case 2:
                R2ButtonStr = "Pusher";
                break;

            default:
                break;
        }
        switch (R3Button)
        {
            case 0:
                R3ButtonStr = "Toggle";
                break;
            case 1:
                R3ButtonStr = "Rotatory";
                break;
            case 2:
                R3ButtonStr = "Pusher";
                break;

            default:
                break;
        }
    }
    
}



