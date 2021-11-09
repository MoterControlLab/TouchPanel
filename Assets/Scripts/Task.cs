using ScriptableObjectDropdown;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Task : MonoBehaviour
{
    //how many operation tester need to do
    public int OperationNum;
    //name of subject
    public string SubjectID = "Default User";
    //configuration file
    [ScriptableObjectDropdown(typeof(Configuration))]
    public ScriptableObjectReference Config;

    //the button  order sequnce
    [HideInInspector]
    public List<int> CurrentCodeList = new List<int>();

    public bool Vibrate;
    public bool Audio;
    public bool ColorChange;
    public bool RightHand;
    //whether the operation generated in a shuffle mode, advertage the number of each kind of button
    public bool Shuffle;
    [HideInInspector]
    public string Sequence = "";
    [HideInInspector]
    public string LastSequence = "";
    [HideInInspector]
    public bool WrongGesture;
    [HideInInspector]
    public string CurrentOperationShowTime;
    [HideInInspector]
    public DateTime CurrentOperationTime;
    [HideInInspector]
    public string TouchTime;
    [HideInInspector]
    public DateTime CurrentTouchTime;

    [HideInInspector]
    public string TargetButtonName;
    [HideInInspector]
    public string TriggeredButtonName;
    // Start is called before the first frame update


    void Start()
    {
 

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void GenerateOperationSequence()
    {
        CurrentCodeList.Clear();


        if (Shuffle)
        {
            if (OperationNum %6 != 0)
            {
                OperationNum += (6 - OperationNum % 6);
            }
        }

        int shuffleNum = OperationNum / 6;

        for (int i = 0; i < OperationNum; i++)
        {
            RandomIndex:
            int randomIndex = UnityEngine.Random.Range(0, 6);
            if (Shuffle)
            {
                if (GetShowTimesInaList(randomIndex, CurrentCodeList) < shuffleNum)
                {
                    CurrentCodeList.Add(randomIndex);
                }
                else { goto RandomIndex; }

            }
            else CurrentCodeList.Add(randomIndex);


        }
    }


    public int GetShowTimesInaList(int element, List<int> list)
    {
        int showtime = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == element)
            {
                showtime++;
            }
        }

        return showtime;
    }


    public void ShowOperationCode(int codeIndex)
    {
        CurrentOperationShowTime = DateTime.Now.ToString("hh:mm:ss:ff");
        CurrentOperationTime = DateTime.Now;
        //reset


        TouchPanel.Instance.CurrentTask.Sequence = "";
        TouchPanel.Instance.CurrentTask.LastSequence = "";
        switch (codeIndex)
        {
            case 0:
                TouchPanel.Instance.NoticeText.text = "L1";
                break;
            case 1:
                TouchPanel.Instance.NoticeText.text = "L2";
                break;
            case 2:
                TouchPanel.Instance.NoticeText.text = "L3";
                break;
            case 3:
                TouchPanel.Instance.NoticeText.text = "R1";
                break;
            case 4:
                TouchPanel.Instance.NoticeText.text = "R2";
                break;
            case 5:
                TouchPanel.Instance.NoticeText.text = "R3";
                break;
            default:
                TouchPanel.Instance.NoticeText.text = "";
                break;
        }
    }


}
