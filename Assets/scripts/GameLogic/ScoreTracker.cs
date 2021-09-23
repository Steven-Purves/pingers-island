using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI hiScorceUI;
    public MyData data;
   
    //void Start() //redo this shite!
    //{
    //    UpdateAScore(hiScorceUI, data.playerHighScore);
    //    UpdateAScore(scoreUI, data.CurrentScore);
    //}

    void UpdateAScore(TextMeshProUGUI score, int scoreCurrent)
    {
        score.text = data.CurrentScore > 999999 ? "score \n999999" : "score \n" + scoreCurrent.ToString("D6");
    }

    public void AddPoints(int _AddPoints)
    {
        data.CurrentScore += _AddPoints;
        UpdateAScore(scoreUI, data.CurrentScore);

        if (data.CurrentScore > data.playerHighScore)
        {
            data.playerHighScore = data.CurrentScore;
            data.ReachedHighScore = true;
            UpdateAScore(hiScorceUI, data.playerHighScore);
        }
    }
}
