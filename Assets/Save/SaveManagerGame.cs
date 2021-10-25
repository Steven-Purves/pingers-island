using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManagerGame : MonoBehaviour
{
    public static event Action<CurrentGameSaveObject> OnSendCurrentSavedData;
    public static Action<CurrentGameSaveObject> OnSaveCurrentGame;
    public static Action<CurrentGameSaveObject> OnResetGame;

    private CurrentGameSaveObject currentGame;

    private string saveCurrentPath;

    private void Awake()
    {
        OnSaveCurrentGame += Save;
        OnResetGame += ResetGame;
        saveCurrentPath = Application.persistentDataPath + "currentGame.txt";
        Load();
    }

    private void OnDestroy()
    {
        OnSaveCurrentGame -= Save;
        OnResetGame -= ResetGame;
    }

    private void Start()
    {
        OnSendCurrentSavedData?.Invoke(currentGame);
    }

    private void Save(CurrentGameSaveObject dataToSave)
    {
        string json = JsonUtility.ToJson(dataToSave);
        File.WriteAllText(saveCurrentPath, json);
    }

    private void Load()
    {
        if (File.Exists(saveCurrentPath))
        {
            string loadedString = File.ReadAllText(saveCurrentPath);

            currentGame = JsonUtility.FromJson<CurrentGameSaveObject>(loadedString);
        }
        else
        {
            currentGame = new CurrentGameSaveObject();
            string json = JsonUtility.ToJson(currentGame);

            File.WriteAllText(saveCurrentPath, json);
        }
    }

    public void ResetGame(CurrentGameSaveObject dataToSave)
    {
        if (File.Exists(Application.persistentDataPath + "currentGame.txt"))
        {
            CurrentGameSaveObject newGame = new CurrentGameSaveObject();
            newGame.currentHighScore = dataToSave.currentHighScore;
            string json = JsonUtility.ToJson(newGame);

            File.WriteAllText(saveCurrentPath, json);
        }
    }

}
