using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TouchPanel : MonoBehaviour
{


    public TextMeshProUGUI NoticeText;

    public int TestNum;
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
        TestNum--;
        int randomindex = Random.Range(0, 5);
        switch (randomindex)
        {
            case 0:
                NoticeText.text = "L1";
                break;
            case 1:
                NoticeText.text = "L2";
                break;
            case 2:
                NoticeText.text = "L3";
                break;
            case 3:
                NoticeText.text = "R1";
                break;
            case 4:
                NoticeText.text = "R2";
                break;
            case 5:
                NoticeText.text = "R3";
                break;

            default:
                break;
        }


    }


}
