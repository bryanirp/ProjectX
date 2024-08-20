using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI
{
    public enum PopupType
    {
        Message = 0,
        Warning = 1,
        Error = 2
    }
    public enum AnswerType
    {
        Information = 1,
        Question = 2
    }
    public class DialogData
    {
        public string title = "Example Title";
        public string message = "Example Message.";
        public Color panelTitleColor = new Color(41f / 255f, 136f / 255f, 224f / 255f, 1f);
        public Color panelMessageColor = new Color(44f / 255f, 44f / 255f, 44f / 255f, 1f);
        public PopupType popupType = PopupType.Message;
        public AnswerType answerType = AnswerType.Information;
        public UnityAction OnClose;
        public UnityAction OnCancel;
        public UnityAction OnDecline;
    }
    public class Dialogue : MonoBehaviour
    {
        #region Punlic Varianbles
        [Header("Canvas")]
        public CanvasGroup canvasGroup;
        [Header("Setting Appareance")]
        public TMP_Text title;
        public TMP_Text message;
        [Header("")]
        public RawImage titlePanel;
        public RawImage messagePanel;
        [Header("Button 1")]
        public Button button1;
        public TMP_Text button1Text;
        public GameObject button1GameObject;
        [Header("Button 2")]
        public Button button2;
        public TMP_Text button2Text;
        public GameObject button2GameObject;
        [Header("Button 3")]
        public Button button3;
        public TMP_Text button3Text;
        public GameObject button3GameObject;
        [Header("Cancel Button")]
        public Button cancelButton;
        //public TMP_Text cancelButtonText; -> There's no text on Cancel Button
        [Header("Type")]
        /*public AnswerType answerType = AnswerType.Information;
        public PopupType popupType = PopupType.Message;*/
        public Image typePanel;
        [Header("")]
        public Sprite messType;
        public Sprite warnType;
        public Sprite errType;
        [Header("")]
        [HideInInspector] public bool IsActive = false;
        #endregion
        #region Private Variables
        DialogData dialog = new DialogData();
        DialogData tempDialog;
        Queue<DialogData> dialogsQueue = new Queue<DialogData>();
        #endregion

        public static Dialogue instance;
        #region This script Functions
        // --------------------------------------
        public Dialogue SetTitle(string title)
        {
            dialog.title = title;
            return instance;
        }
        public Dialogue SetMessage(string message)
        {
            dialog.message = message;
            return instance;
        }
        public Dialogue OnClose(UnityAction action)
        {
            dialog.OnClose = action;
            return instance;
        }
        public Dialogue OnCancel(UnityAction action)
        {
            dialog.OnCancel = action;
            return instance;
        }
        public Dialogue OnDecline(UnityAction action)
        {
            dialog.OnDecline = action;
            return instance;
        }
        public Dialogue SetTitleBackgroundColor(Color color)
        {
            dialog.panelTitleColor = color;
            return instance;
        }
        public Dialogue SetMessageBackgroundColor(Color color)
        {
            dialog.panelMessageColor = color;
            return instance;
        }
        public Dialogue SetPopupType(PopupType popupType)
        {
            dialog.popupType = popupType;
            return instance;
        }
        public Dialogue SetAnswerType(AnswerType answerType)
        {
            dialog.answerType = answerType;
            return instance;
        }
        // --------------------------------------

        // Show popup
        public void Show()
        {
            dialogsQueue.Enqueue(dialog);


            dialog = new DialogData();
            gameObject.SetActive(true);
            if (!IsActive)
            {
                ShowNextDialog();
            }
        }
        // Hide popup
        public void Hide()
        {
            gameObject.SetActive(false);
            IsActive = false;

            if (tempDialog != null && tempDialog.OnClose != null)
            {
                tempDialog.OnClose.Invoke();
            }

            StopAllCoroutines();

            if (dialogsQueue.Count != 0)
            {
                ShowNextDialog();
            }
        }

        public void OnCancel()
        {
            gameObject.SetActive(false);
            IsActive = false;

            if (tempDialog != null && tempDialog.OnCancel != null)
            {
                tempDialog.OnCancel.Invoke();
            }

            StopAllCoroutines();

            if (dialogsQueue.Count != 0)
            {
                ShowNextDialog();
            }
        }
        public void OnDecline()
        {
            gameObject.SetActive(false);
            IsActive = false;

            if (tempDialog != null && tempDialog.OnDecline != null)
            {
                tempDialog.OnDecline.Invoke();
            }

            StopAllCoroutines();

            if (dialogsQueue.Count != 0)
            {
                ShowNextDialog();
            }
        }
        // Show next popup
        void ShowNextDialog()
        {
            tempDialog = dialogsQueue.Dequeue();

            titlePanel.color = tempDialog.panelTitleColor;
            messagePanel.color = tempDialog.panelMessageColor;
            title.text = tempDialog.title;
            message.text = tempDialog.message;
            if (tempDialog.popupType == PopupType.Message) typePanel.sprite = messType;
            if (tempDialog.popupType == PopupType.Warning) typePanel.sprite = warnType;
            if (tempDialog.popupType == PopupType.Error) typePanel.sprite = errType;

            if (tempDialog.answerType == AnswerType.Question)
            {
                button1GameObject.SetActive(true);
                button2GameObject.SetActive(true);
                button3GameObject.SetActive(true);
            }
            if (tempDialog.answerType == AnswerType.Information)
            {
                button1GameObject.SetActive(true);
                button2GameObject.SetActive(false);
                button3GameObject.SetActive(false);
            }

            gameObject.SetActive(true);
            IsActive = true;
            StartCoroutine(FadeIn(0.3f));

        }
        // Animation
        IEnumerator FadeIn(float duration)
        {
            float startTime = Time.time;
            float alpha = 0f;

            while (alpha < 1f)
            {
                alpha = Mathf.Lerp(0f, 1f, (Time.time - startTime) / duration);
                canvasGroup.alpha = alpha;

                yield return null;
            }
        }

        private void SetQuestionTypeImage()
        {

        }
        #endregion
        #region Unity Functions
        void Awake()
        {
            instance = this;
            button1.onClick.RemoveAllListeners();
            button1.onClick.AddListener(Hide);

            button2.onClick.RemoveAllListeners();
            button2.onClick.AddListener(OnDecline);

            button3.onClick.RemoveAllListeners();
            button3.onClick.AddListener(OnCancel);
            cancelButton.onClick.RemoveAllListeners();
            cancelButton.onClick.AddListener(OnCancel);
            Hide();
        }
        #endregion
    }
}