using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace ABKaspo.UI
{
    public class MainMenu : MonoBehaviour
    {
        public Animator animatorMain;
        public Animator animatorSettings;
        bool isMainPanel = true;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public static void ChangeSceneByName(string name)
        {
            SceneManager.LoadScene(name);
        }
        public static void ChangeSceneByNumber(int sceneNumber)
        {
            SceneManager.LoadScene(sceneNumber);
        }
        public static void QuitGame()
        {
            Application.Quit();
            Debug.Log(Application.productName + ": Game closed!");
        }
        public void ChangeCanvas()
        {
            isMainPanel = !isMainPanel;
            if (isMainPanel == true)
            {
                animatorMain.SetBool("Show", true);
                animatorSettings.SetBool("Show", false);
            }
            else
            {
                animatorMain.SetBool("Show", false);
                animatorSettings.SetBool("Show", true);
            }
        }
    }
}