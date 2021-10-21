namespace VRTK.Examples
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using VRTK.Controllables;
    using VRTK.Controllables.ArtificialBased;

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


            if (TouchPanel.Instance.FinishTask || !TouchPanel.Instance.BeginStoreData)
            {
                return;
            }

            if (outputOnMax != "")
            {
                if (!TouchPanel.Instance.IsResetForCurrentButton || !TouchPanel.Instance.isFirstAttachRecorded)
                {
                    return;
                }

                Record newRecord;
                TimeSpan finishtriggerdifferenceTime = DateTime.Now - TouchPanel.Instance.CurrentTask.CurrentOperationTime;
                TimeSpan touchtotriggerdifferenceTime = DateTime.Now - TouchPanel.Instance.CurrentTask.CurrentTouchTime;



                if (outputOnMax == TouchPanel.Instance.NoticeText.text)
                {
                    newRecord = new Record(TouchPanel.Instance.CurrentTask.SubjectID,
                                           TouchPanel.Instance.CurrentTask.CurrentOperationShowTime,
                                           TouchPanel.Instance.CurrentTask.TouchTime,
                                           DateTime.Now.ToString("hh:mm:ss:ff"),
                                           finishtriggerdifferenceTime.TotalSeconds.ToString(),
                                           touchtotriggerdifferenceTime.TotalSeconds.ToString(),
                                           "Success",  //result
                                           TouchPanel.Instance.GetButtonPosition(outputOnMax), //
                                           TouchPanel.Instance.GetButtonPosition(TouchPanel.Instance.NoticeText.text), //target pos
                                           TouchPanel.Instance.GetButtonType(outputOnMax),
                                           TouchPanel.Instance.GetButtonType(TouchPanel.Instance.NoticeText.text),//target buttontype
                                           TouchPanel.Instance.CurrentTask.Sequence
                                           );
                    TouchPanel.Instance.OperateRight();
                }
                else
                {



          

                    newRecord = new Record(TouchPanel.Instance.CurrentTask.SubjectID,
                         TouchPanel.Instance.CurrentTask.CurrentOperationShowTime,
                         TouchPanel.Instance.CurrentTask.TouchTime,
                         DateTime.Now.ToString("hh:mm:ss:ff"),
                         finishtriggerdifferenceTime.TotalSeconds.ToString(),
                         touchtotriggerdifferenceTime.TotalSeconds.ToString(),
                         "Fail",  //result
                         TouchPanel.Instance.GetButtonPosition(outputOnMax), //
                         TouchPanel.Instance.GetButtonPosition(TouchPanel.Instance.NoticeText.text), //target pos
                         TouchPanel.Instance.GetButtonType(outputOnMax),
                         TouchPanel.Instance.GetButtonType(TouchPanel.Instance.NoticeText.text),//target buttontype
                          TouchPanel.Instance.CurrentTask.Sequence
                         );
                    TouchPanel.Instance.OperateWrong();
                }

                DataHandler.Instance.AddOneRecord(newRecord);

                TouchPanel.Instance.IsResetForCurrentButton = false;

                if (controllable.gameObject.GetComponent<VRTK_ArtificialPusher>())
                {
                    controllable.gameObject.GetComponent<VRTK_ArtificialPusher>().stayPressed = true;
                }


               StartCoroutine(TouchPanel.Instance.GenerateNewCommond());
            }

            else
            {
                if (!TouchPanel.Instance.IsResetForCurrentButton)
                {

                    TouchPanel.Instance.IsResetForCurrentButton = true;
                }
            }
        }

   

        protected virtual void MinLimitReached(object sender, ControllableEventArgs e)
        {

            if (TouchPanel.Instance.FinishTask || !TouchPanel.Instance.BeginStoreData)
            {
                return;
            }


            if (outputOnMin != "")
            {
                if (!TouchPanel.Instance.IsResetForCurrentButton || !TouchPanel.Instance.isFirstAttachRecorded)
                {
                    return;
                }
                Record newRecord;
                TimeSpan finishtriggerdifferenceTime = DateTime.Now - TouchPanel.Instance.CurrentTask.CurrentOperationTime;
                TimeSpan touchtotriggerdifferenceTime = DateTime.Now - TouchPanel.Instance.CurrentTask.CurrentTouchTime;

                if (outputOnMin == TouchPanel.Instance.NoticeText.text)
                {


                    newRecord = new Record(TouchPanel.Instance.CurrentTask.SubjectID,
                                TouchPanel.Instance.CurrentTask.CurrentOperationShowTime,
                                TouchPanel.Instance.CurrentTask.TouchTime,
                                DateTime.Now.ToString("hh:mm:ss:ff"),
                                finishtriggerdifferenceTime.TotalSeconds.ToString(),
                                touchtotriggerdifferenceTime.TotalSeconds.ToString(),
                                "Success",  //result
                                TouchPanel.Instance.GetButtonPosition(outputOnMin), //
                                TouchPanel.Instance.GetButtonPosition(TouchPanel.Instance.NoticeText.text), //target pos
                                TouchPanel.Instance.GetButtonType(outputOnMin),
                                TouchPanel.Instance.GetButtonType(TouchPanel.Instance.NoticeText.text),//target buttontype
                                TouchPanel.Instance.CurrentTask.Sequence
                                );
                    TouchPanel.Instance.OperateRight();
                }
                else
                {

                    newRecord = new Record(TouchPanel.Instance.CurrentTask.SubjectID,
                                           TouchPanel.Instance.CurrentTask.CurrentOperationShowTime,
                                           TouchPanel.Instance.CurrentTask.TouchTime,
                                           DateTime.Now.ToString("hh:mm:ss:ff"),
                                           finishtriggerdifferenceTime.TotalSeconds.ToString(),
                                           touchtotriggerdifferenceTime.TotalSeconds.ToString(),
                                           "Fail",  //result
                                           TouchPanel.Instance.GetButtonPosition(outputOnMin), //
                                           TouchPanel.Instance.GetButtonPosition(TouchPanel.Instance.NoticeText.text), //target pos
                                           TouchPanel.Instance.GetButtonType(outputOnMin),
                                           TouchPanel.Instance.GetButtonType(TouchPanel.Instance.NoticeText.text),//target buttontype
                                           TouchPanel.Instance.CurrentTask.Sequence
                                           );
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
                    TouchPanel.Instance.IsResetForCurrentButton = true;
                }
            }
        }

    }
  

}