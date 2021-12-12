using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdatingText : MonoBehaviour
{
    public TMP_Text loadingText;
    private readonly string add = ". ";
    public Highscores highscoresManager;
         
    private IEnumerator Loading()
    {
        int i = 0; 

        while (true)
        {
            i++;
            loadingText.text += add;
            yield return new WaitForSeconds(0.5f);

            if(i == 6)
            {
                i = 0;
                loadingText.text = string.Empty;
            }
        }
    }

    private void OnEnable()
    {
        StartCoroutine(Loading());
    }
}
