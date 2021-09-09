using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VRTK;
using System;
using Newtonsoft.Json;

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


    [Header("Buttons")]
    public GameObject Toggle;
    public GameObject Rotatory;
    public GameObject Pusher;

    [Header("TopPositions")]
    public Transform L3TopTransform;
    public Transform L2TopTransform;
    public Transform L1TopTransform;
    public Transform R1TopTransform;
    public Transform R2TopTransform;
    public Transform R3TopTransform;

    [Header("MiddlePositions")]
    public Transform L3MiddleTransform;
    public Transform L2MiddleTransform;
    public Transform L1MiddleTransform;
    public Transform R1MiddleTransform;
    public Transform R2MiddleTransform;
    public Transform R3MiddleTransform;


    [Header("BottomPositions")]
    public Transform L3BottomTransform;
    public Transform L2BottomTransform;
    public Transform L1BottomTransform;
    public Transform R1BottomTransform;
    public Transform R2BottomTransform;
    public Transform R3BottomTransform;

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
        InitializeLayout();

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

    public void InitializeLayout()
    {

        var serialized = JsonConvert.SerializeObject(CurrentTask.Config.value);
        var currentConfig = JsonConvert.DeserializeObject<Configuration>(serialized);
        currentConfig.GetDetails();

       GenerateButton(currentConfig.L1ButtonStr, "L1", currentConfig.L1PositionStr);
       GenerateButton(currentConfig.L2ButtonStr, "L2", currentConfig.L1PositionStr);
       GenerateButton(currentConfig.L3ButtonStr, "L3", currentConfig.L3PositionStr);
       GenerateButton(currentConfig.R1ButtonStr, "R1", currentConfig.R1PositionStr);
       GenerateButton(currentConfig.R2ButtonStr, "R2", currentConfig.R1PositionStr);
       GenerateButton(currentConfig.R3ButtonStr, "R3", currentConfig.R3PositionStr);

    }

    public void GenerateButton(string buttonName, string index, string transform)
    {


        if (buttonName == "Toggle" )   Instantiate(Toggle, GetTransform(index, transform).position, GetTransform(index, transform).rotation);

        if (buttonName == "Rotatory")  Instantiate(Rotatory, GetTransform(index, transform).position, GetTransform(index, transform).rotation);

        if (buttonName == "Pusher")    Instantiate(Pusher, GetTransform(index, transform).position, GetTransform(index, transform).rotation);

    }


    public Transform GetTransform(string index, string transform)
    {
        if (index == "L1" && transform == "Top")    return L1TopTransform;
        if (index == "L1" && transform == "Middle") return L1MiddleTransform;
        if (index == "L1" && transform == "Bottom") return L1BottomTransform;

        if (index == "L2" && transform == "Top")    return L2TopTransform;
        if (index == "L2" && transform == "Middle") return L2MiddleTransform;
        if (index == "L2" && transform == "Bottom") return L2BottomTransform;

        if (index == "L3" && transform == "Top")    return L3TopTransform;
        if (index == "L3" && transform == "Middle") return L3MiddleTransform;
        if (index == "L3" && transform == "Bottom") return L3BottomTransform;


        if (index == "R1" && transform == "Top")    return R1TopTransform;
        if (index == "R1" && transform == "Middle") return R1MiddleTransform;
        if (index == "R1" && transform == "Bottom") return R1BottomTransform;

        if (index == "R2" && transform == "Top")    return R2TopTransform;
        if (index == "R2" && transform == "Middle") return R2MiddleTransform;
        if (index == "R2" && transform == "Bottom") return R2BottomTransform;

        if (index == "R3" && transform == "Top")    return R3TopTransform;
        if (index == "R3" && transform == "Middle") return R3MiddleTransform;
        if (index == "R3" && transform == "Bottom") return R3BottomTransform;

        else return null;
    }
}
