namespace VRTK.Examples
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using VRTK.Controllables;

    public class ControllableReactor : MonoBehaviour
    {
        public VRTK_BaseControllable controllable;
        public Text displayText;
        public string outputOnMax = "Maximum Reached";
        public string outputOnMin = "Minimum Reached";

        protected virtual void OnEnable()
        {
            controllable = (controllable == null ? GetComponent<VRTK_BaseControllable>() : controllable);
            controllable.ValueChanged += ValueChanged;
            controllable.MaxLimitReached += MaxLimitReached;
            controllable.MinLimitReached += MinLimitReached;
        }

        protected virtual void ValueChanged(object sender, ControllableEventArgs e)
        {
            if (displayText != null)
            {
                displayText.text = e.value.ToString("F1");
            }
        }

        protected virtual void MaxLimitReached(object sender, ControllableEventArgs e)
        {
           

            if (TouchPanel.Instance.FinishTask)
            {
                return;
            }

          //  Debug.Log(transform.parent.name + "  1");

            if (outputOnMax != "")
            {
                if (!TouchPanel.Instance.IsResetForCurrentButton)
                {
                    return;
                }

                Record newRecord;
                TimeSpan differenceTime = DateTime.Now - TouchPanel.Instance.CurrentTask.CurrentOperationTime;

                if (outputOnMax == TouchPanel.Instance.NoticeText.text)
                {
                        
                    newRecord = new Record(TouchPanel.Instance.CurrentTask.CurrentOperationShowTime, DateTime.Now.ToLongTimeString(), differenceTime.TotalSeconds.ToString(),"Success", outputOnMax, TouchPanel.Instance.NoticeText.text);
                    TouchPanel.Instance.OperateRight();
                }
                else
                {

                    newRecord = new Record(TouchPanel.Instance.CurrentTask.CurrentOperationShowTime, DateTime.Now.ToLongTimeString(), differenceTime.TotalSeconds.ToString(), "Fail", outputOnMax, TouchPanel.Instance.NoticeText.text);
                    TouchPanel.Instance.OperateWrong();
                }

                DataHandler.Instance.AddOneRecord(newRecord);

                TouchPanel.Instance.IsResetForCurrentButton = false;

               StartCoroutine(TouchPanel.Instance.GenerateNewCommond());
            }

            else
            {
                if (!TouchPanel.Instance.IsResetForCurrentButton)
                {
                  //  Debug.Log(gameObject.name + " 1");
                    TouchPanel.Instance.IsResetForCurrentButton = true;
                }
            }
        }

   

        protected virtual void MinLimitReached(object sender, ControllableEventArgs e)
        {
           // Debug.Log(transform.parent.name + "  2");

            if (TouchPanel.Instance.FinishTask)
            {
                return;
            }


            if (outputOnMin != "")
            {
                if (!TouchPanel.Instance.IsResetForCurrentButton)
                {
                    return;
                }
                Record newRecord;
                TimeSpan differenceTime = DateTime.Now - TouchPanel.Instance.CurrentTask.CurrentOperationTime;

                if (outputOnMin == TouchPanel.Instance.NoticeText.text)
                {

                    newRecord = new Record(TouchPanel.Instance.CurrentTask.CurrentOperationShowTime, DateTime.Now.ToLongTimeString(), differenceTime.TotalSeconds.ToString(), "Success", outputOnMin, TouchPanel.Instance.NoticeText.text);
                    TouchPanel.Instance.OperateRight();
                }
                else
                {

                    newRecord = new Record(TouchPanel.Instance.CurrentTask.CurrentOperationShowTime, DateTime.Now.ToLongTimeString(), differenceTime.TotalSeconds.ToString(), "Fail", outputOnMin, TouchPanel.Instance.NoticeText.text);
                    TouchPanel.Instance.OperateWrong();
                } 

            
                DataHandler.Instance.AddOneRecord(newRecord);


                TouchPanel.Instance.IsResetForCurrentButton = false;

                StartCoroutine(TouchPanel.Instance.GenerateNewCommond());
            }

            else
            {
                if (!TouchPanel.Instance.IsResetForCurrentButton)
                {
                   // Debug.Log(gameObject.name + " 2");
                    TouchPanel.Instance.IsResetForCurrentButton = true;
                }
            }
        }
    }
  
}