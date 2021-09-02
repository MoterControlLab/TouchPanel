using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VRTK;
using System;

public class TouchPanel : MonoBehaviour
{


    public TextMeshProUGUI NoticeText;

    public Task CurrentTask;
    private int currentOperationIndex;
    public GameObject LeftHand;
    public GameObject RightHand;
    static TouchPanel instance;
    public AudioController AudioController;
    //whether current clicked button is reset
    [HideInInspector]
    public bool IsResetForCurrentButton;

    public List<VRTK_InteractHaptics> HapticsList;
    [HideInInspector]
    public bool FinishTask;

    public static TouchPanel Instance

    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(TouchPanel)) as TouchPanel;
                if (instance == null)
                {
                    GameObject obj = new GameObject("TouchPanel");
                    instance = obj.AddComponent<TouchPanel>();
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LeftHand.SetActive(true);
        RightHand.SetActive(true);
        //initialize operation sequence
        CurrentTask.GenerateOperationSequence();
        StartTestButtonClick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTestButtonClick()
    {
        StartCoroutine(BeginTesting());
    }

    private IEnumerator BeginTesting()
    {
        NoticeText.text = "3";
        yield return new WaitForSeconds(1f);
        NoticeText.text = "2";
        yield return new WaitForSeconds(1f);
        NoticeText.text = "1";
        yield return new WaitForSeconds(1f);

        IsResetForCurrentButton = true;

       StartCoroutine( GenerateNewCommond());
    }


    

    public IEnumerator GenerateNewCommond()
    {
        yield return new WaitUntil(()=>IsResetForCurrentButton);
        if (currentOperationIndex < CurrentTask.CurrentCodeList.Count)
        {
            NoticeText.color = Color.white;
            AudioController.PlayEventSFX(AudioController.SpawnSFX);
            CurrentTask.ShowOperationCode(CurrentTask.CurrentCodeList[currentOperationIndex]);
            currentOperationIndex++;
        }

        else
        {
            NoticeText.color = Color.white;
            NoticeText.text = "Done!";
            FinishTask = true;
            DataHandler.Instance.SaveTaskDataToCSV();
        }

        yield break;
    }


    public void OperateRight()
    {
        NoticeText.color = Color.green;
        NoticeText.text = "Correct!";
        AudioController.PlayOperationSFX(AudioController.RightSFX);

    }

    public void OperateWrong()
    {
        NoticeText.color = Color.red;
        NoticeText.text = "Wrong!";
        AudioController.PlayOperationSFX(AudioController.WrongSFX);
    }
}
