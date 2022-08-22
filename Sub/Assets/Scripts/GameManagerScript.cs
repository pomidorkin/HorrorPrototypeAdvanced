using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] Progress progress;
    public SaveManager saveManager;
    public Stage savedStage;
    /*void Start()
    {
        saveManager = SaveManager.Instance;
        SaveManager.Instance.Load();
        if (!saveManager.State.firstStart)
        {
            savedStage = progress.currentStage;
        }

        ResumeGame();
    }*/

    private void OnEnable()
    {
        saveManager = SaveManager.Instance;
        SaveManager.Instance.Load();
        if (!saveManager.State.firstStart)
        {
            savedStage = progress.currentStage;
        }

        ResumeGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
