using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StatsMenu : MonoBehaviour
{
    public static Action OnShowStats;
    public CanvasGroup CanvasGroup;
    public Transform statsPanel;
    public Button okButton;

    private void Start()
    {
        OnShowStats += ShowStats;
        CanvasGroup.alpha = 0;
        okButton.interactable = false;
    }

    private void OnDestroy()
    {
        OnShowStats -= ShowStats;
    }

    private void ShowStats()
    {
        StartCoroutine(AnimateStatsMenu());
    }

    private IEnumerator AnimateStatsMenu()
    {
        statsPanel.localScale = Vector3.zero;
        CanvasGroup.LeanAlpha(1, 0.3f);
        
        yield return new WaitForSeconds(1);
        statsPanel.LeanScale(Vector3.one, 1f).setEaseOutQuart();

        RoundStatsPanel.instance.CountUp();
        okButton.interactable = true;
    }
}
