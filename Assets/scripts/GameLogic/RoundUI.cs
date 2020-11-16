using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

using UnityEngine;
using System;

public class RoundUI : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject enterNameUI;
    public TMP_InputField inputField;

    LevelLoaderGame levelLoader;
    MyData data;
    //bool inputBlocked;

    void Start()
    {
        levelLoader = GetComponent<LevelLoaderGame>();
    }
    void Update()
    {
       // if (!inputBlocked)
        //{
            GameOver();
            EnterHighScore();
    //    }
    }
    private void GameOver()
    {
        if (gameOverUI.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0) || Input.anyKey)
            {
                if (data.ReachedHighScore)
                {
                   gameOverUI.SetActive(false);
                   enterNameUI.SetActive(true);
                   
                }
                else
                {
                    FinishCurrentGame();
                }
            }
        }
    }
    private void EnterHighScore()
    {
        if (enterNameUI.activeInHierarchy)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(inputField.gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
               data.playerHighScore = data.CurrentScore;
               data.playerHighScoreName = inputField.text;
               FinishCurrentGame();
            }
        }
    }
    private void FinishCurrentGame()
    {
        data.ReachedHighScore = false;
        data.CurrentScore = 0;
        data.roundNumber = 0;

        levelLoader.LoadALevel(2);
    }
    public void SetGameObjectUI(MyData _data)
    {
        data = _data;
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }
}
