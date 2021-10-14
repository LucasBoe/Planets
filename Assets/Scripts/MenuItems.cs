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

        newData.AstronautsMax = GameObject.FindObjectsOfType<Astronaut>().Length;
        newData.Name = activeScene.name;

        bool sceneInBuildSettings = false;
        foreach (var scenes in EditorBuildSettings.scenes)
        {
            if (scenes.path == activeScene.path)
                sceneInBuildSettings = true;
        }

        if (!sceneInBuildSettings)
        {
            List<EditorBuildSettingsScene> scenesInBuild = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
            scenesInBuild.Add(new EditorBuildSettingsScene(activeScene.path, true));
            EditorBuildSettings.scenes = scenesInBuild.ToArray();
        }

        newData.SceneInBuildIndex = activeScene.buildIndex;

        GameObject.FindObjectOfType<LevelHandler>().LevelData = newData;

        AssetDatabase.CreateAsset(newData, "Assets/Resources/LEVEL_" + newData.Name + ".asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = newData;
    }
}
