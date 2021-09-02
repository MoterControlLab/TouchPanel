using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public int OperationNum;

    [HideInInspector]
    public List<int> CurrentCodeList = new List<int>();

    public bool Vibrate;
    public bool Audio;
    // Start is called before the first frame update
    void Start()
    {
        if (!Vibrate)
        {
            for (int i = 0; i < TouchPanel.Instance.HapticsList.Count; i++)
            {
                TouchPanel.Instance.HapticsList[i].enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GenerateOperationSequence()
    {
        CurrentCodeList.Clear();
        for (int i = 0; i < OperationNum; i++)
        {
            int randomIndex = Random.Range(0, 6);
            CurrentCodeList.Add(randomIndex);
        }
    }

    public void ShowOperationCode(int codeIndex)
    {
       
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
