using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TouchPanel : MonoBehaviour
{


    public TextMeshProUGUI NoticeText;

    public Task CurrentTask;
    private int currentOperationIndex;
    public GameObject LeftHand;
    public GameObject RightHand;
    static TouchPanel instance;
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
        yield return new WaitForSeconds(1);
        NoticeText.text = "2";
        yield return new WaitForSeconds(1);
        NoticeText.text = "1";
        yield return new WaitForSeconds(1);
        GenerateNewCommond();
    }

    public void GenerateNewCommond()
    {
        if (currentOperationIndex < CurrentTask.CurrentCodeList.Count)
        {
            NoticeText.color = Color.white;
            CurrentTask.ShowOperationCode(CurrentTask.CurrentCodeList[currentOperationIndex]);
            currentOperationIndex++;
        }
    }


    public void OperateRight()
    {
        NoticeText.color = Color.green;
        NoticeText.text = "Correct!";
    }

    public void OperateWrong()
    {
        NoticeText.color = Color.red;
        NoticeText.text = "Wrong!";
    }
}
