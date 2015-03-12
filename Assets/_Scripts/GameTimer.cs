using UnityEngine;
using System.Collections;

public class GameTimer
{

    private static GameTimer instance = new GameTimer();

    private float endingTime;
    private float currTime;
    private bool timerActive;

    //constructor
    private GameTimer()
    {

    }

    //set instance
    public static GameTimer Instance
    {
        get { return instance; }
    }

    public void SetTimerActive(bool isActive)
    {
        this.timerActive = isActive;
        if (isActive)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public bool GetTimerActive()
    {
        return timerActive;
    }

    public void SetEndingTime(int seconds)
    {
        endingTime = seconds;
    }

    public void SetCurrentTime(int seconds)
    {
        currTime = seconds;
    }

    public float GetCurrentTime()
    {
        return currTime;
    }

    public float GetEndingTime()
    {
        return endingTime;
    }

    public float GetCurrentTimeRemain()
    {
        return endingTime - currTime;
    }

    public void UpdateDetlaTimer()
    {
        currTime += Time.deltaTime;
        if (GetCurrentTimeRemain() <= 0)
        {
            GameObject mainGame =  GameObject.Find("MainGameController");
            mainGame.gameObject.BroadcastMessage("GameOver");
        }
    }
}
