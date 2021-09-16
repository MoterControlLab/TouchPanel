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
        string[] rowDataTemp = new string[10];
        rowDataTemp[0] = "ID";
        rowDataTemp[1] = "OperationShowTime";
        rowDataTemp[2] = "OperationTriggerTime";
        rowDataTemp[3] = "MovementTime";
        rowDataTemp[4] = "OperationResult";
        rowDataTemp[5] = "TriggeredButtonName";
        rowDataTemp[6] = "TargetButtonName";
        rowDataTemp[7] = "AudioOn";
        rowDataTemp[8] = "VibrateOn";
        rowDataTemp[9] = "ColorChangeOn";
        RecordData.Add(rowDataTemp);
    }

    public void AddOneRecord(Record record )
    {
        string[] rowDataTemp = new string[10];
        rowDataTemp[0] = RecordData.Count.ToString();
        rowDataTemp[1] =  record.OperationShowTime;
        rowDataTemp[2] =  record.OperationTriggerTime;
        rowDataTemp[3] =  record.MovementTime;
        rowDataTemp[4] =  record.OperationResult;
        rowDataTemp[5] =  record.TriggeredButtonName;
        rowDataTemp[6] =  record.TargetButtonName;
        rowDataTemp[7] =  record.AudioOn;
        rowDataTemp[8] = record.VibrateOn;
        rowDataTemp[9] = record.ColorChangeOn;
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
