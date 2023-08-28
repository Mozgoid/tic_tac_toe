using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI timeLeftText;

    private float timeLeft;

    public Action OnNoMoreTime;

    public void StartTurn(float turnTime)
    {
        StopAllCoroutines();
        StartCoroutine(Countdown(turnTime));
    }

    private IEnumerator Countdown(float turnTime)
    {
        timeLeft = turnTime;
        while (timeLeft > 0)
        {
            timeLeftText.text = timeLeft.ToString("0.00");
            yield return new WaitForSeconds(0.01f);
            timeLeft -= 0.01f;
        }
        timeLeftText.text = "0.00";
        OnNoMoreTime?.Invoke();
    }

    public void StopTimer()
    {
        StopAllCoroutines();
        timeLeftText.text = "0.00";
    }
}
