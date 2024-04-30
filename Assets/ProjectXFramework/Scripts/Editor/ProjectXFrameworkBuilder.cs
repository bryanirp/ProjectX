using UnityEngine;
using UnityEditor;
using System.IO;

namespace ProjectXFramework
{
    public class ProjectXFrameworkBuilder : EditorWindow
    {
        private string modFolderPath = "";

        [MenuItem("Window/ProjectXFramework/Build Mod")]
        public static void ShowWindow()
        {
            GetWindow<ProjectXFrameworkBuilder>("Build Mod");
        }

        private void OnGUI()
        {
            GUILayout.Label("ProjectXFramework - Build Mod", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField("Mod Folder Path:", modFolderPath);
            if (GUILayout.Button("Browse"))
            {
                modFolderPath = EditorUtility.OpenFolderPanel("Select Mod Folder", "", "");
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Build Mod"))
            {
                BuildMod();
            }
        }

        private void BuildMod()
        {
            // Validate if a folder is selected
            if (string.IsNullOrEmpty(modFolderPath))
            {
                Debug.LogError("Select a folder for the mod.");
                return;
            }

            // Get the path to the modinfo.json file
            string modInfoPath = Path.Combine(modFolderPath, "modinfo.json");

            // Validate if modinfo.json exists
            if (!File.Exists(modInfoPath))
            {
                Debug.LogError("modinfo.json not found in the mod folder.");
                return;
            }

            // Read modinfo.json to extract mod information
            string modInfoJson = File.ReadAllText(modInfoPath);
            ModInfo modInfo = JsonUtility.FromJson<ModInfo>(modInfoJson);

            // Generate the mod DLL name
            string modDllName = $"{modInfo.modid}.dll";

            // Output folder path for the mod
            string outputFolderPath = Path.Combine(Application.dataPath, "Mods");

            // Directory for the specific mod
            string modOutputPath = Path.Combine(outputFolderPath, modDllName);

            // Check if the output directory already exists
            if (Directory.Exists(modOutputPath))
            {
                Debug.LogWarning("The output directory for the mod already exists.");
                return;
            }

            // Transform the mod folder into a DLL
            string[] files = Directory.GetFiles(modFolderPath, "*", SearchOption.AllDirectories);
            AssetDatabase.ExportPackage(files, modOutputPath, ExportPackageOptions.Recurse);

            Debug.Log("Mod built successfully.");
        }

        // Class to hold mod information from modinfo.json
        [System.Serializable]
        public class ModInfo
        {
            public string modname;
            public string modid;
            public string modversion;
            public string description;
            public string[] urls;
            public string[] images;
            public string[] authors;
        }
    }
}
