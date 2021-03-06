using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] LevelSelectionRocket rocket;
    [SerializeField] RectTransform rectToTranslate;
    [SerializeField] LevelUI levelUIElementPrefab;
    [SerializeField] RocketSettings rocketSettings;

    public static LevelData[] lds;

    List<LevelData> levelDatas;
    List<LevelUI> levelUIs;

    // Start is called before the first frame update
    void Start()
    {
        int toHighlight = CreateUIElementsForAllLevels();

        if (levelUIs != null && levelUIs.Count > 0)
        {
            rectToTranslate.anchoredPosition = CalculateTranslationPositonForLevelUIElement(toHighlight);
            rocket.selected = toHighlight;
            rocket.Teleport(toHighlight);
            levelUIs[toHighlight].SetFokused(true);
        }
    }

    private int CreateUIElementsForAllLevels()
    {
        if (lds == null)
        {
            var temp = Resources.LoadAll("", typeof(LevelData));
            List<LevelData> td = new List<LevelData>();
            foreach (LevelData data in temp)
            {
                td.Add(data);
            }
            lds = td.ToArray();
        }

        levelDatas = new List<LevelData>();
        levelUIs = new List<LevelUI>();

        int toHighlight = 0;

        for (int i = 0; i < lds.Length; i++)
        {
            LevelData data = lds[i] as LevelData;

            if (!data.Unlocked && i > 0 && (lds[i - 1] as LevelData).Finished)
                data.Unlocked = true;

            levelDatas.Add(data);
            levelUIs.Add(CreateLevelUIFor(data));


            if (data.ComingFromThatScene)
            {
                data.ComingFromThatScene = false;
                toHighlight = i;
                Debug.LogWarning("Coming from scene with id: " + i);
            }
        }

        return toHighlight;
    }

    private int FindIndexToHighlight()
    {
        for (int i = 0; i < levelDatas.Count; i++)
        {
            if ((levelDatas[i] as LevelData).ComingFromThatScene)
            {
                return i;
            }
        }

        return 0;
    }

    private LevelUI CreateLevelUIFor(LevelData data)
    {
        LevelUI ui = Instantiate(levelUIElementPrefab, transform);
        ui.Setup(data);
        return ui;
    }

    internal void Fokus(LevelUI levelUI, LevelData data)
    {
        StopAllCoroutines();
        StartCoroutine(TranslatePositionTo(CalculateTranslationPositonForLevelUIElement(levelUI)));

        int i = 0;
        foreach (var ui in levelUIs)
        {
            if (levelUI == ui)
            {
                rocket.selected = i;
                ui.SetFokused(true);
            }
            else
            {
                ui.SetFokused(false);
            }
            i++;
        }
    }

    private static Vector2 CalculateTranslationPositonForLevelUIElement(LevelUI levelUI)
    {
        return new Vector2(-((levelUI.transform as RectTransform).anchoredPosition.x) + 100, 0);
    }

    private static Vector2 CalculateTranslationPositonForLevelUIElement(int index)
    {
        return new Vector2(index * -200, 0);
    }

    private IEnumerator TranslatePositionTo(Vector2 target)
    {
        float distance = float.MaxValue;

        while (distance > float.MinValue)
        {
            rectToTranslate.anchoredPosition = Vector2.MoveTowards(rectToTranslate.anchoredPosition, target, Time.deltaTime * 200f);
            distance = Vector2.Distance(rectToTranslate.anchoredPosition, target);
            yield return null;
        }

    }

    internal void Play(LevelData data)
    {
        rocketSettings.previousPaths.Clear();
        SceneManager.LoadScene(data.SceneInBuildIndex);
    }
}
