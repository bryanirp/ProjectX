using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using System;
using Desdinova;
namespace Game.UI
{
    public class GameDebug : MonoBehaviour
    {
        public GameObject gameDebugUI;
        public GameObject gameConsoleDebugUI;
        public Transform Rotation;
        public Transform Position;
        public ConsoleGUIController console;
        [Header("Game Debug UI")]
        public TMP_Text companyName;
        public TMP_Text gameName;
        public TMP_Text gameVersion;
        public TMP_Text gameScene;
        public TMP_Text gpuModel;
        public TMP_Text cpuModel;
        public TMP_Text ramUsage;
        public TMP_Text framerate;
        public TMP_Text PositionTXT;
        public TMP_Text RotationTXT;

        public static GameObject _gameDebugUI;
        public static GameObject _gameConsoleDebugUI;
        public static GameDebug instance;

        bool showGameConsoleDebugUI = false;
        bool showGameDebugUI = false;


        public GameDebug ShowConsole()
        {
            showGameConsoleDebugUI = console.ShowConsole;
            console.ShowConsole = !console.ShowConsole;

            if (showGameConsoleDebugUI == true) Debug.Log("Debug: Debug UI Console is enabled");
            if (showGameConsoleDebugUI == false) Debug.Log("Debug: Debug UI Console is unenabled");
            return instance;
        }
        public GameDebug ShowDebugUI()
        {
            showGameDebugUI = !showGameDebugUI;
            gameDebugUI.SetActive(showGameDebugUI);

            if (showGameDebugUI == true) Debug.Log("Debug: Debug UI is enabled");
            if (showGameDebugUI == false) Debug.Log("Debug: Debug UI is unenabled");
            return instance;
        }
        void Awake()
        {
            instance = this;
            _gameDebugUI = gameDebugUI;
            _gameConsoleDebugUI = gameConsoleDebugUI;
        }
        void Start()
        {
            companyName.text = Application.companyName;
            gameName.text = Application.productName;
            gameVersion.text = Application.version;
            cpuModel.text = SystemInfo.processorType;
            gpuModel.text = SystemInfo.graphicsDeviceName;
            gameScene.text = SceneManager.GetActiveScene().name + " (" + SceneManager.GetActiveScene().buildIndex + ")";

            _gameDebugUI.SetActive(show);

        }

        bool show = false;
        void Update()
        {
            ramUsage.text = (SystemInfo.systemMemorySize - SystemInfo.systemMemorySize * 0.1f) + " MB";
            PositionTXT.text = transform.transform.position.ToString();
            RotationTXT.text = transform.rotation.ToString();
            float fps = 1.0f / Time.deltaTime;
            framerate.text = MathF.Round(fps).ToString();

            if (Input.GetKeyDown(KeyCode.F3))
            {
                show = !show;
                gameDebugUI.SetActive(show);
            }
        }
    }
}