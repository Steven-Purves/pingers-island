using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManagerSession : MonoBehaviour
{
    private string saveSessionPath;

    void Start()
    {
        saveSessionPath = Application.persistentDataPath + "currentSession.txt";
    }

    public void Save(int dataToSave)
    {
        CurrentSession thisSession = new CurrentSession();
        thisSession.hightScore = dataToSave;

        string json = JsonUtility.ToJson(thisSession);

        File.WriteAllText(saveSessionPath, json);
    }

    public int LoadScore()
    {
        if (File.Exists(saveSessionPath))
        {
            string loadedString = File.ReadAllText(saveSessionPath);
            CurrentSession currentSession = JsonUtility.FromJson<CurrentSession>(loadedString);

            return currentSession.hightScore;
        }

        return 0;
    }

}
