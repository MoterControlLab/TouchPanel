using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VRTK;
using System;
using Newtonsoft.Json;
using VRTK.Examples;

/// <summary>
/// The Singleton Class of the project
/// </summary>
public class TouchPanel : MonoBehaviour
{

    //the text shows in the order board
    public TextMeshProUGUI NoticeText;
    //the Task class current test used
    public Task CurrentTask;
    //the order index
    private int currentOperationIndex;
    //used for the model of hand, will be invisible in default VRTK settting
    public GameObject LeftHand;
    public GameObject RightHand;
    //outline color change shader
    public Material OutlineMaterial;
    //original shader of hand model
    public Material HandInitialMaterial;

    static TouchPanel instance;
    //begin to record data for button max value or min value reach event data save trigger
    [HideInInspector]
    public bool BeginStoreData;
    //whether is touching toggle button
    [HideInInspector]
    public bool TouchToggle;
    //the name of button currently touching
    [HideInInspector]
    public string LastTriggerButtonName;
    //whether grip button is clicked
    [HideInInspector]
    public bool GripButtonClicked;
    //whether trigger button is clicked
    [HideInInspector]
    public bool TriggerButtonClicked;
    //the hashcode of current toggle button
    [HideInInspector]
    public int CurrentToggleHashCode;

    [HideInInspector]
    //will be used in VRTK_RotateTransformGrabAttach controllerAttachPoint
    public GameObject ControllerAnchor;
    //used in InitializeStartGrabParameterByTouch, which is used in toggle button lever grabbyattach
    [HideInInspector]
    public GameObject ControllerAlias;
    public AudioController AudioController;
    //whether current clicked button is reset
    [HideInInspector]
    public bool IsResetForCurrentButton;
    //used in virbate
    [HideInInspector]
    public List<VRTK_InteractHaptics> HapticsList = new List<VRTK_InteractHaptics>();
    //whether the task is finished
    [HideInInspector]
    public bool FinishTask;
    //whether the hand model is attaching the button
    [HideInInspector]
    public bool IsAttachingWithHand;
    [HideInInspector]
    public GameObject CurrentIndexContactPoint;
    //used in the case that the order hasn't been generated but hand alreay touched the button
    [HideInInspector]
    public bool isFirstAttachRecorded;
    //three different button prefabs
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

    //str used in record.csv

    private string currentConfigL1PositionStr;
    private string currentConfigL2PositionStr;
    private string currentConfigL3PositionStr;
    private string currentConfigR1PositionStr;
    private string currentConfigR2PositionStr;
    private string currentConfigR3PositionStr;

    private string currentConfigL1ButtonTypeStr;
    private string currentConfigL2ButtonTypeStr;
    private string currentConfigL3ButtonTypeStr;
    private string currentConfigR1ButtonTypeStr;
    private string currentConfigR2ButtonTypeStr;
    private string currentConfigR3ButtonTypeStr;

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

        if (!CurrentTask.RightHand)
        {
            LeftHand.transform.parent.gameObject.SetActive(true);
            LeftHand.SetActive(true);
        }
         
        else
        {
            RightHand.transform.parent.gameObject.SetActive(true);
            RightHand.SetActive(true);
        }
        
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

       
        //  yield return new WaitForSeconds(1f);
        yield return new WaitUntil(()=>IsResetForCurrentButton);
        BeginStoreData = true;
        if (currentOperationIndex < CurrentTask.CurrentCodeList.Count)
        {
            NoticeText.color = Color.white;
            AudioController.PlayEventSFX(AudioController.SpawnSFX);

            CurrentTask.ShowOperationCode(CurrentTask.CurrentCodeList[currentOperationIndex]);

            Debug.Log("*************************************************************************************    " + currentOperationIndex);
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
    /// <summary>
    /// Initialize layout of the panel based on configuration
    /// </summary>
    public void InitializeLayout()
    {

        var serialized = JsonConvert.SerializeObject(CurrentTask.Config.value);
        var currentConfig = JsonConvert.DeserializeObject<Configuration>(serialized);
        currentConfig.GetDetails();

       GenerateButton(currentConfig.L1ButtonStr, "L1", currentConfig.L1PositionStr);
       GenerateButton(currentConfig.L2ButtonStr, "L2", currentConfig.L2PositionStr);
       GenerateButton(currentConfig.L3ButtonStr, "L3", currentConfig.L3PositionStr);
       GenerateButton(currentConfig.R1ButtonStr, "R1", currentConfig.R1PositionStr);
       GenerateButton(currentConfig.R2ButtonStr, "R2", currentConfig.R2PositionStr);
       GenerateButton(currentConfig.R3ButtonStr, "R3", currentConfig.R3PositionStr);


        currentConfigL1PositionStr = currentConfig.L1PositionStr;
        currentConfigL2PositionStr = currentConfig.L2PositionStr;
        currentConfigL3PositionStr = currentConfig.L3PositionStr;
        currentConfigR1PositionStr = currentConfig.R1PositionStr;
        currentConfigR2PositionStr = currentConfig.R2PositionStr;
        currentConfigR3PositionStr = currentConfig.R3PositionStr;


        currentConfigL1ButtonTypeStr = currentConfig.L1ButtonStr;
        currentConfigL2ButtonTypeStr = currentConfig.L2ButtonStr;
        currentConfigL3ButtonTypeStr = currentConfig.L3ButtonStr;
        currentConfigR1ButtonTypeStr = currentConfig.R1ButtonStr;
        currentConfigR2ButtonTypeStr = currentConfig.R2ButtonStr;
        currentConfigR3ButtonTypeStr = currentConfig.R3ButtonStr;

        if (!CurrentTask.Vibrate)
        {

            for (int i = 0; i <HapticsList.Count; i++)
            {
                HapticsList[i].enabled = false;
            }
        }

    }

    public void GenerateButton(string buttonName, string index, string transform)
    {
        GameObject newButton;
        if (buttonName == "Toggle")
        {

            newButton = Instantiate(Toggle, GetTransform(index, transform).position, GetTransform(index, transform).rotation);
            newButton.GetComponentInChildren<ControllableReactor>().outputOnMin = index;
            HapticsList.Add(newButton.GetComponentInChildren<VRTK_InteractHaptics>());

        }


        if (buttonName == "Rotatory")
        {
            newButton = Instantiate(Rotatory, GetTransform(index, transform).position, GetTransform(index, transform).rotation);
            newButton.GetComponentInChildren<ControllableReactor>().outputOnMax = index;
            HapticsList.Add(newButton.GetComponentInChildren<VRTK_InteractHaptics>());
        }


        if (buttonName == "Pusher")
        {
            newButton = Instantiate(Pusher, GetTransform(index, transform).position, GetTransform(index, transform).rotation);
            newButton.GetComponentInChildren<ControllableReactor>().outputOnMax = index;
           HapticsList.Add(newButton.GetComponentInChildren<VRTK_InteractHaptics>());
        }
         
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

    public string GetButtonPosition(string positionStr)
    {
        if (positionStr == "L1")
            return positionStr + "_" + currentConfigL1PositionStr;

        if (positionStr == "L2")
            return positionStr + "_" + currentConfigL2PositionStr;
        if (positionStr == "L3")
            return positionStr + "_" + currentConfigL3PositionStr;
        if (positionStr == "R1")
            return positionStr + "_" + currentConfigR1PositionStr;

        if (positionStr == "R2")
            return positionStr + "_" + currentConfigR2PositionStr;
        if (positionStr == "R3")
            return positionStr + "_" + currentConfigR3PositionStr;

        return null;
    }

    public string GetButtonType(string positionStr)
    {
        if (positionStr == "L1")
            return currentConfigL1ButtonTypeStr;

        if (positionStr == "L2")
            return currentConfigL2ButtonTypeStr;
        if (positionStr == "L3")
            return currentConfigL3ButtonTypeStr;
        if (positionStr == "R1")
            return currentConfigR1ButtonTypeStr;

        if (positionStr == "R2")
            return currentConfigR2ButtonTypeStr;
        if (positionStr == "R3")
            return currentConfigR3ButtonTypeStr;

        return null;
    }
}
