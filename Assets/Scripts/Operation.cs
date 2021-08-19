using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operation : MonoBehaviour
{
    // Start is called before the first frame update

    public string OpName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Operate()
    {
        if (OpName == TouchPanel.Instance.NoticeText.text)
        {
            OperateRight();
        }
        else
        {
            OperateWrong();
        }
    }
    public void OperateRight()
    {
        TouchPanel.Instance.GenerateNewCommond();
    }


    public void OperateWrong()
    {
        TouchPanel.Instance.GenerateNewCommond();
    }
}
