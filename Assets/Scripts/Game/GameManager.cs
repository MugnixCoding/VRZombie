using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameTimer gameTimer;
    [SerializeField] GameObject monsterSpawner;

    private void Start()
    {
        player.OnDead += Lose;
        gameTimer.OnReadyTimeOver += StartGame;
        gameTimer.OnSurviveTimeOver += Win;

        monsterSpawner.SetActive(false);
        gameTimer.SetTime();
        gameTimer.GetReady();
        
    }
    private void StartGame(object sender, EventArgs e)
    {
        monsterSpawner.SetActive(true);
        gameTimer.StartSurvive();
    }

    

    private void Lose(object sender,EventArgs e)
    {
        Loader.Instance.Load(Loader.Scene.FailScene, false);
    }
    private void Win(object sender, EventArgs e)
    {
        monsterSpawner.SetActive(false);
        Loader.Instance.Load(Loader.Scene.SuccessScene, false);
    }
}
