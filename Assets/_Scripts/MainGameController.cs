using UnityEngine;
using System.Collections;

public class MainGameController : MonoBehaviour
{
    //public static MainGame mainGame = new MainGame();
    ArrayList cubes;
    int totalCubes = 10;
    bool mLockCursor = false;
    GameObject cubeTemplate;
    GameObject projectileTemplate;
    GameObject gunBarrelObject;
    GameObject playerObject;

    GameObject mainTerrain;

    MainHUD mHUD;
    ScoreTracker score;
    GameTimer gameTimer;
    bool mHudLoaded = false;
    bool mGamePaused = false;
    bool mGameplayActive = false;

    //passed into MainHUD
    public Texture2D crosshairTexture;

    private HUDStates mPreviousHUDState;

    // Use this for initialization
    void Start()
    {

        cubes = new ArrayList(totalCubes);
        cubeTemplate = GameObject.Find("Cube2");
        projectileTemplate = GameObject.Find("Projectile");
        gunBarrelObject = GameObject.Find("GunBarrel");
        playerObject = GameObject.Find("PlayerObject");

        //mainTerrain = GameObject.Find("MainTerrain");

        score = ScoreTracker.Instance;
        gameTimer = GameTimer.Instance;

        //GoToMainMenu();

    }

    public bool IsCursorLocked()
    {
        return mLockCursor;
    }

    // Update is called once per frame
    void Update()
    {

        if (mLockCursor)
        {
            Screen.lockCursor = true;
            Screen.showCursor = false;

        }
        else
        {
            Screen.lockCursor = false;
            Screen.showCursor = true;

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mGameplayActive)
            {
                PauseGame(!mGamePaused);
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            playerObject.gameObject.BroadcastMessage("SetProjectileTemplate", projectileTemplate);
            playerObject.gameObject.BroadcastMessage("SetGunBarrelObject", gunBarrelObject);
            playerObject.gameObject.BroadcastMessage("FireProjectile");
        }

        if (mGameplayActive)
        {
            gameTimer.UpdateDetlaTimer();
        }

    }

    void OnGUI () 
    {
        if (!mHudLoaded)
        {
            mHUD = new MainHUD(this);
            mHUD.Start();
            mHUD.SetCroasshairTexture(crosshairTexture);
            mHudLoaded = true;
            
        }
        else
        {
            mHUD.Update();
        }
    }

    public void PauseGame(bool paused)
    {
        if (paused)
        {
            mPreviousHUDState = mHUD.GetHUDState();
            mHUD.SetHUDState(HUDStates.PAUSE_SCREEN);
            playerObject.gameObject.BroadcastMessage("SetPlayerActive", false);
            mLockCursor = false;
            gameTimer.SetTimerActive(false);
        } else {
            mHUD.SetHUDState(mPreviousHUDState);
            playerObject.gameObject.BroadcastMessage("SetPlayerActive", true);
            mLockCursor = true;
            gameTimer.SetTimerActive(true);
        }
        mGamePaused = paused;
    }

    void QuitGame()
    {
        Debug.Break();
    }

    public void GoToMainMenu()
    {
        if (mHUD.GetHUDState() != HUDStates.MAIN_MENU)
        {
            // stop timer if active
            gameTimer.SetTimerActive(false);
            // show menu screen
            mHUD.SetHUDState(HUDStates.MAIN_MENU);
            // reset player
            playerObject.gameObject.BroadcastMessage("SetPlayerPosition", new Vector3(211f, 1f, 5f));
            playerObject.gameObject.BroadcastMessage("SetPlayerRotation", new Vector3(0f, 0f, 0f));
            //cleaup scene
            ResetGameScene(false);
        }
    }

    public void ResetGameScene(bool gameplayRemainActive)
    {
        mLockCursor = gameplayRemainActive;
        mGameplayActive = gameplayRemainActive;
        mGamePaused = !gameplayRemainActive;
        score.ResetScores();
        gameTimer.SetCurrentTime(0);
        gameTimer.SetTimerActive(false);
        ClearDemoBrickWall();

    }

    public void StartNewGame()
    {

        ResetGameScene(true);
        
        //reset player
        mHUD.SetHUDState(HUDStates.ACTIVE_GAME_SCREEN);
        playerObject.gameObject.BroadcastMessage("SetPlayerActive", true);
        playerObject.gameObject.BroadcastMessage("SetPlayerRotation", new Vector3(0f, 0f, 0f));
        DrawDemoBrickWall();

        //start timer
        gameTimer.SetTimerActive(true);
        
    }

    public void GameOver()
    {
        if (mGameplayActive)
        {
            mLockCursor = false;
            mGameplayActive = false;
            mGamePaused = true;
            mHUD.SetHUDState(HUDStates.GAME_OVER_SCREEN);
            playerObject.gameObject.BroadcastMessage("SetPlayerActive", false);
            gameTimer.SetTimerActive(false);
        }
    }

    public void DrawDemoBrickWall()
    {

        //Set timer
        gameTimer.SetEndingTime(30000);

        //set player at starting location
        playerObject.gameObject.BroadcastMessage("SetPlayerPosition", new Vector3(-10.2f, 1.1f, -18.7f));
        playerObject.gameObject.BroadcastMessage("SetPlayerRotation", new Vector3(0f, 0.75f, 0f));

        float startLeft = (totalCubes / 2);
        GameObject cube;

        //build cube wall
        for (float z = 0.5f; z < 1; z = z + (cubeTemplate.transform.localScale.z + 0.01f))
        {
            for (float y = 1.5f; y < totalCubes; y = y + (cubeTemplate.transform.localScale.y + 0.01f))
            {
                for (int x = 0; x < (totalCubes * 3); x++)
                {
                    cube = Instantiate(cubeTemplate) as GameObject;
                    float left = startLeft - ((cubeTemplate.transform.localScale.x + 0.01f) * x);
                    cube.transform.position = new Vector3(left, y, z);
                    cubes.Add(cube);
                }
            }
        }

    }

    public void ClearDemoBrickWall()
    {
        IEnumerator cubeList = cubes.GetEnumerator();

        while (cubeList.MoveNext())
        {
            GameObject cube = cubeList.Current as GameObject;
            Destroy(cube);
            
            
        }
        cubes = new ArrayList();
        
    }

}
