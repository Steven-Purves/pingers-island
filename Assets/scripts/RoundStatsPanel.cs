using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundStatsPanel : MonoBehaviour
{
    public CanvasGroup canvas;
    public TMP_Text round, white, green, red, blue, weapon, chicken;
    public AudioClip click;
    public int whiteVal, greenVal, redVal, blueVal, weaponVal, chickenVal;
    public static RoundStatsPanel instance;

    private void Awake()
    {
        GamePeriodManager.OnUpdateCurrentLevel += SetRoundNum;
        instance = this;
    }

    private void OnDestroy()
    {
        GamePeriodManager.OnUpdateCurrentLevel -= SetRoundNum;
    }

    private void SetRoundNum(int value)
    {
        round.text = $"Round {value}";
    }

    public void CountUp()
    {
        StartCoroutine(Go());
    }

    private IEnumerator Go()
    {
        yield return new WaitForSeconds(0.75f);

        yield return StartCoroutine(LoopNumber(white, whiteVal));
        yield return StartCoroutine(LoopNumber(green, greenVal));
        yield return StartCoroutine(LoopNumber(red, redVal));
        yield return StartCoroutine(LoopNumber(blue, blueVal));
        yield return StartCoroutine(LoopNumber(weapon, weaponVal));
        yield return StartCoroutine(LoopNumber(chicken, chickenVal));
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }

    private IEnumerator LoopNumber(TMP_Text label, int value)
    {
        int count = 0;

        while (count != value+1)
        {
            label.text = count.ToString();
            AudioManger.Instance.PlaySfx2D(click);
            count++;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
