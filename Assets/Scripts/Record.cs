using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
    public string SubjectID;
    public string OperationShowTime;
    public string HandContactTime;
    public string OperationTriggerTime;
    public string OperationFinishTimeSpan;
    public string TouchToTriggerTimeSpan;
    public string OperationResult;
    public string TriggeredButtonPosition;
    public string TargetButtonPosition;
    public string TriggeredButtonType;
    public string TargetButtonType;
 //   public int TouchMultipleTimes;
    public string AudioOn;
    public string VibrateOn;
    public string ColorChangeOn;
    

    public Record(string subjectid,
                  string operationshotime,
                  string touchtime, 
                  string triggerTime, 
                  string movementTime,
                  string touchtoTriggerTime,
                  string result, 
                  string buttonPos, 
                  string targetButtonPos, 
                  string buttonType, 
                  string targetButtonType
                 // int touchmultipleTime
        )
    {
        SubjectID = subjectid;
        OperationShowTime = operationshotime;
        HandContactTime = touchtime;
        OperationTriggerTime = triggerTime;
        OperationFinishTimeSpan = movementTime;
        TouchToTriggerTimeSpan = touchtoTriggerTime;
        OperationResult = result;
        TargetButtonPosition = targetButtonPos;
        TriggeredButtonPosition = buttonPos;
        TargetButtonType = targetButtonType;
        TriggeredButtonType = buttonType;
       // TouchMultipleTimes = touchmultipleTime;
        AudioOn = TouchPanel.Instance.CurrentTask.Audio.ToString();
        VibrateOn = TouchPanel.Instance.CurrentTask.Vibrate.ToString();
        ColorChangeOn = TouchPanel.Instance.CurrentTask.ColorChange.ToString();
    }

}
