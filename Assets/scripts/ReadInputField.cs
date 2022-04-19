using System.Collections;
using TMPro;
using UnityEngine;

public class ReadInputField : MonoBehaviour
{
    public TMP_InputField  inputField;
    public Highscores highscores;
    private string userName;
    public int userScore = 0;

    public SaveManagerSession saveSession;

    public GameObject inputPage;
    public GameObject updatingPage;
    public Texture2D cursorTex;
    public void Start()
    {
        Cursor.visible = true;
        Cursor.SetCursor(cursorTex, Vector2.zero, CursorMode.ForceSoftware);
        StartCoroutine(Focus());
        userScore = saveSession.LoadScore();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            DoneButton();
        }
    }

    private IEnumerator Focus()
    {
        yield return new WaitForEndOfFrame();
        inputField.ActivateInputField();
    }

    public void ReadStringInput(string name)
    {
        userName = name;
    }

    public void DoneButton()
    {
        if (!string.IsNullOrEmpty(userName))
        {
            Highscores.AddNewHighscore(userName, userScore);
            saveSession.Save(0);

            Invoke(nameof(CheckScore), 2);

            inputPage.SetActive(false);
            updatingPage.SetActive(true);
            this.enabled = false;

        }
        else
        {
            StartCoroutine(Focus());
        }
    } 

    public void CheckScore()
    {
        highscores.DownloadHighscores();
    }
}
