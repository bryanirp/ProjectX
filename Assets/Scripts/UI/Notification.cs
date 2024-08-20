using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI
{
    public class NotificationData
    {
        public string title = "Example title";
        public string content = "Example content";
        public Color contentBackgroundColor = new Color(42 / 255f, 42 / 255f, 42 / 255f, 255 / 255);
        public Color titleBackgroundColor = new Color(95 / 255f, 95 / 255f, 95 / 255f, 255 / 255f);
        public float duration = 2.5f;
        public UnityAction OnClose;
        public PopupType popupType = PopupType.Message;
        public float notfDuration = 10.0f;
    }
    public class Notification : MonoBehaviour
    {
        #region Public Variables
        [Header("Canvas")]
        public CanvasGroup canvasGroup;
        [Header("Setting Appareance")]
        public TMP_Text title;
        public TMP_Text message;
        [Header("")]
        public RawImage titlePanel;
        public RawImage messagePanel;
        [Header("Type")]
        public Image typePanel;
        [Header("")]
        public Sprite messType;
        public Sprite warnType;
        public Sprite errType;
        [HideInInspector] public bool IsActive = false;
        #endregion
        #region Private Variables
        NotificationData notification = new NotificationData();
        NotificationData tempNotf;
        Queue<NotificationData> notfQueue = new Queue<NotificationData>();
        [HideInInspector] public bool isAnimating = false;
        private Vector3 positionA;
        private Vector3 positionB;
        #endregion

        public static Notification instance;
        #region This script Functions
        // --------------------------------------
        public Notification OnClose(UnityAction action)
        {
            notification.OnClose = action;
            return instance;
        }
        public Notification SetTitle(string content)
        {
            notification.title = content;
            return instance;
        }
        public Notification SetMessage(string content)
        {
            notification.content = content;
            return instance;
        }
        public Notification SetTileBackgroundColor(Color color)
        {
            notification.titleBackgroundColor = color;
            return instance;
        }
        public Notification SetMessageBackgroundColor(Color color)
        {
            notification.contentBackgroundColor = color;
            return instance;
        }
        public Notification SetPopupType(PopupType popupType)
        {
            notification.popupType = popupType;
            return instance;
        }
        /*public Notification SetDuration(float duration)
        {
            notification.duration = duration;
            return instance;
        }*/
        // --------------------------------------
        public void Show()
        {
            notfQueue.Enqueue(notification);

            notification = new NotificationData();
            if (!IsActive)
            {
                ShowNextNotification();
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            IsActive = false;

            if (tempNotf != null && tempNotf.OnClose != null)
            {
                tempNotf.OnClose.Invoke();
            }

            StopAllCoroutines();

            if (notfQueue.Count != 0)
            {
                ShowNextNotification();
            }
        }
        IEnumerator ShowNextNotificationAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            ShowNextNotification();
        }
        void ShowNextNotification()
        {
            tempNotf = notfQueue.Dequeue();

            titlePanel.color = tempNotf.titleBackgroundColor;
            messagePanel.color = tempNotf.contentBackgroundColor;
            title.text = tempNotf.title;
            message.text = tempNotf.content;
            if (tempNotf.popupType == PopupType.Message) typePanel.sprite = messType;
            if (tempNotf.popupType == PopupType.Warning) typePanel.sprite = warnType;
            if (tempNotf.popupType == PopupType.Error) typePanel.sprite = errType;

            gameObject.SetActive(true);
            IsActive = true;
            StartCoroutine(FadeIn(0.5f, tempNotf.duration));//, true));
        }
        IEnumerator FadeIn(float speed, float duration)//, bool show)
        {
            float startTime = Time.time;
            float alpha = 0f;

            Vector3 targetPosition = positionA;
            Vector3 initialPosition = positionB;

            while (alpha < 1f)
            {
                alpha = Mathf.Lerp(0f, 1f, (Time.time - startTime) / speed);
                messagePanel.rectTransform.anchoredPosition = Vector3.Lerp(initialPosition, targetPosition, alpha);
                canvasGroup.alpha = alpha;
                yield return null;
            }
            yield return new WaitForSeconds(duration);
            startTime = Time.time;

            while (alpha > 0f)
            {
                alpha = Mathf.Lerp(1f, 0f, (Time.time - startTime) / speed);
                canvasGroup.alpha = alpha;
                messagePanel.rectTransform.anchoredPosition = Vector3.Lerp(initialPosition, targetPosition, alpha);
                yield return null;
            }
            Hide();
        }

        #endregion

        #region Unity Functions
        void Awake()
        {
            instance = this;
            positionA = messagePanel.rectTransform.anchoredPosition;
            positionB.x = messagePanel.rectTransform.anchoredPosition.x + 495;
            positionB.y = messagePanel.rectTransform.anchoredPosition.y;
            Hide();
        }
        #endregion
        #region Example Notifications
        public void NoneAction()
        {
            instance.SetTitle("Function not developed")
                .SetMessage("Sorry, this function has not been developed yet.")
                .SetPopupType(PopupType.Warning)
                .OnClose(() => Debug.Log("Game: 'Function not developed' Notification shown"))
                .Show();
        }
        #endregion
    }
}