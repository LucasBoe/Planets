using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MenuItems
{
    [MenuItem("Levels/Create Level Data from acitve Scene")]
    private static void CreateLevelFile()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        LevelData newData = ScriptableObject.CreateInstance<LevelData>();

        newData.Scene = activeScene;
        newData.AstronautsMax = GameObject.FindObjectsOfType<Astronaut>().Length;
        newData.Name = activeScene.name;

        List<EditorBuildSettingsScene> scenesInBuild = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
        scenesInBuild.Add(new EditorBuildSettingsScene(activeScene.path, true));
        EditorBuildSettings.scenes = scenesInBuild.ToArray();

        AssetDatabase.CreateAsset(newData, "Assets/Resources/LEVEL_" + newData.Name + ".asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = newData;
    }
}
