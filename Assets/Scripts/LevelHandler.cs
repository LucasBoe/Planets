using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : SingletonBehaviour<LevelHandler>
{
    public static bool Reset = false;

    [SerializeField] GameObject transitionPrefab;
    [SerializeField] RocketSettings rocketSettings;

    public LevelData LevelData;
    public int Astronauts;

    private void Start()
    {
        var lds = Resources.LoadAll("", typeof(LevelData));

        foreach (LevelData data in lds)
        {
            if (SceneManager.GetActiveScene().buildIndex == data.SceneInBuildIndex)
                LevelData = data;
        }

        if (Reset == false)
            IntroPlayer.Instance.PlayIntro(LevelData.Intro);
        else
            Reset = false;
    }

    public void ResetLevel()
    {
        Reset = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReachedTarget()
    {
        Debug.Log("ReachedTarget");

        if (LevelData.Astronauts < Astronauts)
            LevelData.Astronauts = Astronauts;

        LevelData.Finished = true;

        Invoke("StartTransition", 2);
        Invoke("ReturnToLevelSelection", 4);
    }

    private void StartTransition()
    {
        Instantiate(transitionPrefab);
    }

    public void ReturnToLevelSelection()
    {
        rocketSettings.previousPaths.Clear();
        LevelData.ComingFromThatScene = true;
        SceneManager.LoadScene("LevelSelection");
    }
}
