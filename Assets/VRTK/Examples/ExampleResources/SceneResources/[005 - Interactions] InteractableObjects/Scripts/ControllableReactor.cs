namespace VRTK.Examples
{
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

           // if (TouchPanel.Instance.FinishTask)
           // {
           //     return;
           // }

            if (outputOnMax != "")
            {
                if (!TouchPanel.Instance.IsResetForCurrentButton)
                {
                    return;
                }
     
                if (outputOnMax == TouchPanel.Instance.NoticeText.text)
                {
                    TouchPanel.Instance.OperateRight();
                }
                else TouchPanel.Instance.OperateWrong();

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

   

        protected virtual void MinLimitReached(object sender, ControllableEventArgs e)
        {

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

                if (outputOnMin == TouchPanel.Instance.NoticeText.text)
                {
                    TouchPanel.Instance.OperateRight();
                }
                else TouchPanel.Instance.OperateWrong();

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