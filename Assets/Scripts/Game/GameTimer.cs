using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private int PrepareTime = 5;
    [SerializeField] private int SurviveTime = 60;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private AudioSource tickAudio;
    [SerializeField] private AudioClip tickSound;

    public event EventHandler OnReadyTimeOver;
    public event EventHandler OnSurviveTimeOver;

    private int prepareTime;
    private int surviveTime;

    private Coroutine timer;

    public void SetTime()
    {
        prepareTime = PrepareTime;
        surviveTime = SurviveTime;
    }
    public void GetReady()
    {
        if (timer==null)
        {
            timer = StartCoroutine(ReadyTimer());
        }
    }
    public void StartSurvive()
    {
        if (timer == null)
        {
            timer = StartCoroutine(surviveTimer());
        }
    }
    private IEnumerator ReadyTimer()
    {
        timerText.color = Color.yellow;
        timerText.text = "Ready: "+prepareTime;
        while (prepareTime>0)
        {
            yield return new WaitForSeconds(1);
            if (tickAudio && tickSound)
            {
                tickAudio.PlayOneShot(tickSound);
            }
            prepareTime -= 1;
            timerText.text = "Ready: " + prepareTime;
        }

        timer = null;
        timerText.text = "";
        timer = null;
        OnReadyTimeOver?.Invoke(this,EventArgs.Empty);
    }
    private IEnumerator surviveTimer()
    {
        timerText.color = Color.red;
        timerText.text = "" + surviveTime;
        while (surviveTime > 0)
        {
            yield return new WaitForSeconds(1);
            if (surviveTime<=5)
            {
                if (tickAudio && tickSound)
                {
                    tickAudio.PlayOneShot(tickSound);
                }
            }
            surviveTime -= 1;
            timerText.text = "" + surviveTime;
        }

        timerText.text = "";
        OnSurviveTimeOver?.Invoke(this, EventArgs.Empty);
    }
}
