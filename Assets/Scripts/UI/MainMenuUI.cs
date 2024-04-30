using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.SceneManagement;
using ABKaspo.UI;
namespace ABKaspo.Game
{
    public class MainMenuUI : MonoBehaviour
    {
        public GameObject[] mainTabs;
        public GameObject[] configTabs;
        public TMP_Text hiUsername;
        public TMP_InputField playerNameField;
        bool isFirstJoin = true;
        void Awake()
        {
            isFirstJoin = SaveManager.LoadPlayerConfig().firstJoin;
            hiUsername.text = "Hi " + SaveManager.LoadPlayerConfig().playerName + "!";
        }
        void Start()
        {
            EnableSettingsPanelByName("Input");
            EnableTabPanelByName("Game");

            if (isFirstJoin == true)
            {
                Dialogue.instance
                    .SetTitle("Warning")
                    .SetMessage("This game contains mature content suitable only for adults aged 18 and older. Player discretion is advised.")
                    //.EnableCustomButton(true)
                    //.SetCustomButtonText("Quit")
                    //.SetCustomFunction()
                    //.SetTitleBackgroundColor(new Color(0.769f, 0f, 0f, 1f))
                    //.SetTitleBackgroundColor(new Color(0.1f, 0.1f, 0.1f, 0.5019608f))
                    .SetAnswerType(AnswerType.Information)
                    .SetPopupType(PopupType.Warning)
                    .OnClose(() => SaveFirstJoin())
                    .Show();
            }
        }
        void Update()
        {
            
        }
        public void InfoSetPlayerName()
        {
            Notification.instance.SetTitle("About option")
                .SetMessage("This function will be your player name in the game. It doesn't change the experience. The default player name is 'Player'")
                .SetPopupType(PopupType.Message)
                .Show();
        }
        public void SaveAndReloadPlayerName()
        {
            PlayerConfig config = SaveManager.LoadPlayerConfig();
            config.playerName = playerNameField.text;
            SaveManager.SavePlayerConfig(config);
            hiUsername.text = "Hi " + SaveManager.LoadPlayerConfig().playerName + "!";
        }
        public void QuitGame()
        {
            Dialogue.instance
                .SetTitle("Wait")
                .SetMessage("You are leaving this game. Are you sure?")
                //.SetTitleBackgroundColor(new Color(0.769f, 0f, 0f, 1f))
                //.SetMessageBackgroundColor(new Color(0.1f, 0.1f, 0.1f, 0.5019608f))
                .SetAnswerType(AnswerType.Question)
                .OnCancel(()=>CancelQuit())
                .OnClose(QuitGameFunc)
                .Show();
        }
        public void asdf()
        {
            Notification.instance
                .SetMessage("This function isn't developed yet! Make you sure that you have installed the last version!")
                .SetTitle("Warning")
                .SetPopupType(PopupType.Warning)
                .Show();
        }
        public void CancelQuit()
        {
            Debug.Log("Game: Quiting canceled");
            Application.wantsToQuit += () =>
            {
                return false;
            };
        }
        public void SaveFirstJoin()
        {
            PlayerConfig playerConfig = SaveManager.LoadPlayerConfig();
            playerConfig.firstJoin = false;

            SaveManager.SavePlayerConfig(playerConfig);
        }

        void Log(string conent)
        {
            Debug.Log(conent);
        }
        void QuitGameFunc()
        {
            Application.Quit();
            Debug.Log("Game: Game closed");
        }
        public void OpenURL(string url)
        {
            Dialogue.instance
                .SetAnswerType(AnswerType.Question)
                .SetTitle("Opening URL")
                .SetMessage("You are opening a URL! Are you sure you want to open it?")
                .SetPopupType(PopupType.Message)
                .OnClose(() => OpenURLFunction(url))
                .Show();    
        }
        private void OpenURLFunction(string url)
        {
            Application.OpenURL(url);
            Debug.Log("Game: URL oppened! (" + url + ")");
        }
        public static void ChangeScene(int sceneNumber)
        {
            SceneManager.LoadSceneAsync(sceneNumber);
        }
        public void ChangeLog()
        {
            EnableTabPanelByName("Settings");
            EnableSettingsPanelByName("Version Info");
        }

        public void EnableSettingsPanelByName(string name)
        {
            foreach (GameObject configTab in configTabs)
            {
                if (configTab.name == name)
                {
                    configTab.SetActive(true);
                }
                else
                {
                    configTab.SetActive(false);
                }
            }
        }
        public void EnableTabPanelByName(string name)
        {
            foreach (GameObject configTab in mainTabs)
            {
                if (configTab.name == name)
                {
                    configTab.SetActive(true);
                }
                else
                {
                    configTab.SetActive(false);
                }
            }
        }
    }
}