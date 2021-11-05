using System.Collections;
using TMPro;
using UnityEngine;

public class ReadInputField : MonoBehaviour
{
    public TMP_InputField  inputField;
    private string userName;
    public int userScore = 100;

    public GameObject inputPage;
    public GameObject updatingPage;

    public void Start()
    {
        StartCoroutine(Focus()); 
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

            inputPage.SetActive(false);
            updatingPage.SetActive(true);
        }
        else
        {
            StartCoroutine(Focus());
        }
    } 
}
