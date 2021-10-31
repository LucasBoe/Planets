using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : SingletonBehaviour<LevelHandler>
{
    [SerializeField] GameObject transitionPrefab;

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

        if (LevelData.JustCameFromMenue)
        {
            LevelData.JustCameFromMenue = false;
            IntroPlayer.Instance.PlayIntro(LevelData.Intro);
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReachedTarget()
    {
        Debug.Log("ReachedTarget");

        if (LevelData.Astronauts < Astronauts)
            LevelData.Astronauts = Astronauts;

        LevelData.Finished = true;

        Invoke("StartTransition", 2);
        Invoke("ReturnToLevelSelection",4);
    }

    private void StartTransition()
    {
        Instantiate(transitionPrefab);
    }

    private void ReturnToLevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}
