using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{


    private Camera playerCam;
    private bool mPlayerActive = false;
    private GameObject mainGame;
    private GameObject projectileTemplate;
    private GameObject gunBarrelObject;
    static ScoreTracker score;

    // Use this for initialization
    void Start()
    {
        playerCam = GameObject.Find("PlayerCamera").camera;
        score = ScoreTracker.Instance;
        SetPlayerActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool GetPlayerActive()
    {
        return mPlayerActive;
    }

    public void SetPlayerPosition(Vector3 newPos)
    {
        gameObject.transform.position = newPos;
    }

    public void SetPlayerRotation(Vector3 newRot)
    {
        gameObject.transform.Rotate(newRot);
    }

    public void SetPlayerActive(bool isActive)
    {
        mPlayerActive = isActive;
        if (!mPlayerActive)
        {
            gameObject.GetComponent("MouseLook").BroadcastMessage("SetCameraActive", false);
            gameObject.GetComponent<CharacterMotor>().SetControllable(false);
            playerCam.GetComponent("MouseLook").BroadcastMessage("SetCameraActive", false);
            
        }
        else
        {
            gameObject.GetComponent("MouseLook").BroadcastMessage("SetCameraActive", true);
            gameObject.GetComponent<CharacterMotor>().SetControllable(true);
            playerCam.GetComponent("MouseLook").BroadcastMessage("SetCameraActive", true);
        }
        
    }

    void SetProjectileTemplate(GameObject template)
    {
        projectileTemplate = template;
    }

    void SetGunBarrelObject(GameObject obj)
    {
        gunBarrelObject = obj;
    }

    void FireProjectile()
    {
        if (mPlayerActive)
        {
            int speed = 1000;
            GameObject projectile = Instantiate(projectileTemplate, gunBarrelObject.transform.position, gunBarrelObject.transform.rotation) as GameObject;
            projectile.rigidbody.AddForce(playerCam.transform.forward * speed);
            score.TickTotalShotCount();
        }

    }

}
