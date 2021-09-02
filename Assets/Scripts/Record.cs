using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
    public string OperationShowTime;
    public string OperationTriggerTime;
    public string MovementTime;
    public string OperationResult;
    public string TriggeredButtonName;
    public string TargetButtonName;
    public string AudioOn;
    public string VibrateOn;
    

    public Record(string operationshotime, string eventTime, string movementTime,string result, string buttonName, string targetButtonName)
    {
        OperationShowTime = operationshotime;
        OperationTriggerTime = eventTime;
        MovementTime = movementTime;
        OperationResult = result;
        TriggeredButtonName = buttonName;
        TargetButtonName = targetButtonName;
        AudioOn = TouchPanel.Instance.CurrentTask.Audio.ToString();
        VibrateOn = TouchPanel.Instance.CurrentTask.Vibrate.ToString();
    }

}
