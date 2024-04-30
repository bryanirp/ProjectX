using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using TMPro;
using ABKaspo.UI;
namespace ABKaspo.Game
{
    public class Settings : MonoBehaviour
    {
        private const string password = "Caca10##";

        public Toggle fullscreenTog, vsycnTog, rtxToggle;
        public TMP_Dropdown resolutionDropdown, fpsDromdown, windowMode;
        public List<FrameRate> fpsLists;

        public TMP_InputField debugPasswordField;
        public GameObject debugPasswordLocked, debugPassword;
        //public RTXManager rTXManager;
        Resolution[] resolutions;
        RenderingType renderingType;
        Camera mainCamera;

        // Set an instance for just a script
        public static Settings instance;
        private void Awake()
        {
            instance = this;
            mainCamera = Camera.main;
            // Set window mode
            windowMode.ClearOptions();
            windowMode.AddOptions(new List<string> { "Windowed", "Maximized Window" });

            // Limit FPS start
            fpsDromdown.ClearOptions();
            List<TMPro.TMP_Dropdown.OptionData> fpsOptions = new List<TMPro.TMP_Dropdown.OptionData>();
            foreach (var fpsItem in fpsLists)
            {
                fpsOptions.Add(new TMPro.TMP_Dropdown.OptionData(fpsItem.fps.ToString()));
            }
            fpsDromdown.AddOptions(fpsOptions);

            fpsDromdown.value = 5;
            fpsDromdown.RefreshShownValue();

            // Set RTX
            if (RTXManager.instance.renderingType == RenderingType.DR) rtxToggle.isOn = false;
            if (RTXManager.instance.renderingType == RenderingType.RTX) rtxToggle.isOn = true;

            // Set fullscren as fullscreen togle
            fullscreenTog.isOn = Screen.fullScreen;

            //Screen.SetResolution(1920, 1080, fullscreenTog.isOn);

            // Set vsync logic toggle
            if (QualitySettings.vSyncCount <= 0) vsycnTog.isOn = false;
            else vsycnTog.isOn = true;

            // Check Resolutuion
            CheckResolutions();

            // Apply Frame Rate
            ChangeFramerate();
            LoadSettings();
            
        }
        // Load Settings
        void LoadSettings()
        {
            PlayerConfig config = SaveManager.LoadPlayerConfig(); 
            fullscreenTog.isOn = config.fullscreen;
            vsycnTog.isOn = config.vsync;
            rtxToggle.isOn = config.rtx;
            resolutionDropdown.value =  FindResolutionIndex(config.resolution);
            resolutionDropdown.RefreshShownValue();
        }
        // Find Resoltion By index
        int FindResolutionIndex(Resolution resolution)
        {
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolutions[i].width == resolution.width && resolutions[i].height == resolution.height)
                {
                    return i;
                }
            }
            return 0; // Valor predeterminado si no se encuentra la resolución
        }
        private void Start()
        {
            
        }
        public void ApplyScreenOptions()
        {
            // Save Options
            PlayerConfig config = new PlayerConfig();
            config.fullscreen = fullscreenTog.isOn;
            config.vsync = vsycnTog.isOn;
            config.rtx = rtxToggle.isOn;
            config.resolution = resolutions[resolutionDropdown.value];

            SaveManager.SavePlayerConfig(config);
            // Set window mode from dropdown selection, if is fullscreen, ignore it
            if (fullscreenTog.isOn == false)
            {
                Resolution selectedResolution = resolutions[resolutionDropdown.value];
                Screen.SetResolution(selectedResolution.width, selectedResolution.height, fullscreenTog.isOn);

                string windowModeSelected = windowMode.options[windowMode.value].text;
                if (windowModeSelected == "Windowed")
                {
                    Screen.fullScreenMode = FullScreenMode.Windowed;
                }
                else if (windowModeSelected == "Maximized Window")
                {
                    Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                }
            }
            else
            {
                // Establecer el modo de pantalla a pantalla completa
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            }

            // Set Vsync
            QualitySettings.vSyncCount = vsycnTog.isOn ? 1 : 0;
            // Set FPS
            ChangeFramerate();

            Debug.Log("Game: Screen Option Applied!");
        }

        public void ApplyGraphicsOptions()
        {
            // Save Options
            PlayerConfig config = new PlayerConfig();
            config.fullscreen = fullscreenTog.isOn;
            config.vsync = vsycnTog.isOn;
            config.rtx = rtxToggle.isOn;
            config.resolution = resolutions[resolutionDropdown.value];

            SaveManager.SavePlayerConfig(config);

            // Apply Config To Game
            RenderingType renderingType = rtxToggle.isOn ? RenderingType.RTX : RenderingType.DR;
            RTXManager.instance.SetRenderingType(renderingType);
            RTXManager.instance.UpdateRendering();
            StartCoroutine(ResetCamera());
            Debug.Log("Game: Graphics Options Applied!");
        }
        private IEnumerator ResetCamera()
        {
            mainCamera.enabled = false;

            // Esperar un frame para asegurarnos de que la cámara se haya desactivado completamente
            yield return null;

            // Activar la cámara
            mainCamera.enabled = true;
        }
        void CheckResolutions()
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();
            int actualResolution = new int();

            for (int i = new int(); i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(option);

                /*if (Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    actualResolution = i;
                }*/
                actualResolution = i;
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = actualResolution;
            resolutionDropdown.RefreshShownValue();
        }
        public void ChangeScreenResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
        public void ChangeFramerate()
        {
            // Get the selected framerate option from the dropdown
            int selectedFramerate = int.Parse(fpsDromdown.options[fpsDromdown.value].text);

            // Apply the selected framerate
            if (QualitySettings.vSyncCount < 1)
            {
                Application.targetFrameRate = selectedFramerate;
                Debug.Log("Game: Target framerate changed to: " + selectedFramerate);
            }
            else if (QualitySettings.vSyncCount > 1) Debug.Log("Game: Vsync is on");
        }
        public void ValidatePassword()
        {
            if (debugPasswordField.text == password)
            {
                debugPassword.SetActive(true);
                debugPasswordLocked.SetActive(false);
            }
            else
            {

                debugPassword.SetActive(false);
                debugPasswordLocked.SetActive(true);
                debugPasswordField.text = "";
            }
        }
        public void ShowDebugUI(GameObject debugUI)
        {
            GameDebug.instance.ShowDebugUI();
        }
        public void ShowConsole()
        {
            GameDebug.instance.ShowConsole();
        }
    }
    [Serializable]
    public class FrameRate
    {
        public int fps;
    }
}