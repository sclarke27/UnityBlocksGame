using UnityEngine;
using System.Collections;

public enum HUDStates
{
    MAIN_MENU,
    PAUSE_SCREEN,
    ACTIVE_GAME_SCREEN,
    GAME_OVER_SCREEN
}

public class MainHUD
{
    private MainGameController mGame;
    private HUDStates mCurrHUDState;
    private Texture btnTexture;
    private Texture2D crosshairTexture;
    private float crosshairScale = 1;

    static ScoreTracker score;
    static GameTimer gameTimer;

    public MainHUD(MainGameController main)
    {
        this.mGame = main;
    }

    // Use this for initialization
    public void Start()
    {
        score = ScoreTracker.Instance;
        gameTimer = GameTimer.Instance;
        
    }

    // Update is called once per frame
    public void Update()
    {
        switch(this.mCurrHUDState) {
            case HUDStates.MAIN_MENU:
                this.ShowMainMenu();
                break;
            case HUDStates.ACTIVE_GAME_SCREEN:
                this.ShowGameScore();
                this.ShowCrosshair();
                this.ShowGameTimer();
                break;
            case HUDStates.PAUSE_SCREEN:
                this.ShowPauseScreen();
                break;
            case HUDStates.GAME_OVER_SCREEN:
                this.ShowGameOverScreen();
                break;
            default:
                this.ShowMainMenu();
                break;
        }
    }

    public HUDStates GetHUDState()
    {
        return mCurrHUDState;
    }

    public void SetCroasshairTexture(Texture2D newTexture)
    {
        crosshairTexture = newTexture;
    }

    public void SetHUDState(HUDStates newState)
    {
        if (newState != this.mCurrHUDState)
        {
            this.mCurrHUDState = newState;
        }
    }

    private void ShowMainMenu()
    {
        int mainBoxWidth = 200;
        int mainBoxHeight = 200;
        int mainBoxLeft = (Screen.width / 2) - (mainBoxWidth / 2);
        int mainBoxTop = (Screen.height / 2) - (mainBoxHeight / 2);
        GUI.Box(new Rect(mainBoxLeft, mainBoxTop, mainBoxWidth, mainBoxHeight), "Main Menu");

        int topMargin = 50;
        int buttonSpacing = 10;
        int buttonWidth = 100;
        int buttonHeight = 30;
        int buttonTop = mainBoxTop + topMargin;
        int buttonLeft = (mainBoxLeft + (mainBoxWidth / 2)) - (buttonWidth / 2);

        if (GUI.Button(new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight), "Start Game"))
        {
            this.mGame.StartNewGame();
        }

        buttonTop = buttonTop + buttonHeight + buttonSpacing;
        if (GUI.Button(new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight), "Exit"))
        {
            Application.Quit();
        }
    }

    private void ShowGameScore()
    {
        int currScore = score.GetTotalScore();// (this.mGame.blocksBrokenByShot * this.mGame.brokenByShotMultiplier) + (this.mGame.blocksBrokenByFall * this.mGame.brokenByFallMultiplier);

        GUI.Box(new Rect(5, 5, 130, 90), "");
        GUI.Label(new Rect(10, 10, 100, 50), "<b>Shots:</b>" + score.GetTotalShots());
        GUI.Label(new Rect(10, 25, 100, 50), "<b>Blocks Broken</b>");
        GUI.Label(new Rect(20, 40, 100, 50), "By shot:" + score.GetBlocksBrokenByShot());
        GUI.Label(new Rect(20, 55, 100, 50), "By fall:" + score.GetBlocksBrokenByFall());
        GUI.Label(new Rect(10, 70, 100, 50), "<b>Score:</b>" + currScore);

    }

    private void ShowCrosshair()
    {
        GUI.DrawTexture(new Rect((Screen.width - crosshairTexture.width * crosshairScale) / 2, (Screen.height - crosshairTexture.height * crosshairScale) / 2, crosshairTexture.width * crosshairScale, crosshairTexture.height * crosshairScale), crosshairTexture);
    }

    private void ShowGameOverScreen()
    {
        int mainBoxWidth = 200;
        int mainBoxHeight = 200;
        int mainBoxLeft = (Screen.width / 2) - (mainBoxWidth / 2);
        int mainBoxTop = (Screen.height / 2) - (mainBoxHeight / 2);
        GUI.Box(new Rect(mainBoxLeft, mainBoxTop, mainBoxWidth, mainBoxHeight), "<b>Game Over</b>");

        int topMargin = 50;
        int buttonSpacing = 10;
        int buttonWidth = 100;
        int buttonHeight = 30;
        int buttonTop = mainBoxTop + topMargin;
        int buttonLeft = (mainBoxLeft + (mainBoxWidth / 2)) - (buttonWidth / 2);

        if (GUI.Button(new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight), "Restart Game"))
        {
            this.mGame.ClearDemoBrickWall();
            this.mGame.StartNewGame();
        }

        buttonTop = buttonTop + buttonHeight + buttonSpacing;
        if (GUI.Button(new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight), "Main Menu"))
        {
            this.mGame.GoToMainMenu();
        }
    }

    private void ShowGameTimer() {
        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;
        centeredStyle.fontSize = 14;

        float currTime = gameTimer.GetCurrentTimeRemain();

        int mainBoxWidth = 200;
        int mainBoxHeight = 50;
        int padding = 10;
        int mainBoxLeft = Screen.width - mainBoxWidth - padding;
        int mainBoxTop = padding;
        GUI.Box(new Rect(mainBoxLeft, mainBoxTop, mainBoxWidth, mainBoxHeight), "Time Remaining:");

        GUI.Label(new Rect(mainBoxLeft + padding, (padding * 3), 150, 50), "" + currTime, centeredStyle);
        //Debug.Log(currTime);
    }

    private void ShowPauseScreen()
    {
        int mainBoxWidth = 200;
        int mainBoxHeight = 200;
        int mainBoxLeft = (Screen.width / 2) - (mainBoxWidth / 2);
        int mainBoxTop = (Screen.height / 2) - (mainBoxHeight / 2);
        GUI.Box(new Rect(mainBoxLeft, mainBoxTop, mainBoxWidth, mainBoxHeight), "<b>Pause</b>");

        int topMargin = 50;
        int buttonSpacing = 10;
        int buttonWidth = 100;
        int buttonHeight = 30;
        int buttonTop = mainBoxTop + topMargin;
        int buttonLeft = (mainBoxLeft + (mainBoxWidth / 2)) - (buttonWidth / 2);

        if (GUI.Button(new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight), "Restart Game"))
        {
            this.mGame.ClearDemoBrickWall();
            this.mGame.StartNewGame();
        }

        buttonTop = buttonTop + buttonHeight + buttonSpacing;
        if (GUI.Button(new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight), "Main Menu"))
        {
            this.mGame.GoToMainMenu();
        }

        buttonTop = buttonTop + buttonHeight + buttonSpacing;
        if (GUI.Button(new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight), "Exit"))
        {
            Application.Quit();
        }

    }
}
