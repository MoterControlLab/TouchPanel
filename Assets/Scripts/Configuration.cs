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
  public int R1Position = 0;

  public  int L3Button = 0;
  public  int L2Button = 0;
  public  int L1Button = 0;
  public  int R3Button = 0;
  public  int R2Button = 0;
  public  int R1Button = 0;

    public string[] positions = new string[]
    {
     "Top", "Middle", "Bottom",
    };

    public string[] buttons = new string[]
    {
         "Toggle", "Rotatory", "Pusher",
    };

}



