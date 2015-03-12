using UnityEngine;
using System.Collections;

public class CubeBehavior : MonoBehaviour
{

    int totalHits = 3;
    int currentHits = 0;
    GameObject mainGame;
    static ScoreTracker score;

    // Use this for initialization
    void Start()
    {
        score = ScoreTracker.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        switch (totalHits - currentHits)
        {
            case 0:
                {
                    gameObject.renderer.material.color = Color.red;
                    break;
                }
            case 1:
                {
                    gameObject.renderer.material.color = Color.yellow;
                    break;
                }
            case 2:
                {
                    gameObject.renderer.material.color = Color.green;
                    break;
                }
                    

        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != "Cube2(Clone)")
        {
            StaticObjectBehaviour colStaticBehaviour = (StaticObjectBehaviour)col.gameObject.GetComponent("StaticObjectBehaviour");
            if (colStaticBehaviour != null)
            {
                if (colStaticBehaviour.causeBlockDestruction)
                {
                    currentHits++;
                }
                if (currentHits > totalHits)
                {
                    score.TickBlockBreakByFallCount();
                    Destroy(gameObject);
                }
            }
            if (col.gameObject.name == "Projectile(Clone)")
            {
                currentHits++;
                Destroy(col.gameObject);
                if (currentHits > totalHits)
                {
                    score.TickBlockBreakByShotCount();
                    Destroy(gameObject);
                }
            }

        }

    }

    void OnCollisionExit(Collision col)
    {
        //Debug.Log(currentHits);
        if (col.gameObject.name == "Terrain")
        {
           Destroy(gameObject);
        }
    }
}
