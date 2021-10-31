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
    List<LevelData> levelDatas;
    List<LevelUI> levelUIs;

    // Start is called before the first frame update
    void Start()
    {
        CreateUIElementsForAllLevels();
        if (levelUIs != null && levelUIs.Count > 0)
            rocket.selected = 0;
    }

    private void CreateUIElementsForAllLevels()
    {
        var lds = Resources.LoadAll("", typeof(LevelData));

        levelDatas = new List<LevelData>();
        levelUIs = new List<LevelUI>();

        foreach (LevelData data in lds)
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
        StopAllCoroutines();
        StartCoroutine(TranslatePositionTo(new Vector2(-((levelUI.transform as RectTransform).anchoredPosition.x) +100, 0)));

        int i = 0;
        foreach (var ui in levelUIs)
        {
            if (levelUI == ui)
            {
                rocket.selected = i;
                ui.SetFokused(true);
            } else
            {
                ui.SetFokused(false);
            }
            i++;
        }
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
        data.JustCameFromMenue = true;
        SceneManager.LoadScene(data.SceneInBuildIndex);
    }
}
