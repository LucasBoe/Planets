using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] LevelUI levelUIElementPrefab;
    List<LevelData> levelDatas;
    List<LevelUI> levelUIs;

    // Start is called before the first frame update
    void Start()
    {
        CreateUIElementsForAllLevels();
    }

    private void CreateUIElementsForAllLevels()
    {
        var textures = Resources.LoadAll("", typeof(LevelData));

        levelDatas = new List<LevelData>();
        levelUIs = new List<LevelUI>();

        foreach (LevelData data in textures)
        {
            levelDatas.Add(data);
            levelUIs.Add(CreateLevelUIFor(data));
        }
    }

    private LevelUI CreateLevelUIFor(LevelData data)
    {
        LevelUI ui = Instantiate(levelUIElementPrefab, transform);
        ui.Setup(data);
        return ui;
    }

    internal void Fokus(LevelUI levelUI, LevelData data)
    {
        (transform as RectTransform).anchoredPosition = new Vector2(-((levelUI.transform as RectTransform).anchoredPosition.x), 0);

        foreach (var ui in levelUIs)
        {
            ui.SetFokused(levelUI == ui);
        }
    }

    internal void Play(LevelData data)
    {
        SceneManager.LoadScene(data.Scene.buildIndex);
    }
}
