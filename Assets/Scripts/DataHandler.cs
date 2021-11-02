using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class DataHandler : MonoBehaviour
{


    public List<string[]> RecordData = new List<string[]>();

    private static DataHandler instance;

    public static DataHandler Instance


        {
          get
        {
                if (instance == null)
                {
                    instance = FindObjectOfType(typeof(DataHandler)) as DataHandler;
                    if (instance == null)
                    {
                        GameObject obj = new GameObject("DataHandler");
                        instance = obj.AddComponent<DataHandler>();
                        DontDestroyOnLoad(obj);
                    }
                }
                return instance;
            }
        }

    void Start()
    {
        InitializeRecordFirstRow();
    }


    public void SaveTaskDataToCSV()
    {
        string[][] output = new string[RecordData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = RecordData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));

        string filePath = getPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();

    }

    

    public void InitializeRecordFirstRow()
    {
        // Creating First row of titles manually..
        string[] rowDataTemp = new string[17];
        rowDataTemp[0] = "ID";
        rowDataTemp[1] = "SubectID";
        rowDataTemp[2] = "OperationShowTime";
        rowDataTemp[3] = "HandTouchTime";
        rowDataTemp[4] = "OperationTriggerTime";
        rowDataTemp[5] = "OperationFinishTimeSpan";
        rowDataTemp[6] = "TouchToTriggerTimeSpan";
        rowDataTemp[7] = "OperationResult";
        rowDataTemp[8] = "TargetButtonPosition";
        rowDataTemp[9] = "TriggeredButtonPosition";
        rowDataTemp[10] = "TargetButtonType";
        rowDataTemp[11] = "TriggeredButtonType";
        rowDataTemp[12] = "AudioOn";
        rowDataTemp[13] = "VibrateOn";
        rowDataTemp[14] = "ColorChangeOn";
        rowDataTemp[15] = "Shuffle";
        rowDataTemp[16] = "Sequence";
        RecordData.Add(rowDataTemp);
    }

    public void AddOneRecord(Record record )
    {
        string[] rowDataTemp = new string[17];
         rowDataTemp[0] = RecordData.Count.ToString();
         rowDataTemp[1] =  record.SubjectID;
         rowDataTemp[2] =  record.OperationShowTime;
         rowDataTemp[3] =  record.HandTouchTime;
         rowDataTemp[4] =  record.OperationTriggerTime;
         rowDataTemp[5] =  record.OperationFinishTimeSpan;
         rowDataTemp[6] =  record.TouchToTriggerTimeSpan;
         rowDataTemp[7] =  record.OperationResult;
         rowDataTemp[8] =  record.TargetButtonPosition;
         rowDataTemp[9]=  record.TriggeredButtonPosition;
         rowDataTemp[10]=  record.TargetButtonType;
         rowDataTemp[11]=  record.TriggeredButtonType;

        if (TouchPanel.Instance.CurrentTask.Audio)
            rowDataTemp[12] = "ON";
        else rowDataTemp[12] = "OFF";

        if (TouchPanel.Instance.CurrentTask.Vibrate)
            rowDataTemp[13] = "ON";
        else rowDataTemp[13] = "OFF";


        if (TouchPanel.Instance.CurrentTask.ColorChange)
            rowDataTemp[14] = "ON";
        else rowDataTemp[14] = "OFF";

        if (TouchPanel.Instance.CurrentTask.Shuffle)
            rowDataTemp[15] = "ON";
        else rowDataTemp[15] = "OFF";

         rowDataTemp[16] = record.Sequence;
         RecordData.Add(rowDataTemp);
    }


    private string getPath()
    {
#if UNITY_EDITOR

        string handDetails;
        if (TouchPanel.Instance.CurrentTask.RightHand)
        {
            handDetails = "RightHand";
        } else
            handDetails = "LeftHand";
        return Application.dataPath + "/CSV/" +  DateTime.Now.ToLongTimeString().Replace(':','_') + "_" +  DateTime.Now.ToLongDateString()+ "_" +    TouchPanel.Instance.CurrentTask.OperationNum + "_" + handDetails + "_SavedData.csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
#else
        return Application.dataPath +"/"+"Saved_data.csv";
#endif
    }
}
