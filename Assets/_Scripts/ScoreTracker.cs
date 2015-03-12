using UnityEngine;
using System.Collections;

public class ScoreTracker
{

    private static ScoreTracker instance = new ScoreTracker();

    //multipliers
    public int brokenByShotMultiplier = 27;
    public int brokenByFallMultiplier = 12;

    // score values
    public int totalShots = 0;
    public int totalScore = 0;
    public int blocksBrokenByShot = 0;
    public int blocksBrokenByFall = 0;

    //constructor
    private ScoreTracker()
    {

    }

    //set instance
    public static ScoreTracker Instance
    {
        get { return instance; }
    }

    //util methods
    public void ResetScores()
    {
        totalShots = 0;
        totalScore = 0;
        blocksBrokenByShot = 0;
        blocksBrokenByFall = 0;
    }

    //score setters
    public void TickBlockBreakByShotCount()
    {
        blocksBrokenByShot++;
    }

    public void TickBlockBreakByFallCount()
    {
        blocksBrokenByFall++;
    }

    public void TickTotalShotCount()
    {
        totalShots++;
    }


    //score getters
    public int GetTotalShots()
    {
        return totalShots;
    }

    public int GetTotalScore()
    {
        int totalScore = (blocksBrokenByShot * brokenByShotMultiplier) + (blocksBrokenByFall * brokenByFallMultiplier);
        return totalScore;
    }

    public int GetBlocksBrokenByShot()
    {
        return blocksBrokenByShot;
    }

    public int GetBlocksBrokenByFall()
    {
        return blocksBrokenByFall;
    }
}
