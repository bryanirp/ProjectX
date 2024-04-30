using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ABKaspo.UI;
using UnityEngine.Events;
namespace ABKaspo.Game
{
    public class IntroManager : MonoBehaviour
    {
        void Start()
        {
            Dialogue.instance
                .SetTitle("Warning")
                .SetMessage("This game contains sensitive and sexual content intended for adult audiences. Do you want to open the game?")
                .SetTitleBackgroundColor(new Color(175/255f, 0/255f, 0/255f, 255/255f))
                .SetAnswerType(AnswerType.Question)
                .SetPopupType(PopupType.Warning)
                .OnClose(() => LoadScene())
                .OnCancel(() => CancelGame())
                .Show();
            Dialogue.instance.title.color = Color.white;
        }
        void LoadScene()
        {
            SceneManager.LoadScene(1);
        }
        void CancelGame()
        {
            Application.Quit();
            Debug.Log("Game: Application closed");
        }
    }
}